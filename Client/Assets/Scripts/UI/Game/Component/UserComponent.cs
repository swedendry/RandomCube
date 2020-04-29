using Extension;
using Network.GameServer;
using UnityEngine.UI;

namespace UI.Game
{
    public class UserComponent : UIComponent<GameUser>
    {
        public Team team;
        public Text sp_text;
        public Text life_text;

        private void Update()
        {
            if (props != null && props.data != null)
            {
                sp_text.SetText(props.data.SP.ToString());
                life_text.SetText(props.data.Life.ToString());
            }
        }

        public override void Event(string param)
        {
            switch (param)
            {
                case "Create":
                    {
                        team?.CreateCube(1);
                    }
                    break;
            }
            base.Event(param);
        }
    }
}
