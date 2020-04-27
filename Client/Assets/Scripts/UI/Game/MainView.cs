namespace UI.Game
{
    public class MainView : UIView
    {
        private UserComponent userComponent;

        private void Start()
        {
            userComponent = GetUIComponent<UserComponent>();
        }

        private void OnEnable()
        {
            userComponent.Upsert();
        }
    }
}
