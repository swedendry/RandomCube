using Extension;
using Network.GameServer;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Team : MonoBehaviour
{
    private readonly List<Cube> cubes = new List<Cube>();
    private readonly List<Monster> monsters = new List<Monster>();
    private readonly List<Missile> missiles = new List<Missile>();

    private GameUser user;
    private Zone zone;
    private Bounds bounds;
    private int cubeSeq;
    private int monsterSeq;

    public void Result()
    {
        //cubes.ForEach(x =>
        //{
        //    PoolFactory.Delete("Cube", x);
        //});

        //monsters.ForEach(x =>
        //{
        //    PoolFactory.Delete("Monster", x);
        //});

        //missiles.ForEach(x =>
        //{
        //    PoolFactory.Delete("Missile", x);
        //});

        cubes.Clear();
        monsters.Clear();
        missiles.Clear();

        //PoolFactory.UnRegister();
    }

    public void Create(GameUser user, Zone zone)
    {
        this.user = user;
        this.zone = zone;
        bounds = this.zone.box.GetComponent<BoxCollider>().bounds;
    }

    public void CreateCube()
    {
        user.SP -= 10;

        CreateCube(1);
    }

    public void CreateCube(byte combineLv)
    {
        var min = bounds.min;
        var max = bounds.max;
        var x = Random.Range(min.x, max.x);
        var y = Random.Range(min.y, max.y);

        CreateCube(combineLv, new Vector3(x, y, 0f));
    }

    public void CreateCube(GameCube gameCube)
    {
        var center = bounds.center;
        var positionX = center.x - (gameCube.PositionX * 0.01f);
        var positionY = center.y - (gameCube.PositionY * 0.01f);

        var gameSlot = user.Slots.Find(x => x.CubeId == gameCube.CubeId);
        var cube = PoolFactory.Get<Cube>("Cube");
        cube.transform.parent = transform;
        cube.transform.localPosition = new Vector3(positionX, positionY, 0f);
        cube.transform.localRotation = Quaternion.identity;
        cube.gameObject.SetActive(true);
        cube.OnShot = OnShot;
        cube.OnMove = OnMove;
        cube.OnCombineMove = OnCombineMove;
        cube.OnCombine = OnCombine;
        cube.Spawn(gameCube, gameSlot);
        cubes.Add(cube);
    }

    public void CreateCube(byte combineLv, Vector3 position)
    {
        var gameSlot = user.Slots.Random();
        var center = bounds.center;
        var positionX = (int)((position.x - center.x) * 100f);
        var positionY = (int)((position.y - center.y) * 100f);

        var gameCube = new GameCube()
        {
            CubeSeq = cubeSeq,
            CubeId = gameSlot.CubeId,
            CombineLv = combineLv,
            PositionX = positionX,
            PositionY = positionY,
        };

        var cube = PoolFactory.Get<Cube>("Cube");
        cube.transform.parent = transform;
        cube.transform.localPosition = position;
        cube.transform.localRotation = Quaternion.identity;
        cube.gameObject.SetActive(true);
        cube.OnShot = OnShot;
        cube.OnMove = OnMove;
        cube.OnCombineMove = OnCombineMove;
        cube.OnCombine = OnCombine;
        cube.Spawn(gameCube, gameSlot);
        cubes.Add(cube);

        if (user.Id == ServerInfo.User.Id)
            GameServer.sInstance.CreateCube(user.Id, gameCube);

        cubeSeq += 1;
    }

    public void MoveCube(int seq, int positionX, int positionY)
    {
        var cube = cubes.Find(x => x.gameCube.CubeSeq == seq);
        if (cube)
        {
            var center = bounds.center;
            var x = center.x - (positionX * 0.01f);
            var y = center.y - (positionY * 0.01f);

            cube.Move(new Vector3(x, y, 0f));
        }
    }

    public void CombineCube(GameCube gameCube, List<int> deleteCubes)
    {
        deleteCubes.ForEach(x =>
        {
            var cube = cubes.Find(c => c.gameCube.CubeSeq == x);
            cubes.Remove(cube);
            PoolFactory.Return("Cube", cube);
        });

        //CreateCube(gameCube);
    }

    public void DieMonster(int monsterSeq)
    {
        var monster = monsters.Find(x => x.seq == monsterSeq);
    }

    public void EscapeMonster(int monsterSeq)
    {
        var monster = monsters.Find(x => x.seq == monsterSeq);
    }

    public IEnumerator CreateMonster()
    {
        var paths = zone.paths;

        CreateMonster(paths);
        yield return new WaitForSeconds(1f);
        CreateMonster(paths);
        yield return new WaitForSeconds(1f);
        CreateMonster(paths);

        yield return new WaitForSeconds(10f);

        StartCoroutine(CreateMonster());
    }

    private void CreateMonster(List<GameObject> paths)
    {
        var startPosition = paths.FirstOrDefault().transform.position;
        var monster = PoolFactory.Get<Monster>("Monster");
        monster.transform.parent = transform;
        monster.transform.localPosition = Vector3.zero;
        monster.transform.position = new Vector3(startPosition.x, startPosition.y, 0f);
        monster.gameObject.SetActive(true);
        monster.OnDie = OnDie;
        monster.OnEscape = OnEscape;
        monster.Spawn(monsterSeq);
        monster.Move(paths);

        monsters.Add(monster);

        monsterSeq++;
    }

    private Monster GetShotTarget(Cube owner)
    {
        var center = owner.transform.position;
        var radius = (owner.gameCube.CombineLv * 1f);
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

    private void OnShot(Cube owner)
    {
        var target = GetShotTarget(owner);
        if (!target)
            return;

        var missile = PoolFactory.Get<Missile>("Missile");
        missile.transform.parent = transform;
        missile.transform.position = owner.transform.position;
        missile.gameObject.SetActive(true);
        missile.OnHit = OnHit;
        missile.Shot(owner, target);
        missiles.Add(missile);
    }

    private void OnMove(Cube owner, Vector3 target)
    {
        owner.Move(target);

        var center = bounds.center;
        var positionX = (int)((target.x - center.x) * 100f);
        var positionY = (int)((target.y - center.y) * 100f);

        if (user.Id == ServerInfo.User.Id)
            GameServer.sInstance.MoveCube(user.Id, owner.gameCube.CubeSeq, positionX, positionY);
    }

    private void OnCombineMove(Cube owner, Cube target)
    {
        owner.Combine(target);

        var center = bounds.center;
        var positionX = (int)((target.transform.position.x - center.x) * 100f);
        var positionY = (int)((target.transform.position.y - center.y) * 100f);

        if (user.Id == ServerInfo.User.Id)
            GameServer.sInstance.MoveCube(user.Id, owner.gameCube.CubeSeq, positionX, positionY);
    }

    private void OnCombine(Cube owner, Cube target)
    {
        var combineLv = owner.gameCube.CombineLv;
        var position = target.transform.position;

        var deleteSeq = new List<int>();
        deleteSeq.Add(owner.gameCube.CubeSeq);
        deleteSeq.Add(target.gameCube.CubeSeq);

        cubes.Remove(owner);
        PoolFactory.Return("Cube", owner);

        cubes.Remove(target);
        PoolFactory.Return("Cube", target);

        CreateCube((byte)(combineLv + 1), position);

        if (user.Id == ServerInfo.User.Id)
            GameServer.sInstance.CombineCube(user.Id, null, deleteSeq);
    }

    private void OnHit(Cube owner, Monster target, Missile collider)
    {
        owner.Hit(collider);
        target.Hit(owner, collider);

        missiles.Remove(collider);
        PoolFactory.Return("Missile", collider);
    }

    private void OnDie(Monster target, Missile collider)
    {
        user.SP += 10;

        //target.transform.position = zone.paths.LastOrDefault().transform.position;
        var seq = target.seq;

        monsters.Remove(target);
        PoolFactory.Return("Monster", target);

        if (user.Id == ServerInfo.User.Id)
            GameServer.sInstance.DieMonster(user.Id, seq);
    }

    private void OnEscape(Monster target)
    {
        user.Life -= 1;

        //target.transform.position = zone.paths.LastOrDefault().transform.position;
        var seq = target.seq;

        monsters.Remove(target);
        PoolFactory.Return("Monster", target);

        if (user.Id == ServerInfo.User.Id)
            GameServer.sInstance.EscapeMonster(user.Id, seq);
    }
}
