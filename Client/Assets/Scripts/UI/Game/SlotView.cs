using Extension;
using Network.GameServer;
using System.Collections.Generic;

namespace UI.Game
{
    public class SlotView : UIView<SlotComponent, GameSlot>
    {
        private List<SlotComponent> slotComponents;

        protected override void Awake()
        {
            base.Awake();

            slotComponents = GetUIComponents<SlotComponent>();
        }

        public void Upsert(GameUser user)
        {
            if (user == null)
                gameObject.SetVisible(false);

            user?.Slots.ForEach((x, i) =>
            {
                slotComponents[i].Upsert(i, x);
            });
        }
    }
}
