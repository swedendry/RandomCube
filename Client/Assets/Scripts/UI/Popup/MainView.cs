using UnityEngine;

namespace UI.Popup
{
    public class MainView : UIView
    {
        public override void Event(string param)
        {
            switch (param)
            {
                case "yes":
                    {
                        Application.Quit();
                    }
                    break;
                case "no":
                    {
                        Router.Close("PopupView");
                    }
                    break;
            }
        }
    }
}
