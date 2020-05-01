//using Network;
//using Network.GameServer;

//public class VirtualGameServer : GameServer
//{
//    public override void CreateCube(string id, GameCube cube)
//    {
//        var sc = new SC_CreateCube()
//        {
//            Id = id,
//            NewCube = cube,
//        };

//        OnInvocation("CreateCube", PayloadPack.Success(sc));
//    }

//    public override void MoveCube(string id, int cubeSeq, int positionX, int positionY)
//    {
//        var sc = new SC_MoveCube()
//        {
//            Id = id,
//            CubeSeq = cubeSeq,
//            PositionX = positionX,
//            PositionY = positionY
//        };

//        OnInvocation("MoveCube", PayloadPack.Success(sc));
//    }
//}
