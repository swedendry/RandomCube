namespace UI.Match
{
    public class MainView : UIView
    {
        public override void Event(string param)
        {
            switch (param)
            {
                case "cancel":
                    {
                        GameServer.sInstance.ExitMatch(ServerInfo.User.Id);
                    }
                    break;
            }
        }
    }
}
