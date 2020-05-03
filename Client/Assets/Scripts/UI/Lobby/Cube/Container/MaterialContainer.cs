using Extension;
using Network.LobbyServer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Cube
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

        public override void Upsert()
        {
            base.Upsert();

            var cubes = ServerInfo.User.Cubes;
            var cubeDatas = XmlKey.CubeData.FindAll<CubeDataXml.Data>();

            var allCubes = cubeDatas.Select(x =>
            {
                var cube = cubes.Find(c => c.CubeId == x.CubeId);
                if (cube == null)
                {   //생성
                    cube = new CubeViewModel()
                    {
                        CubeId = x.CubeId,
                        Lv = 0,
                        Parts = 0,
                        CubeData = x.Map<CubeDataViewModel>(),
                    };
                }
                return cube;
            }).OrderBy(x => x.CubeId);

            allCubes.ForEach((x, i) =>
            {
                if (i >= slotComponents.Count)
                    Create();   //슬롯생성

                slotComponents[i]?.Upsert(i, x);
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