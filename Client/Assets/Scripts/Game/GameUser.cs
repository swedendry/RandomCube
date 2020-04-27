//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class GameUser : MonoBehaviour
//{
//    private readonly List<Cube> cubes = new List<Cube>();
//    private readonly List<Monster> monsters = new List<Monster>();
//    private readonly List<Missile> missiles = new List<Missile>();

//    private Zone zone;
//    private GameUserViewModel gameUser;

//    public void Create(bool isMe, Zone zone)
//    {
//        gameUser = isMe ? ServerInfo.MyUser : ServerInfo.EnemyUser;
//        gameUser.SP = 100;
//        gameUser.Life = 3;

//        this.zone = zone;

//        StartCoroutine(CreateMonster());
//        //CreateMonster();
//    }

//    public void CreateCube()
//    {
//        gameUser.SP -= 10;

//        CreateCube(1);
//    }

//    public void CreateCube(int grade)
//    {
//        var box = zone.box.GetComponent<BoxCollider>();
//        var min = box.bounds.min;
//        var max = box.bounds.max;
//        var x = Random.Range(min.x, max.x);
//        var y = Random.Range(min.y, max.y);

//        CreateCube(grade, new Vector3(x, y, 0f));
//    }

//    public void CreateCube(int grade, Vector3 position)
//    {
//        var cube = PoolFactory.Get<Cube>("Cube");
//        cube.transform.parent = transform;
//        cube.transform.localPosition = position;
//        cube.transform.localRotation = Quaternion.identity;
//        cube.gameObject.SetActive(true);
//        cube.OnShot = OnShot;
//        cube.OnCombine = OnCombine;
//        cube.Spawn(grade);
//        cubes.Add(cube);
//    }

//    private IEnumerator CreateMonster()
//    {
//        var paths = zone.paths;

//        CreateMonster(paths);
//        yield return new WaitForSeconds(1f);
//        CreateMonster(paths);
//        yield return new WaitForSeconds(1f);
//        CreateMonster(paths);

//        yield return new WaitForSeconds(10f);

//        StartCoroutine(CreateMonster());
//    }

//    private void CreateMonster(List<GameObject> paths)
//    {
//        var startPosition = paths.FirstOrDefault().transform.position;
//        var monster = PoolFactory.Get<Monster>("Monster");
//        monster.transform.parent = transform;
//        monster.transform.localPosition = Vector3.zero;
//        monster.transform.position = new Vector3(startPosition.x, startPosition.y, 0f);
//        monster.gameObject.SetActive(true);
//        monster.OnDie = OnDie;
//        monster.OnFinish = OnFinish;
//        monster.Spawn();
//        monster.Move(paths);

//        monsters.Add(monster);
//    }

//    private void OnShot(Cube owner)
//    {
//        var target = monsters.FirstOrDefault();
//        if (!target)
//            return;

//        var missile = PoolFactory.Get<Missile>("Missile");
//        missile.transform.parent = transform;
//        missile.transform.position = owner.transform.position;
//        missile.gameObject.SetActive(true);
//        missile.OnHit = OnHit;
//        missile.Shot(owner, target);
//        missiles.Add(missile);
//    }

//    private void OnCombine(Cube owner, Cube target)
//    {
//        var grade = owner.grade;
//        var position = target.transform.position;

//        cubes.Remove(owner);
//        PoolFactory.Return("Cube", owner);

//        cubes.Remove(target);
//        PoolFactory.Return("Cube", target);

//        CreateCube(grade + 1, position);
//    }

//    private void OnHit(Cube owner, Monster target, Missile collider)
//    {
//        owner.Hit(collider);
//        target.Hit(owner, collider);

//        missiles.Remove(collider);
//        PoolFactory.Return("Missile", collider);
//    }

//    private void OnDie(Monster target, Missile collider)
//    {
//        gameUser.SP += 10;

//        //target.transform.position = zone.paths.LastOrDefault().transform.position;

//        monsters.Remove(target);
//        PoolFactory.Return("Monster", target);
//    }

//    private void OnFinish(Monster target)
//    {
//        gameUser.Life -= 1;

//        //target.transform.position = zone.paths.LastOrDefault().transform.position;

//        monsters.Remove(target);
//        PoolFactory.Return("Monster", target);
//    }
//}
