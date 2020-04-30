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

        public override void Upsert()
        {
            blueSlotView?.Upsert(ServerInfo.MyGameUser());
            redSlotView?.Upsert(ServerInfo.EnemyGameUser());
            userComponent?.Upsert(ServerInfo.MyGameUser());
        }
    }
}
