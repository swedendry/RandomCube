using Network.LobbyServer;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Register
{
    public class MainView : UIView
    {
        public InputField name_input;

        public override void Event(string param)
        {
            switch (param)
            {
                case "register":
                    {
                        Register();
                    }
                    break;
            }
        }

        private void Register()
        {
            var id = ServerInfo.userId; //var id = SystemInfo.deviceUniqueIdentifier;
            var name = name_input.text;

            var body = new CreateUserBody()
            {
                Id = id,
                Name = name,
            };

            LobbyServer.sInstance?.CreateUser(body).Callback(
            success: (data) =>
            {
                GameServer.sInstance.Connect();
            });
        }
    }
}
