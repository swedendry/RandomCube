using Extension;
using Network.GameServer;
using UnityEngine.UI;

namespace UI.Game
{
    public class SlotComponent : UIComponent<GameCube>
    {
        public Image cube_image;
        public Text lv_text;

        public override void Upsert(GameCube data)
        {
            base.Upsert(data);

            lv_text.SetText(data?.GameLv.ToString());
        }
    }
}
