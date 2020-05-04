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
                        SceneManager.LoadScene("SingleGame", LoadSceneMode.Single);
                    }
                    break;
                case "multiplay":
                    {
                        GameServer.sInstance.EnterMatch(ServerInfo.User.Id);
                    }
                    break;
                case "recordplay":
                    {
                        SceneManager.LoadScene("RecordGame", LoadSceneMode.Single);
                    }
                    break;
            }
        }
    }
}
