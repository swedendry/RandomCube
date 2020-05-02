using Network.GameServer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//public interface ITeam
//{
//    void Register(GameUser user, Zone zone);
//    void UnRegister();

//    void CreateCube(byte combineLv);
//    void CreateCube(byte combineLv, Vector3 position);
//    void CreateCube(GameCube gameCube);
//    void MoveCube(int seq, int positionX, int positionY);
//    void CombineCube(GameCube gameCube, List<int> deleteCubes);

//    void DieMonster(int monsterSeq);
//    void EscapeMonster(int monsterSeq);
//    Monster CreateMonster();
//}

public class Team : MonoBehaviour
{
    protected readonly List<Cube> cubes = new List<Cube>();
    protected readonly List<Monster> monsters = new List<Monster>();
    protected readonly List<Missile> missiles = new List<Missile>();

    protected GameUser user;
    protected Zone zone;
    protected Bounds bounds;

    public void Register(GameUser user, Zone zone)
    {
        this.user = user;
        this.zone = zone;
        bounds = this.zone.box.GetComponent<BoxCollider>().bounds;
    }

    public void UnRegister()
    {
        cubes.Clear();
        monsters.Clear();
        missiles.Clear();
    }

    public virtual Cube CreateCube(GameCube gameCube)
    {
        var position = Server2Local(new Vector3(gameCube.PositionX, gameCube.PositionY, 0f));
        var gameSlot = user.Slots.Find(x => x.CubeId == gameCube.CubeId);
        var cube = PoolFactory.Get<Cube>("Cube", position, Quaternion.identity, transform);
        cube.OnShot = OnShot;
        cube.Spawn(gameCube, gameSlot);
        cubes.Add(cube);

        if (gameCube.CombineLv == 1)
            user.SP -= ServerDefine.Seq2NeedSP(user.CubeSeq);

        user.CubeSeq++;

        return cube;
    }

    public virtual void MoveCube(int seq, int positionX, int positionY)
    {
        var cube = cubes.Find(x => x.gameCube.CubeSeq == seq);
        var localPos = Server2Local(new Vector3(positionX, positionY, 0f));

        cube?.Move(new Vector3(localPos.x, localPos.y, 0f));
    }

    public virtual void CombineCube(int ownerSeq, int targetSeq)
    {
        var owner = cubes.Find(x => x.gameCube.CubeSeq == ownerSeq);
        var target = cubes.Find(x => x.gameCube.CubeSeq == targetSeq);
        owner?.Combine(target);
    }

    public virtual void DeleteCube(List<int> deleteCubes)
    {
        deleteCubes.ForEach(x =>
        {
            var cube = cubes.Find(c => c.gameCube.CubeSeq == x);
            cubes.Remove(cube);
            PoolFactory.Return("Cube", cube);
        });
    }

    public virtual Monster CreateMonster()
    {
        var paths = zone.paths;
        var startPosition = zone.paths.FirstOrDefault().transform.position;
        var position = new Vector3(startPosition.x, startPosition.y, 0f);
        var monster = PoolFactory.Get<Monster>("Monster", position, Quaternion.identity, transform);
        monster.Spawn(user.MonsterSeq);
        monster.Move(paths);

        monsters.Add(monster);

        user.MonsterSeq++;

        return monster;
    }

    public virtual void DieMonster(int monsterSeq)
    {
        var monster = monsters.Find(x => x.seq == monsterSeq);
        monsters.Remove(monster);
        PoolFactory.Return("Monster", monster);

        user.SP += ServerDefine.MONSTER_DIE_SP;
    }

    public virtual void EscapeMonster(int monsterSeq)
    {
        var monster = monsters.Find(x => x.seq == monsterSeq);
        monsters.Remove(monster);
        PoolFactory.Return("Monster", monster);

        user.Life -= 1;
    }

    public virtual void UpdateSlot(byte index, byte lv)
    {
        var slot = user.Slots.Find(x => x.SlotIndex == index);
        user.SP -= ServerDefine.SlotLv2Price(slot.SlotLv);
        slot.SlotLv = lv;
    }

    public virtual void OnCreateCube(byte combineLv)
    {

    }

    public virtual void OnCreateCube(byte combineLv, Vector3 position)
    {

    }

    public virtual void OnUpdateSlot(byte slotIndex)
    {

    }

    protected Monster GetShotTarget(Cube owner)
    {
        var center = owner.transform.position;
        var radius = owner.gameCube.CombineLv * 1f;
        var targets = new List<Monster>();
        monsters.ForEach(x =>
        {
            var target = x.transform.position;
            var distance = Vector3.Distance(center, target);
            if (distance <= radius)
                targets.Add(x);
        });

        return targets.FirstOrDefault();
    }

    protected void OnShot(Cube owner)
    {
        var target = GetShotTarget(owner);
        if (!target)
            return;

        var missile = PoolFactory.Get<Missile>("Missile", owner.transform.position, Quaternion.identity, transform);
        missile.OnHit = OnHit;
        missile.Spawn(owner, target);
        missiles.Add(missile);
    }

    protected void OnHit(Cube owner, Monster target, Missile collider)
    {
        owner.Hit(collider);
        target.Hit(owner, collider);

        missiles.Remove(collider);
        PoolFactory.Return("Missile", collider);
    }

    protected virtual Vector3 Local2Server(Vector3 local)
    {
        var center = bounds.center;
        var x = (local.x - center.x) * 100f;
        var y = (local.y - center.y) * 100f;
        return new Vector3(x, y, 0f);
    }

    protected virtual Vector3 Server2Local(Vector3 server)
    {
        var center = bounds.center;
        var x = center.x - (server.x * 0.01f);
        var y = center.y - (server.y * 0.01f);
        return new Vector3(x, y, 0f);
    }
}
