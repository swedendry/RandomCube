using Network.LobbyServer;
using System.Collections.Generic;
using System.Linq;

namespace UI.Entry
{
    public class MainView : UIView
    {
        public TargetView targetView;
        public MaterialView materialView;

        private List<int> dummySlots;

        protected override void Awake()
        {
            targetView.OnEventProps = TargetEvent;
            materialView.OnEventProps = MaterialEvent;
        }

        private void OnDisable()
        {
            LobbyServer.sInstance?.UpdateEntry(ServerInfo.User.Id, dummySlots.ToArray());
        }

        public override void Upsert()
        {
            dummySlots = ServerInfo.User.Entry.Slots.ToList();

            targetView.Upsert(dummySlots);
            materialView.Upsert(dummySlots);
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
            if (targetView.selectProps.Count < 2)
                return;

            var target1 = targetView.selectProps[0];
            var target2 = targetView.selectProps[1];

            dummySlots[target1.index] = target2.data.CubeId;
            dummySlots[target2.index] = target1.data.CubeId;

            targetView.Upsert(dummySlots);
        }

        private void MaterialSwap()
        {
            var target = targetView.selectProps.FirstOrDefault();
            var material = materialView.selectProps.FirstOrDefault();

            if (target == null || material == null)
                return;

            dummySlots[target.index] = material.data.CubeId;

            targetView.Upsert(dummySlots);
            materialView.Upsert(dummySlots);
        }
    }
}
