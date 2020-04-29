using Extension;
using Network.GameServer;
using System.Collections.Generic;

namespace UI.Game
{
    public class SlotView : UIView<GameSlot>
    {
        private List<SlotComponent> slotComponents;

        protected override void Awake()
        {
            base.Awake();

            slotComponents = GetUIComponents<SlotComponent>();
        }

        public void Upsert(GameUser user)
        {
            user?.Slots.ForEach((x, i) =>
            {
                slotComponents[i].Upsert(i, x);
            });
        }
    }
}
