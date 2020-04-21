using Service.Databases.Sql.Models;
using System;
using System.Collections.Generic;

namespace Game.Contents
{
    public class BaseUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; set; }
    }

    public class MatchBaseUser : BaseUser
    {

    }

    public class MatchUser : MatchBaseUser
    {
        public DateTime MatchTime { get; set; }
    }

    public class RoomBaseUser : BaseUser
    {
        public EntryViewModel Entry { get; set; }
        public List<CubeViewModel> Cubes { get; set; }
    }

    public class RoomUser : RoomBaseUser
    {
        public int Life { get; set; }
        public float SP { get; set; }
    }
}
