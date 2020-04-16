using System.Collections.Generic;
using UnityEngine;

public class GameUser : MonoBehaviour
{
    private readonly List<Cube> cubes = new List<Cube>();
    private readonly List<Monster> monsters = new List<Monster>();

    public void Create()
    {
        var cube = PoolFactory.Get<Cube>("Cube");
        cubes.Add(cube);


    }
}
