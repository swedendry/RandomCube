using Network.GameServer;

namespace UI.Game
{
    public class TeamContainer : UIContainer
    {
        public Team teamHandler;

        private SlotContainer slotContainer;
        private UserComponent userComponent;

        protected override void Awake()
        {
            base.Awake();

            slotContainer = GetUIContainer<SlotContainer>();
            slotContainer.OnEventProps = Event;
            userComponent = GetUIComponent<UserComponent>();
        }

        public void Upsert(GameUser user)
        {
            slotContainer?.Upsert(user);
            userComponent?.Upsert(user);
        }

        public override void Event(string param)
        {
            switch (param)
            {
                case "Create":
                    {
                        var data = userComponent.props.data;
                        var needsp = ServerDefine.CubeSeq2NeedSP(data.CubeSeq);
                        if (data.SP < needsp)
                            return;

                        teamHandler?.OnCreateCube(1);
                    }
                    break;
            }
            base.Event(param);
        }

        public void Event(bool isSelected, Props<GameSlot> props)
        {
            if (!isSelected)
                return;

            var slotIndex = props.data.SlotIndex;
            teamHandler?.OnUpdateSlot(slotIndex);
        }
    }
}
