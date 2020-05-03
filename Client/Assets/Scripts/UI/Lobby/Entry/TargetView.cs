using Extension;
using Network.LobbyServer;
using System.Collections.Generic;

namespace UI.Entry
{
    public class TargetView : UIView<SlotComponent, CubeViewModel>
    {
        private List<SlotComponent> slotComponents;

        protected override void Awake()
        {
            base.Awake();

            slotComponents = GetUIComponents<SlotComponent>();
        }

        public void Upsert(List<int> slots)
        {
            base.Empty();

            var cubes = ServerInfo.User.Cubes;

            slots.ForEach((x, i) =>
            {
                var cube = cubes.Find(c => c.CubeId == x);
                slotComponents[i]?.Upsert(i, cube);
            });
        }

        public override void Event(bool isSelected, Props<CubeViewModel> props)
        {
            if (isSelected)
            {
                selectProps.Add(props);
            }
            else
            {
                selectProps.Remove(props);
            }

            slotComponents.ForEach(x =>
            {
                x.Selected(selectProps.Contains(x.props));
            });

            base.Event(isSelected, props);
        }
    }
}