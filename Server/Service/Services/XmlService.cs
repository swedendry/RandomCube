using Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Service.Services
{
    public class XmlService
    {
        private static readonly List<Xml> _xml = new List<Xml>();

        protected static bool Load(Xml xml, string key)
        {
            try
            {
                string fullpath = "wwwroot/xml/" + key + ".xml";
                return Load(xml, key, fullpath);
            }
            catch (Exception)
            {

            }

            return false;
        }

        protected static bool Load(Xml xml, string key, string fullpath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fullpath);
                if (xmlDoc == null)
                    return false;

                xml.key = key;
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
            }

            return false;
        }

        public static Xml Find(string key)
        {
            return _xml.Find(p => p.key == key);
        }
    }

    public class Xml
    {
        public string key;

        protected List<object> _datas = new List<object>();

        public bool LoadXML(XmlElement xmlElement)
        {
            int nodecount = xmlElement.ChildNodes.Count;
            for (int i = 0; i < nodecount; i++)
            {
                XmlNode node = xmlElement.ChildNodes[i];
                if (!Set(node))
                    return false;
            }

            return true;
        }

        public virtual bool Set(XmlNode node) { return true; }
        public virtual List<T> FindAll<T>() { return _datas.Cast<T>().ToList(); }
        public virtual List<T> FindAll<T>(Predicate<T> match) { return FindAll<T>().FindAll(match); }
        public virtual T Find<T>(Predicate<T> match) { return FindAll<T>().Find(match); }

        public T GetValue<T>(XmlNode node, string key)
        {
            var attributes = node.Attributes[key];
            if (attributes == null || string.IsNullOrEmpty(attributes.Value))
                return default;

            return attributes.Value.Parse<T>();
        }

        public IEnumerable<T> GetSplitValue<T>(XmlNode node, string key, char separator)
        {
            var value = GetValue<string>(node, key);
            var splitValue = value.Split(separator);

            return splitValue.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Parse<T>());
        }
    }
}
