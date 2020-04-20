using Extension;
using UnityEngine.UI;

namespace UI.Game
{
    public class UserComponent : UIComponent
    {
        public GameUser user;
        public Text sp_text;
        public Text life_text;

        public override void Upsert()
        {
            base.Upsert();
        }

        private void Update()
        {
            sp_text.SetText(ServerInfo.MyUser.SP.ToString());
            life_text.SetText(ServerInfo.MyUser.Life.ToString());
        }

        public override void Event(string param)
        {
            switch(param)
            {
                case "Create":
                    {
                        user.CreateCube();
                    }
                    break;
            }
            base.Event(param);
        }
    }
}
