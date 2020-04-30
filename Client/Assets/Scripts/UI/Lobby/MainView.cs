namespace UI.Lobby
{
    public class MainView : UIView
    {
        private UserComponent userComponent;

        protected override void Awake()
        {
            base.Awake();

            LobbyServer.ActionUpdateCubeLv += (payloader) =>
            {
                payloader.Callback(
                success: (data) =>
                {
                    userComponent?.Upsert();
                });
            };

            userComponent = GetUIComponent<UserComponent>();
        }

        public override void Upsert()
        {
            userComponent?.Upsert();
        }

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
