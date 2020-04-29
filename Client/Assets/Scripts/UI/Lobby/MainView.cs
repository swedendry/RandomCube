namespace UI.Lobby
{
    public class MainView : UIView
    {
        private void OnEnable()
        {
            Router.Open("LobbyView/PlayView");
        }
    }
}
