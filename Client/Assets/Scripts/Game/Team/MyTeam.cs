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

    public override Cube CreateCube(GameCube gameCube)
    {
        var cube = base.CreateCube(gameCube);
        cube.OnMove = OnMove;
        cube.OnCombineMove = OnCombineMove;
        cube.OnCombine = OnCombine;

        return cube;
    }

    public override Monster CreateMonster()
    {
        var monster = base.CreateMonster();
        monster.OnDie = OnDie;
        monster.OnEscape = OnEscape;

        return monster;
    }

    protected void OnMove(Cube owner, Vector3 target)
    {
        //owner.Move(target);

        var serverPos = Local2Server(target);

        GameServer.sInstance?.MoveCube(user.Id, owner.gameCube.CubeSeq, (int)serverPos.x, (int)serverPos.y);
    }

    protected void OnCombineMove(Cube owner, Cube target)
    {
        //owner.Combine(target);

        GameServer.sInstance?.CombineCube(user.Id, owner.gameCube.CubeSeq, target.gameCube.CubeSeq);

        //var center = bounds.center;
        //var positionX = (int)((target.transform.position.x - center.x) * 100f);
        //var positionY = (int)((target.transform.position.y - center.y) * 100f);

        //GameServer.sInstance?.CombineMoveCube(user.Id, owner.gameCube.CubeSeq, positionX, positionY);
    }

    protected void OnCombine(Cube owner, Cube target)
    {
        var combineLv = owner.gameCube.CombineLv;
        var position = target.transform.position;

        var deleteSeq = new List<int>();
        deleteSeq.Add(owner.gameCube.CubeSeq);
        deleteSeq.Add(target.gameCube.CubeSeq);

        //cubes.Remove(owner);
        //PoolFactory.Return("Cube", owner);

        //cubes.Remove(target);
        //PoolFactory.Return("Cube", target);

        GameServer.sInstance?.DeleteCube(user.Id, deleteSeq);

        OnCreateCube((byte)(combineLv + 1), position);
    }

    protected void OnDie(Monster target, Missile collider)
    {
        var seq = target.seq;

        //monsters.Remove(target);
        //PoolFactory.Return("Monster", target);

        GameServer.sInstance?.DieMonster(user.Id, seq);
    }

    protected void OnEscape(Monster target)
    {
        var seq = target.seq;

        //monsters.Remove(target);
        //PoolFactory.Return("Monster", target);

        GameServer.sInstance?.EscapeMonster(user.Id, seq);
    }

    protected override Vector3 Server2Local(Vector3 server)
    {
        var center = bounds.center;
        var x = center.x + (server.x * 0.01f);
        var y = center.y + (server.y * 0.01f);
        return new Vector3(x, y, 0f);
    }
}
