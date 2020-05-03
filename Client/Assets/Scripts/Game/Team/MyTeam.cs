using Extension;
using Network.GameServer;
using System.Collections.Generic;
using UnityEngine;

public class MyTeam : Team
{
    public override void OnCreateCube(byte combineLv)
    {
        var min = bounds.min;
        var max = bounds.max;
        var x = Random.Range(min.x, max.x);
        var y = Random.Range(min.y, max.y);

        OnCreateCube(combineLv, new Vector3(x, y, 0f));
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

        GameServer.sInstance?.CreateCube(user.Id, gameCube);
    }

    public override void OnUpdateSlot(byte slotIndex)
    {
        var slot = user.Slots.Find(x => x.SlotIndex == slotIndex);
        var lv = slot.SlotLv + 1;

        GameServer.sInstance?.UpdateSlot(user.Id, slotIndex, (byte)lv);
    }

    protected override void OnMove(Cube owner, Vector3 target)
    {
        var serverPos = Local2Server(target);

        GameServer.sInstance?.MoveCube(user.Id, owner.gameCube.CubeSeq, (int)serverPos.x, (int)serverPos.y);
    }

    protected override void OnCombineMove(Cube owner, Cube target)
    {
        GameServer.sInstance?.CombineCube(user.Id, owner.gameCube.CubeSeq, target.gameCube.CubeSeq);
    }

    protected override void OnCombine(Cube owner, Cube target)
    {
        var combineLv = owner.gameCube.CombineLv;
        var position = target.transform.position;

        var deleteSeq = new List<int>();
        deleteSeq.Add(owner.gameCube.CubeSeq);
        deleteSeq.Add(target.gameCube.CubeSeq);

        GameServer.sInstance?.DeleteCube(user.Id, deleteSeq);

        OnCreateCube((byte)(combineLv + 1), position);
    }

    protected override void OnShot(Cube owner)
    {
        var target = GetShotTarget(owner);
        if (!target)
            return;

        GameServer.sInstance?.ShotMissile(user.Id, owner.gameCube.CubeSeq, target.seq);
    }

    protected override void OnDie(Monster target)
    {
        GameServer.sInstance?.DieMonster(user.Id, target.seq);
    }

    protected override void OnEscape(Monster target)
    {
        GameServer.sInstance?.EscapeMonster(user.Id, target.seq);
    }

    protected override Vector3 Server2Local(Vector3 server)
    {
        var center = bounds.center;
        var x = center.x + (server.x * 0.01f);
        var y = center.y + (server.y * 0.01f);
        return new Vector3(x, y, 0f);
    }
}
