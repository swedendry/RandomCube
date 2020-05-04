using Extension;
using Network;
using Network.GameServer;
using Network.LobbyServer;
using System.Linq;
using UI;
using UnityEngine;

public class RecordGame : Game
{
    private string tempUserId;
    private string tempUserName;

    protected override void Start()
    {
        CretaeUsers();

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

        users.OrderByDescending(x => x.Life).ForEach((x, i) =>
        {
            x.Rank = i;
            x.Money = ServerDefine.Time2Money(ServerInfo.Room.ProgressTime);
        });

        GameServer.sInstance?.SendLocal("Result", new SC_Result
        {
            Users = users
        });
    }

    protected override void Result(Payloader<SC_Result> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GetTeams().ForEach(x => x.UnRegister());

                    ServerInfo.GameUsers = data.Users;
                    ServerInfo.GameUsers.FirstOrDefault().Id = tempUserId;
                    ServerInfo.GameUsers.FirstOrDefault().Name = tempUserName;
                    ServerInfo.User.Id = tempUserId;
                    ServerInfo.User.Name = tempUserName;

                    Router.Open("ResultView");
                });
    }

    private void CretaeUsers()
    {
        DummySetting();

        ServerInfo.GameUsers.Clear();

        tempUserId = ServerInfo.User.Id;
        tempUserName = ServerInfo.User.Name;
        ServerInfo.User.Id = "ai_0000";
        ServerInfo.User.Name = "ai_0000";
        ServerInfo.GameUsers.Add(ServerInfo.User.ToGameUser());
    }

    private void DummySetting()
    {   //Scene 바로 시작시
        if (!string.IsNullOrEmpty(ServerInfo.User.Id))
            return;

        MapperFactory.Register();
        XmlFactory.Load();

        gameObject.AddComponent<GameServer>();
        ServerInfo.User = DummyUser(SystemInfo.deviceUniqueIdentifier, SystemInfo.deviceName);
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
}