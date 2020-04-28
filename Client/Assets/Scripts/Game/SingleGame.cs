using Extension;
using Network.GameServer;
using Network.LobbyServer;
using System.Linq;
using UI;
using UnityEngine;

public class SingleGame : Game
{
    protected override void Start()
    {
        Router.Close();

        MapperFactory.Register();
        XmlFactory.Load();

        ServerInfo.User = DummyUser();
        ServerInfo.GameUsers.Clear();
        ServerInfo.GameUsers.Add(DummyGameUser(ServerInfo.User));

        base.Start();

        StartCoroutine(blue.CreateMonster());
    }

    private UserViewModel DummyUser()
    {
        var allCubes = XmlKey.CubeData.FindAll<CubeDataXml.Data>();
        var cubes = allCubes.Random(ServerDefine.MAX_ENTRY_SLOT).Select((x, i) => new CubeViewModel()
        {
            CubeId = x.CubeId,
            CubeData = x.Map<CubeDataViewModel>(),
            Lv = 1,
            Parts = 0,
        }).ToList();

        var entry = new EntryViewModel()
        {
            Slots = cubes.Select(x => x.CubeId).ToArray(),
        };

        return new UserViewModel()
        {
            Id = SystemInfo.deviceUniqueIdentifier,
            Name = SystemInfo.deviceName,
            Money = 1000,
            Cubes = cubes,
            Entry = entry
        };
    }

    private GameUser DummyGameUser(UserViewModel user)
    {
        var slots = user.Entry.Slots.ToList().Select((x, i) =>
        {
            var cube = user.Cubes.Find(c => c.CubeId == x);

            return new GameSlot()
            {
                SlotIndex = (byte)i,
                CubeId = cube.CubeId,
                CubeLv = cube.Lv,
            };
        }).ToList();

        return DummyGameUser(new RoomUser()
        {
            Id = user.Id,
            Slots = slots,
        });
    }

    private GameUser DummyGameUser(RoomUser user)
    {
        return user.Map<GameUser>();
    }
}
