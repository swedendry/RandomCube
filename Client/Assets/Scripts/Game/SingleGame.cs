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
        DummySetting();

        GameServer.sInstance.isLockSend = true;
        ServerInfo.Room.GameMode = GameMode.Single;

        base.Start();

        StartCoroutine(WaveMonster());
    }

    protected override void Loading()
    {
        teams[0].Register(ServerInfo.MyGameUser(), Map.blue);

        Router.CloseAndOpen("GameView");
    }

    protected override void CheckResult()
    {
        var users = ServerInfo.GameUsers;
        if (users.TrueForAll(x => x.Life > 0))
            return; //진행중

        users.OrderBy(x => x.Life).ForEach((x, i) =>
        {
            x.Rank = i;
            x.Money = ServerDefine.Time2Money(ServerInfo.Room.ProgressTime);
        });

        GameServer.sInstance?.SendLocal("Result", new SC_Result
        {
            Users = users
        });
    }

    private void DummySetting()
    {   //Scene 바로 시작시
        if (string.IsNullOrEmpty(ServerInfo.User.Id))
        {
            MapperFactory.Register();
            XmlFactory.Load();

            gameObject.AddComponent<GameServer>();
            ServerInfo.User = DummyUser(SystemInfo.deviceUniqueIdentifier, SystemInfo.deviceName);
        }

        ServerInfo.GameUsers.Clear();
        ServerInfo.GameUsers.Add(DummyGameUser(ServerInfo.User));
        //ServerInfo.GameUsers.Add(DummyGameUser(DummyUser("ai", "ai")));
    }

    private UserViewModel DummyUser(string id, string name)
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
            Id = id,
            Name = name,
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

        return new GameUser()
        {
            Id = user.Id,
            Name = user.Name,
            Slots = slots,
        };
    }
}
