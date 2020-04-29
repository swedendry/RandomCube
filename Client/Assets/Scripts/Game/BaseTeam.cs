//using Network.GameServer;
//using System.Collections.Generic;
//using UnityEngine;

//public class BaseTeam : MonoBehaviour
//{
//    private readonly List<Cube> cubes = new List<Cube>();
//    private readonly List<Monster> monsters = new List<Monster>();
//    private readonly List<Missile> missiles = new List<Missile>();

//    private GameUser user;
//    private Zone zone;
//    private Bounds bounds;

//    public void Create(GameUser user, Zone zone)
//    {
//        this.user = user;
//        this.zone = zone;
//        bounds = this.zone.box.GetComponent<BoxCollider>().bounds;
//    }

//    public void CreateCube(GameCube gameCube)
//    {
//        var center = bounds.center;
//        var positionX = center.x - (gameCube.PositionX * 0.01f);
//        var positionY = center.y - (gameCube.PositionY * 0.01f);

//        var gameSlot = user.Slots.Find(x => x.CubeId == gameCube.CubeId);

//        var cube = PoolFactory.Get<Cube>("Cube");
//        cube.transform.parent = transform;
//        cube.transform.localPosition = new Vector3(positionX, positionY, 0f);
//        cube.transform.localRotation = Quaternion.identity;
//        cube.gameObject.SetActive(true);
//        cube.OnShot = OnShot;
//        cube.OnMove = OnMove;
//        cube.OnCombineMove = OnCombineMove;
//        cube.OnCombine = OnCombine;
//        cube.Spawn(gameCube, gameSlot);
//        cubes.Add(cube);
//    }
//}
