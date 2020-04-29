namespace UI.Game
{
    public class MainView : UIView
    {
        public SlotView blueSlotView;
        public SlotView redSlotView;

        private UserComponent userComponent;

        protected override void Awake()
        {
            base.Awake();

            userComponent = GetUIComponent<UserComponent>();
        }

        private void OnEnable()
        {
            blueSlotView?.Upsert(ServerInfo.MyGameUser());
            redSlotView?.Upsert(ServerInfo.EnemyGameUser());
            userComponent?.Upsert(ServerInfo.MyGameUser());
        }
    }
}
