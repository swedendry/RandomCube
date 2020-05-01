using Extension;
using Network.GameServer;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UI.Game
{
    public class UserComponent : UIComponent<GameUser>
    {
        public Team team;
        public Text name_text;
        public Text sp_text;
        public Text needsp_text;
        public List<Image> life_image;

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

        public override void Upsert(GameUser user)
        {
            base.Upsert(user);

            name_text.SetText(user.Name);
            sp_text.SetText(user.SP.ToString());
            needsp_text.SetText(ServerDefine.Seq2NeedSP(user.CubeSeq).ToString());
            life_image.ForEach((x, i) =>
            {
                x.gameObject.SetVisible((i >= user.Life));
            });
        }
    }
}
