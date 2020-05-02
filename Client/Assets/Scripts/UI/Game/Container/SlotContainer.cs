using Extension;
using Network.GameServer;
using System.Collections.Generic;

namespace UI.Game
{
    public class SlotContainer : UIContainer<SlotComponent, GameSlot>
    {
        private List<SlotComponent> slotComponents;

        protected override void Awake()
        {
            base.Awake();

            slotComponents = GetUIComponents<SlotComponent>();
        }

        public void Upsert(GameUser data)
        {
            base.Empty();

            if (data == null)
                return;

            base.Upsert();

            data.Slots.ForEach((x, i) =>
            {
                slotComponents[i].Upsert(i, x);
                slotComponents[i].Lock(data.SP < ServerDefine.SlotLv2Price(x.SlotLv));
            });
        }
    }
}