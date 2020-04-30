using Microsoft.AspNetCore.Http;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Service.Xmls
{
    public enum XmlKey
    {
        CubeData,
        SkillData,
    }

    public static class XmlExtension
    {
        public static List<T> FindAll<T>(this XmlKey key, IFormFile file, Xml xml)
        {
            return file == null ? key.FindAll<T>() : file.FindAll<T>(xml);
        }

        public static List<T> FindAll<T>(this XmlKey key)
        {
            return XmlService.Find(key.ToString()).FindAll<T>();
        }

        public static List<T> FindAll<T>(this XmlKey key, Predicate<T> match)
        {
            return XmlService.Find(key.ToString()).FindAll(match);
        }

        public static T Find<T>(this XmlKey key, Predicate<T> match)
        {
            return XmlService.Find(key.ToString()).Find(match);
        }

        public static List<T> FindAll<T>(this IFormFile file, Xml xml)
        {
            using (var fileStram = file.OpenReadStream())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileStram);
                xml.LoadXML(xmlDoc.DocumentElement);

                return xml.FindAll<T>();
            }
        }
    }

    public class GameXmlService : XmlService
    {
        public static void Initialize()
        {
            Load(new CubeDataXml(), XmlKey.CubeData.ToString());
        }
    }
}
