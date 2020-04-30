using Extension;
using UnityEngine.UI;

namespace UI.Lobby
{
    public class UserComponent : UIComponent
    {
        public Text name_text;
        public Text money_text;

        public override void Upsert()
        {
            base.Upsert();

            name_text.SetText(ServerInfo.User.Name);
            money_text.SetText(ServerInfo.User.Money.ToString());
        }
    }
}