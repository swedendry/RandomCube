using Extension;
using Network.GameServer;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class RecordPack
{
    public float Time { get; set; }
    public string Method { get; set; }
    public string Args { get; set; }
}

public class Record
{
    public RoomUser User { get; set; }
    public Queue<RecordPack> Packs = new Queue<RecordPack>();
}

public class RecordTeam : InputTeam
{
    private Record record = new Record();

    private void AddPack(string method, object args)
    {
        var progressTime = ServerInfo.Room.ProgressTime;

        record.Packs.Enqueue(new RecordPack()
        {
            Time = progressTime,
            Method = method,
            Args = JsonConvert.SerializeObject(args),
        });
    }

    public override void Register(GameUser user, Zone zone)
    {
        base.Register(user, zone);

        record.User = user.Map<RoomUser>();
    }

    public override void UnRegister()
    {
        base.UnRegister();

        XmlFactory.Save(XmlKey.RecordData.ToString(), ServerInfo.Room.ProgressTime, record);

        record.Packs.Clear();
    }

    public override void OnCreateCube(byte combineLv, Vector3 position)
    {
        var gameSlot = user.Slots.Random();
        var serverPos = Local2Server(position);

        var gameCube = new GameCube()
        {
            CubeSeq = user.CubeSeq,
            CubeId = gameSlot.CubeId,
            CombineLv = combineLv,
            PositionX = (int)serverPos.x,
            PositionY = (int)serverPos.y,
        };

        var pair = GameServer.sInstance?.CreateCube(user.Id, gameCube);
        AddPack(pair.Value.Key, pair.Value.Value);
    }

    public override void OnUpdateSlot(byte slotIndex)
    {
        var slot = user.Slots.Find(x => x.SlotIndex == slotIndex);
        var lv = slot.SlotLv + 1;

        var pair = GameServer.sInstance?.UpdateSlot(user.Id, slotIndex, (byte)lv);
        AddPack(pair.Value.Key, pair.Value.Value);
    }

    protected override void OnMove(Cube owner, Vector3 target)
    {
        var serverPos = Local2Server(target);

        var pair = GameServer.sInstance?.MoveCube(user.Id, owner.gameCube.CubeSeq, (int)serverPos.x, (int)serverPos.y);
        AddPack(pair.Value.Key, pair.Value.Value);
    }

    protected override void OnCombineMove(Cube owner, Cube target)
    {
        var pair = GameServer.sInstance?.CombineCube(user.Id, owner.gameCube.CubeSeq, target.gameCube.CubeSeq);
        AddPack(pair.Value.Key, pair.Value.Value);
    }

    protected override void OnCombine(Cube owner, Cube target)
    {
        var combineLv = owner.gameCube.CombineLv;
        var position = target.transform.position;

        var deleteSeq = new List<int>();
        deleteSeq.Add(owner.gameCube.CubeSeq);
        deleteSeq.Add(target.gameCube.CubeSeq);

        var pair = GameServer.sInstance?.DeleteCube(user.Id, deleteSeq);
        AddPack(pair.Value.Key, pair.Value.Value);

        OnCreateCube((byte)(combineLv + 1), position);
    }
}
