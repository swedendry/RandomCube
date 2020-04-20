using Extension;
using UI;
using UnityEngine.UI;

public class LoginComponet : UIComponent
{
    public Text title_text;

    public override void Upsert()
    {
        base.Upsert();

        title_text.SetText("Login");
    }
}
