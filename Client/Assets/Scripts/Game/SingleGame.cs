using Extension;
using Network.GameServer;
using Newtonsoft.Json;
using System.Linq;
using UI;

public class SingleGame : Game
{
    private Record record;

    protected override void Start()
    {
        CretaeUsers();

        GameServer.sInstance.isLockSend = true;
        ServerInfo.Room.GameMode = GameMode.Single;

        base.Start();

        StartCoroutine(WaveMonster(3f));
    }

    protected override void Loading()
    {
        teams[0].Register(ServerInfo.MyGameUser(), Map.blue);
        teams[1].Register(ServerInfo.EnemyGameUser(), Map.red, record);

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
            x.Money = ServerDefine.Rank2Money(i);
        });

        GameServer.sInstance?.SendLocal("Result", new SC_Result
        {
            Users = users
        });
    }

    private void CretaeUsers()
    {
        var recordDatas = XmlKey.RecordData.FindAll<RecordDataXml.Data>();
        var recordData = recordDatas.Random();
        record = JsonConvert.DeserializeObject<Record>(recordData.Pack);

        var aiUser = record.User.Map<GameUser>();
        aiUser.Name = "ai_" + recordData.Index;
        aiUser.Slots.ForEach(x => x.SlotLv = 1);

        ServerInfo.GameUsers.Clear();
        ServerInfo.GameUsers.Add(ServerInfo.User.ToGameUser());
        ServerInfo.GameUsers.Add(aiUser);
    }
}
