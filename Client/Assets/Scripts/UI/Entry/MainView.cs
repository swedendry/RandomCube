using Network.LobbyServer;
using System.Collections.Generic;
using System.Linq;

namespace UI.Entry
{
    public class MainView : UIView
    {
        private TargetContainer targetContainer;
        private MaterialContainer materialContainer;

        private List<int> dummySlots;

        protected override void Awake()
        {
            base.Awake();

            targetContainer = GetUIContainer<TargetContainer>();
            materialContainer = GetUIContainer<MaterialContainer>();
            targetContainer.OnEventProps = TargetEvent;
            materialContainer.OnEventProps = MaterialEvent;
        }

        private void OnDisable()
        {
            LobbyServer.sInstance?.UpdateEntry(ServerInfo.User.Id, dummySlots.ToArray());
        }

        public override void Upsert()
        {
            dummySlots = ServerInfo.User.Entry.Slots.ToList();

            targetContainer.Upsert(dummySlots);
            materialContainer.Upsert(dummySlots);
        }

        private void TargetEvent(bool isSelected, Props<CubeViewModel> props)
        {
            if (isSelected)
            {
                TargetSwap();
                MaterialSwap();
            }
        }

        private void MaterialEvent(bool isSelected, Props<CubeViewModel> props)
        {
            if (isSelected)
            {
                MaterialSwap();
            }
        }

        private void TargetSwap()
        {
            if (targetContainer.selectProps.Count < 2)
                return;

            var target1 = targetContainer.selectProps[0];
            var target2 = targetContainer.selectProps[1];

            dummySlots[target1.index] = target2.data.CubeId;
            dummySlots[target2.index] = target1.data.CubeId;

            targetContainer.Upsert(dummySlots);
        }

        private void MaterialSwap()
        {
            var target = targetContainer.selectProps.FirstOrDefault();
            var material = materialContainer.selectProps.FirstOrDefault();

            if (target == null || material == null)
                return;

            dummySlots[target.index] = material.data.CubeId;

            targetContainer.Upsert(dummySlots);
            materialContainer.Upsert(dummySlots);
        }
    }
}
