using Network;
using UnityEngine;

namespace UI.Login
{
    public class MainView : UIView
    {
        public override void Event(string param)
        {
            switch (param)
            {
                case "login":
                    {
                        Login();
                    }
                    break;
            }
        }

        private void Login()
        {
            var id = SystemInfo.deviceUniqueIdentifier;

            LobbyServer.sInstance?.GetUser(id).Callback(
            success: (data) =>
            {
                GameServer.sInstance.Connect();
            },
            fail: (code) =>
            {
                switch (code)
                {
                    case PayloadCode.DbNull:
                        {
                            Router.CloseAndOpen("RegisterView");
                        }
                        break;
                }
            });
        }
    }
}
