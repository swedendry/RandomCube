using Extension;
using Network.LobbyServer;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Entry
{
    public class SlotComponent : SlotComponent<CubeViewModel>
    {
        public Image cube_image;
        public Text lv_text;

        public override void Upsert(int index, CubeViewModel data)
        {
            base.Upsert(index, data);

            var cubeData = XmlKey.CubeData.Find<CubeDataXml.Data>(x => x.CubeId == data.CubeId);
            cube_image.color = new Color(cubeData.Color[0], cubeData.Color[1], cubeData.Color[2]);
            lv_text?.SetText(data?.Lv.ToString());
        }
    }
}
