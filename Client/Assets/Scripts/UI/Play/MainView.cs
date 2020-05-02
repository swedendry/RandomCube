using UnityEngine.SceneManagement;

namespace UI.Play
{
    public class MainView : UIView
    {
        public override void Event(string param)
        {
            switch (param)
            {
                case "singleplay":
                    {
                        GameServer.sInstance.isLockSend = true;

                        SceneManager.LoadScene("SingleGame", LoadSceneMode.Single);
                    }
                    break;
                case "multiplay":
                    {
                        GameServer.sInstance.isLockSend = false;

                        GameServer.sInstance.EnterMatch(ServerInfo.User.Id);
                    }
                    break;
            }
        }
    }
}
