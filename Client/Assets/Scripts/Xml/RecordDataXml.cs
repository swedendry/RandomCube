using Newtonsoft.Json;
using System.Xml;
using Xmls;

public class RecordDataXml : Xml
{
    public class Data
    {
        public int Index { get; set; }
        public float ProgressTime { get; set; }
        public string Pack { get; set; }
    }

    public override bool Set(XmlNode node)
    {
        var data = new Data();
        data.Index = GetValue<int>(node, "Index");
        data.ProgressTime = GetValue<float>(node, "ProgressTime");
        data.Pack = GetValue<string>(node, "Pack");

        _datas.Add(data);
        return true;
    }

    public override bool Save(params object[] values)
    {
        var data = new Data();
        data.Index = _datas.Count;
        data.ProgressTime = (float)values[0];
        data.Pack = JsonConvert.SerializeObject(values[1]);
        _datas.Add(data);

        var xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
        var root = xmlDoc.AppendChild(xmlDoc.CreateNode(XmlNodeType.Element, "XML", string.Empty));

        _datas.ForEach(x =>
        {
            var item = (XmlElement)root.AppendChild(xmlDoc.CreateNode(XmlNodeType.Element, "DATA", string.Empty));
            var value = (Data)x;
            item.SetAttribute("Index", value.Index.ToString());
            item.SetAttribute("ProgressTime", value.ProgressTime.ToString());
            item.SetAttribute("Pack", value.Pack);
        });

        xmlDoc.Save(string.Format("./Assets/Resources/xml/{0}.xml", key));

        return true;
    }
}