using Extension;
using Network.LobbyServer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Entry
{
    public class MaterialContainer : UIContainer<SlotComponent, CubeViewModel>
    {
        public Transform slotParent;
        public SlotComponent slotDummy;

        private List<SlotComponent> slotComponents;

        protected override void Awake()
        {
            base.Awake();

            slotComponents = GetUIComponents<SlotComponent>();
        }

        protected override void Create()
        {
            var obj = Instantiate(slotDummy, slotParent) as SlotComponent;
            obj.OnEventProps = Event;
            slotComponents.Add(obj);
        }

        public void Upsert(List<int> slots)
        {
            base.Upsert();

            var cubes = ServerInfo.User.Cubes.Select(x => new
            {
                Cube = x,
                IsLock = slots.Contains(x.CubeId),
            }).OrderBy(x => x.IsLock).ThenBy(x => x.Cube.CubeId);

            cubes.ForEach((x, i) =>
            {
                if (i >= slotComponents.Count)
                    Create();   //슬롯생성

                slotComponents[i]?.Upsert(i, x.Cube);
                slotComponents[i]?.Lock(x.IsLock);
            });
        }

        public override void Event(bool isSelected, Props<CubeViewModel> props)
        {
            selectProps.Clear();

            if (isSelected)
                selectProps.Add(props);

            slotComponents.ForEach(x =>
            {
                x.Selected(selectProps.Contains(x.props));
            });

            base.Event(isSelected, props);
        }
    }
}