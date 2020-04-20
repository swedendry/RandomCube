using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameUser : MonoBehaviour
{
    private readonly List<Cube> cubes = new List<Cube>();
    private readonly List<Monster> monsters = new List<Monster>();
    private readonly List<Missile> missiles = new List<Missile>();

    public void Create(bool isMe, Zone zone)
    {
        var box = zone.box.GetComponent<BoxCollider>();
        var min = box.bounds.min;
        var max = box.bounds.max;
        var x = Random.Range(min.x, max.x);
        var y = Random.Range(min.y, max.y);
        var cube = PoolFactory.Get<Cube>("Cube");
        cube.transform.parent = transform;
        cube.transform.position = new Vector3(x, y, 0f);
        cube.gameObject.SetActive(true);
        cube.OnShot = OnShot;
        cubes.Add(cube);

        var paths = zone.paths;
        var monster = PoolFactory.Get<Monster>("Monster");
        monster.transform.parent = transform;
        monster.transform.position = paths[0].transform.position;
        monster.gameObject.SetActive(true);
        monster.Move(paths);
        monsters.Add(monster);
    }

    private void OnShot(Cube owner)
    {
        var target = monsters.FirstOrDefault();
        if (!target)
            return;

        var missile = PoolFactory.Get<Missile>("Missile");
        missile.transform.parent = transform;
        missile.transform.position = owner.transform.position;
        missile.gameObject.SetActive(true);
        missile.OnHit = Hit;
        missile.Shot(owner, target);
        missiles.Add(missile);
    }

    public void Hit(Cube owner, Monster target, Missile collider)
    {
        owner.Hit(collider);
        target.Hit(collider);

        missiles.Remove(collider);
        PoolFactory.Return("Missile", collider);
    }
}
