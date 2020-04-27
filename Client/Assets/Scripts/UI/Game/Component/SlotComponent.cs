using Extension;
using Network.GameServer;
using UnityEngine.UI;

namespace UI.Game
{
    public class SlotComponent : UIComponent<GameSlot>
    {
        public Image cube_image;
        public Text lv_text;

        public override void Upsert(GameSlot data)
        {
            base.Upsert(data);

            lv_text.SetText(data?.GameLv.ToString());
        }
    }
}
