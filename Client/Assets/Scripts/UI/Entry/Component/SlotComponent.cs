using Extension;
using Network.LobbyServer;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Entry
{
    [RequireComponent(typeof(Image))]
    public class SlotComponent : UIComponent<CubeViewModel>
    {
        private Image cube_image;
        public Text lv_text;

        public GameObject selected_obj;
        public GameObject lock_obj;

        private void Awake()
        {
            cube_image = GetComponent<Image>();
        }

        public override void Empty()
        {
            base.Empty();

            Selected(false);
            Lock(false);
        }

        public override void Upsert(int index, CubeViewModel data)
        {
            base.Upsert(index, data);

            var cubeData = XmlKey.CubeData.Find<CubeDataXml.Data>(x => x.CubeId == data.CubeId);
            cube_image.color = new Color(cubeData.Color[0], cubeData.Color[1], cubeData.Color[2]);
            lv_text?.SetText(data?.Lv.ToString());
        }

        public override void Event()
        {
            if (lock_obj.activeSelf)
                return; //lock 클릭 안됌

            base.Event();
        }

        public void Selected(bool isSelected)
        {
            selected_obj?.SetVisible(isSelected);
        }

        public void Lock(bool isLock)
        {
            lock_obj?.SetVisible(isLock);
        }
    }
}
