using Service.Services;
using System.Xml;

namespace Service.Xmls
{
    public class SkillDataXml : Xml
    {
        public class Data
        {
            public int SkillId { get; set; }
            public string Name { get; set; }
            public float Percent { get; set; }
            public float Duration { get; set; }
        }

        public override bool Set(XmlNode node)
        {
            var data = new Data();
            data.SkillId = GetValue<int>(node, "SkillId");
            data.Name = GetValue<string>(node, "Name");
            data.Percent = GetValue<float>(node, "Percent");
            data.Duration = GetValue<float>(node, "Duration");

            _datas.Add(data);
            return true;
        }
    }
}
