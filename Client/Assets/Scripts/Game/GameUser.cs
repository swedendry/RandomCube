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

        //if(isMe)
        //{
        //    cube = PoolFactory.Get<MyCube>("Cube");
        //}
        //else
        //{
        //    cube = PoolFactory.Get<EnemyCube>("Cube");
        //}

        cube.transform.parent = transform;
        cube.transform.localPosition = new Vector3(x, y, 0f);
        cubes.Add(cube);

        //var redMin = redBox.bounds.min;
        //var redMax = redBox.bounds.max;
        //var redX = Random.Range(redMin.x, redMax.x);
        //var redY = Random.Range(redMin.y, redMax.y);
        //var monster = PoolFactory.Get<Monster>("Monster");
        //monster.transform.parent = transform;
        //monster.transform.localPosition = redBox.bounds.max; //new Vector3(redX, redY, 0f);
        //monsters.Add(monster);
    }
}
