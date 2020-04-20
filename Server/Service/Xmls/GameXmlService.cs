using Microsoft.AspNetCore.Http;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Service.Xmls
{
    public static class XMLNAME
    {   //XML 파일 네임
        public const string CubeData = "CubeData";
    }

    public class GameXmlService : XmlService
    {
        public static void Initialize()
        {
            Load(new CubeDataXml(), XMLNAME.CubeData);
        }
    }

    public static class XmlExtension
    {
        public static List<T> FindAll<T>(this string key, IFormFile file, Xml xml)
        {
            return file == null ? key.FindAll<T>() : file.FindAll<T>(xml);
        }

        public static List<T> FindAll<T>(this string key)
        {
            return XmlService.Find(key).FindAll<T>();
        }

        public static List<T> FindAll<T>(this string key, Predicate<T> match)
        {
            return XmlService.Find(key).FindAll(match);
        }

        public static T Find<T>(this string key, Predicate<T> match)
        {
            return XmlService.Find(key).Find(match);
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
}
