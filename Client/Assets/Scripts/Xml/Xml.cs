using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Xmls
{
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
        public virtual bool Save(object value) { return true; }

        public T GetValue<T>(XmlNode node, string key)
        {
            var attributes = node.Attributes[key];
            if (attributes == null || string.IsNullOrEmpty(attributes.Value))
                return default(T);

            return GetValue<T>(attributes.Value);
        }

        public IEnumerable<T> GetSplitValue<T>(XmlNode node, string key, char separator)
        {
            var value = GetValue<string>(node, key);
            var splitValue = value.Split(separator);

            return splitValue.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => GetValue<T>(x));
        }

        private T GetValue<T>(string value)
        {
            var defaultValue = default(T);
            var isEnum = (defaultValue != null && defaultValue.GetType().IsEnum);

            return isEnum ? (T)Enum.Parse(typeof(T), value, true) : (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
