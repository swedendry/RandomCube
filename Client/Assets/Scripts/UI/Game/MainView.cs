namespace UI.Game
{
    public class MainView : UIView
    {
        private TeamContainer blueContainer;
        private TeamContainer redContainer;

        protected override void Awake()
        {
            base.Awake();

            blueContainer = GetUIContainer<TeamContainer>(0);
            redContainer = GetUIContainer<TeamContainer>(1);
        }

        public override void Upsert()
        {
            blueContainer?.Upsert(ServerInfo.MyGameUser());
            redContainer?.Upsert(ServerInfo.EnemyGameUser());
        }
    }
}
