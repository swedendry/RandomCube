using System.Collections.Generic;
using UnityEngine;

public enum TeamId
{
    Red,
    Blue
}

public class Team : MonoBehaviour
{
    private readonly List<Cube> cubes = new List<Cube>();
    private readonly List<Monster> monsters = new List<Monster>();

    public void Create(TeamId teamId)
    {
        //var cube = PoolManager.Instance.Get<Cube>("Cube");
        //cubes.Add(cube);

        
    }
}
