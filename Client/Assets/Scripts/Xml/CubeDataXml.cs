using System.Xml;
using Xmls;

public class CubeDataXml : Xml
{
    public class Data
    {
        public int CubeId { get; set; }
        public string Name { get; set; }
        public float AD { get; set; } //attack damage
        public float AS { get; set; } //attack speed
        public int SkillId { get; set; }
    }

    public override bool Set(XmlNode node)
    {
        var data = new Data();
        data.CubeId = GetValue<int>(node, "CubeId");
        data.Name = GetValue<string>(node, "Name");
        data.AD = GetValue<float>(node, "AD");
        data.AS = GetValue<float>(node, "AS");
        data.SkillId = GetValue<int>(node, "SkillId");

        _datas.Add(data);
        return true;
    }
}
