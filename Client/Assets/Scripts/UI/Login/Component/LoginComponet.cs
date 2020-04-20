using Extension;
using UnityEngine.UI;

namespace UI.Login
{
    public class LoginComponet : UIComponent
    {
        public Text title_text;

        public override void Upsert()
        {
            base.Upsert();

            title_text.SetText("Login");
        }
    }
}