using System;
using System.Collections.Generic;

namespace Network.GameServer
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
    }

    public class MatchUser : User
    {
        public DateTime MatchTime { get; set; }
    }

    public class RoomUser : User
    {
        public List<GameSlot> Slots { get; set; }
    }

    public class GameSlot
    {
        public byte SlotIndex { get; set; }
        public byte SlotLv { get; set; } = 1;
        public int CubeId { get; set; }
        public byte CubeLv { get; set; }
    }

    public class GameCube
    {
        public int CubeSeq { get; set; }
        public int CubeId { get; set; }
        public byte CombineLv { get; set; } = 1;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }

    public class GameUser : RoomUser
    {
        public UserState State { get; set; } = UserState.Wait;
        public int Life { get; set; } = 3;
        public float SP { get; set; } = 100;
        public int Rank { get; set; } = 1;
        public int Money { get; set; }
        public int CubeSeq { get; set; } = 0;
        public int MonsterSeq { get; set; } = 0;
        public List<GameCube> Cubes { get; set; } = new List<GameCube>();
    }

    public enum UserState
    {
        Wait = 0,                   //대기중 
        Disconnected,               //접속 끊김
        CompleteLoading,	        //로딩 완료
        Play,			            //플레이 중
        EndWave,                    //웨이브 종료
        End,                        //종료
    }
}
