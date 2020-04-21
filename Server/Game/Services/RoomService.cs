using Game.Contents;
using Game.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace Game.Services
{
    public interface IRoomService
    {
        bool Enter(string groupName, RoomBaseUser user);
        bool Exit(string id);
        bool Delete(string groupName);
    }

    public class RoomService : IRoomService
    {
        private readonly IHubContext<GameHub> _context;

        private readonly List<Room> _rooms = new List<Room>();
        private readonly object _lock = new object();

        public RoomService(IHubContext<GameHub> context)
        {
            _context = context;
        }

        public bool Enter(string groupName, RoomBaseUser user)
        {
            lock (_lock)
            {
                var room = GetRoomByGroupName(groupName);
                if (room == null)
                {   //생성
                    room = new Room(_context, groupName);
                }

                return room.Enter(user);
            }
        }

        public bool Exit(string id)
        {
            lock (_lock)
            {
                var room = GetRoomById(id);
                if (room == null)
                    return false;

                var result = room.Exit(id, ExitId.Default);

                if (room.GetUserSize() <= 0)
                {   //유저가 아무도 없다 방 삭제
                    Delete(room.groupName);
                }

                return result;
            }
        }

        public bool Delete(string groupName)
        {
            lock (_lock)
            {
                var room = GetRoomByGroupName(groupName);
                if (room == null)
                    return false;

                room.Delete();
                _rooms.Remove(room);

                return true;
            }
        }

        private Room GetRoomByGroupName(string groupName)
        {
            return _rooms.Find(p => p.groupName == groupName);
        }

        private Room GetRoomById(string id)
        {
            return _rooms.Find(p => p.GetUserById(id) != null);
        }

        private Room GetRoomByConnectionId(string connectionId)
        {
            return _rooms.Find(p => p.GetUserByConnectionId(connectionId) != null);
        }
    }
}
