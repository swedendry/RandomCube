using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using Xmls;

public enum XmlKey
{
    CubeData,
    SkillData,
    RecordData,
}

public static class XmlExtension
{
    public static List<T> FindAll<T>(this XmlKey key)
    {
        return XmlFactory.Find(key.ToString()).FindAll<T>();
    }

    public static List<T> FindAll<T>(this XmlKey key, Predicate<T> match)
    {
        return XmlFactory.Find(key.ToString()).FindAll(match);
    }

    public static T Find<T>(this XmlKey key, Predicate<T> match)
    {
        return XmlFactory.Find(key.ToString()).Find(match);
    }
}

public static class XmlFactory
{
    private static readonly List<Xml> _xml = new List<Xml>();

    public static void Load()
    {
        Load(new CubeDataXml(), XmlKey.CubeData.ToString());
        Load(new SkillDataXml(), XmlKey.SkillData.ToString());
        Load(new RecordDataXml(), XmlKey.RecordData.ToString());
    }

    private static bool Load(Xml xml, string key)
    {
        try
        {
            string fullpath = "xml/" + key;
            TextAsset textAsset = Resources.Load(fullpath) as TextAsset;

            XmlDocument xmlDoc = new XmlDocument();
            if (textAsset != null)
                xmlDoc.LoadXml(textAsset.text);
            if (xmlDoc == null)
                return false;

            xml.key = key;
            if (xmlDoc.DocumentElement != null)
                if (!xml.LoadXML(xmlDoc.DocumentElement))
                    return false;

            //중복 키 XML 존재시 덮처쓰기
            Xml tempxml = Find(key);
            if (tempxml != null)
            {
                _xml.Remove(tempxml);
            }

            _xml.Add(xml);

            return true;
        }
        catch (Exception e)
        {
            string message = e.Message;
            Debug.LogError(message);
        }

        return false;
    }

    public static bool Save(string key, params object[] values)
    {
        var xml = Find(key);
        return xml.Save(values);
    }

    public static Xml Find(string key)
    {
        return _xml.Find(p => p.key == key);
    }
}
