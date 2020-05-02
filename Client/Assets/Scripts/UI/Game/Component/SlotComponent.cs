using Extension;
using Network.GameServer;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class SlotComponent : SlotComponent<GameSlot>
    {
        public Image cube_image;
        public Text lv_text;
        public Text sp_text;

        public override void Upsert(int index, GameSlot data)
        {
            base.Upsert(index, data);

            var cubeData = XmlKey.CubeData.Find<CubeDataXml.Data>(x => x.CubeId == data.CubeId);
            var price = ServerDefine.SlotLv2Price(data.SlotLv);

            cube_image.color = new Color(cubeData.Color[0], cubeData.Color[1], cubeData.Color[2]);
            lv_text.SetText(data.SlotLv.ToString());
            sp_text.SetText(price.ToString());
        }
    }
}
