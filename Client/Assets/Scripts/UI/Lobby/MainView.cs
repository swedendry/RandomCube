namespace UI.Lobby
{
    public class MainView : UIView
    {
        //private void OnEnable()
        //{
        //    Router.Open("LobbyView/PlayView");
        //}

        public override void Event(string param)
        {
            switch (param)
            {
                case "playview":
                    {
                        Router.CloseAndOpen("LobbyView/PlayView");
                    }
                    break;
                case "entryview":
                    {
                        Router.CloseAndOpen("LobbyView/EntryView");
                    }
                    break;
                case "cubeview":
                    {
                        Router.CloseAndOpen("LobbyView/CubeView");
                    }
                    break;
            }
        }
    }
}
