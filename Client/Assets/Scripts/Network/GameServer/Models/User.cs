using Network.LobbyServer;
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
        public EntryViewModel Entry { get; set; }
        public List<CubeViewModel> Cubes { get; set; }
    }

    public class GameUser : RoomUser
    {
        public int Life { get; set; }
        public float SP { get; set; }
    }
}
