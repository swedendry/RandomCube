using Extension;
using Network.GameServer;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    [RequireComponent(typeof(Image))]
    public class SlotComponent : UIComponent<GameSlot>
    {
        private Image cube_image;
        public Text lv_text;
        public Text sp_text;

        private void Awake()
        {
            cube_image = GetComponent<Image>();
        }

        public override void Upsert(int index, GameSlot data)
        {
            base.Upsert(index, data);

            var cubeData = XmlKey.CubeData.Find<CubeDataXml.Data>(x => x.CubeId == data.CubeId);
            var price = data.CubeLv * 100;

            cube_image.color = new Color(cubeData.Color[0], cubeData.Color[1], cubeData.Color[2]);
            lv_text?.SetText(data?.SlotLv.ToString());
            sp_text?.SetText(price.ToString());
        }
    }
}
