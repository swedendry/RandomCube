namespace UI.Play
{
    public class MainView : UIView
    {
        public override void Event(string param)
        {
            switch (param)
            {
                case "match":
                    {
                        GameServer.sInstance.EnterMatch(ServerInfo.User.Id);
                    }
                    break;
            }
        }
    }
}
