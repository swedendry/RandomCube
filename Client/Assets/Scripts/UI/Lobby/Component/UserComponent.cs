using Extension;
using UnityEngine.UI;

namespace UI.Lobby
{
    public class UserComponent : UIComponent
    {
        public Text id_text;
        public Text name_text;

        public override void Upsert()
        {
            base.Upsert();

            id_text.SetText(ServerInfo.User.Id);
            name_text.SetText(ServerInfo.User.Name);
        }
    }
}