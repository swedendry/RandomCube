using System.Collections.Generic;
using UnityEngine;

public class GameUser : MonoBehaviour
{
    private readonly List<Cube> cubes = new List<Cube>();
    private readonly List<Monster> monsters = new List<Monster>();

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
        cubes.Add(cube);

        var paths = zone.paths;
        var monster = PoolFactory.Get<Monster>("Monster");
        monster.transform.parent = transform;
        monster.transform.position = paths[0].transform.position;
        monster.Move(paths);
        monsters.Add(monster);
    }
}
