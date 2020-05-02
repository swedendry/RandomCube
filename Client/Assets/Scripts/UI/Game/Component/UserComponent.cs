using Extension;
using Network.GameServer;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UI.Game
{
    public class UserComponent : UIComponent<GameUser>
    {
        public Text name_text;
        public Text sp_text;
        public Text needsp_text;
        public Image needsp_lock_image;
        public List<Image> life_image;

        public override void Upsert(GameUser data)
        {
            base.Upsert(data);

            if (data == null)
                return;

            var needsp = ServerDefine.CubeSeq2NeedSP(data.CubeSeq);
            name_text.SetText(data.Name);
            sp_text.SetText(data.SP.ToString());
            needsp_text.SetText(needsp.ToString());
            needsp_lock_image?.gameObject.SetVisible(data.SP < needsp);
            life_image.ForEach((x, i) =>
            {
                x.gameObject.SetVisible((i < data.Life));
            });
        }
    }
}
