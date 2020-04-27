using System;
using System.Collections.Generic;

namespace GameServer.Models
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
        public List<GameCube> Cubes { get; set; }
    }

    public class GameCube
    {
        public byte SlotIndex { get; set; }
        public int CubeId { get; set; }
        public byte Lv { get; set; }
        public byte GameLv { get; set; } = 1;
    }

    public class GameUser : RoomUser
    {
        public UserState State { get; set; }
        public int Life { get; set; } = 3;
        public float SP { get; set; } = 100;
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
