using Network.GameServer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Team : MonoBehaviour
{
    protected readonly List<Cube> cubes = new List<Cube>();
    protected readonly List<Monster> monsters = new List<Monster>();
    protected readonly List<Missile> missiles = new List<Missile>();

    public GameUser user;
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
        Debug.Log(string.Format("CreateCube {0}:{1}", user.Id, gameCube.CubeSeq));

        var position = Server2Local(new Vector3(gameCube.PositionX, gameCube.PositionY, 0f));
        var gameSlot = user.Slots.Find(x => x.CubeId == gameCube.CubeId);
        var cube = PoolFactory.Get<Cube>("Cube", position, Quaternion.identity, transform);
        cube.OnMove = OnMove;
        cube.OnCombineMove = OnCombineMove;
        cube.OnCombine = OnCombine;
        cube.OnShot = OnShot;
        cube.Spawn(user.Id, gameCube, gameSlot);
        cubes.Add(cube);

        user.Cubes.Add(gameCube);
        if (gameCube.CombineLv == 1)
            user.SP -= ServerDefine.CubeSeq2NeedSP(user.CubeSeq);

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
        Debug.Log(string.Format("CombineCube {0}:{1}:{2}", user.Id, ownerSeq, targetSeq));

        var owner = cubes.Find(x => x.gameCube.CubeSeq == ownerSeq);
        var target = cubes.Find(x => x.gameCube.CubeSeq == targetSeq);
        owner?.Combine(target);
    }

    public virtual void DeleteCube(List<int> deleteCubes)
    {
        Debug.Log(string.Format("DeleteCube {0}:{1}:{2}", user.Id, deleteCubes[0], deleteCubes[1]));

        deleteCubes.ForEach(x =>
        {
            var cube = cubes.Find(c => c.gameCube.CubeSeq == x);
            user.Cubes.Remove(cube.gameCube);
            cube.Release();
            cubes.Remove(cube);
            PoolFactory.Return("Cube", cube);
        });
    }

    public virtual void ShotMissile(int cubeSeq, int monsterSeq)
    {
        var cube = cubes.Find(x => x.gameCube.CubeSeq == cubeSeq);
        var monster = monsters.Find(x => x.seq == monsterSeq);
        if (cube == null || monster == null)
            return;

        var missile = PoolFactory.Get<Missile>("Missile", cube.transform.position, Quaternion.identity, transform);
        missile.OnHit = OnHit;
        missile.Spawn(cube, monster);
        missiles.Add(missile);

        cube.Shot(missile);
    }

    public virtual Monster CreateMonster()
    {
        var paths = zone.paths;
        var startPosition = zone.paths.FirstOrDefault().transform.position;
        var position = new Vector3(startPosition.x, startPosition.y, 0f);
        var monster = PoolFactory.Get<Monster>("Monster", position, Quaternion.identity, transform);
        monster.OnDie = OnDie;
        monster.OnEscape = OnEscape;
        monster.Spawn(user.MonsterSeq);
        monster.Move(paths);

        monsters.Add(monster);

        Debug.LogWarning(string.Format("CreateMonster {0}:{1}", user.Id, user.MonsterSeq));

        user.MonsterSeq++;

        return monster;
    }

    public virtual void DieMonster(int monsterSeq)
    {
        var monster = monsters.Find(x => x.seq == monsterSeq);
        monster.Release();
        Debug.LogWarning(string.Format("DieMonster {0}:{1}", user.Id, monsterSeq));
        monsters.Remove(monster);
        PoolFactory.Return("Monster", monster);

        user.SP += ServerDefine.MONSTER_DIE_SP;
    }

    public virtual void EscapeMonster(int monsterSeq)
    {
        var monster = monsters.Find(x => x.seq == monsterSeq);
        monster.Release();
        Debug.LogWarning(string.Format("EscapeMonster {0}:{1}", user.Id, monsterSeq));
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

    protected virtual void OnMove(Cube owner, Vector3 target)
    {

    }

    protected virtual void OnCombineMove(Cube owner, Cube target)
    {

    }

    protected virtual void OnCombine(Cube owner, Cube target)
    {

    }

    protected virtual void OnDie(Monster target)
    {

    }

    protected virtual void OnEscape(Monster target)
    {

    }

    protected virtual void OnShot(Cube owner)
    {

    }

    protected virtual void OnHit(Cube owner, Monster target, Missile missile)
    {
        owner.Hit(missile);
        target.Hit(owner, missile);

        missiles.Remove(missile);
        PoolFactory.Return("Missile", missile);
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
