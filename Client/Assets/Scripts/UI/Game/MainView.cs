namespace UI.Game
{
    public class MainView : UIView
    {
        private UserComponent userComponent;

        protected override void Awake()
        {
            base.Awake();

            userComponent = GetUIComponent<UserComponent>();
        }

        private void OnEnable()
        {
            userComponent.Upsert(ServerInfo.MyGameUser());
        }
    }
}
