using AutoMapper;
using GameServer.Contents;
using GameServer.Hubs;
using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace GameServer.Services
{
    public interface IRoomService
    {
        bool Enter(string groupName, RoomUser user);
        bool Exit(string id);
        bool Delete(string groupName);

        IRoom GetRoomByGroupName(string groupName);
        IRoom GetRoomById(string id);
        IRoom GetRoomByConnectionId(string connectionId);
    }

    public class RoomService : IRoomService
    {
        private readonly IHubContext<GameHub> _context;
        private readonly IMapper _mapper;

        private readonly List<IRoom> _rooms = new List<IRoom>();
        private readonly object _lock = new object();

        public RoomService(IHubContext<GameHub> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool Enter(string groupName, RoomUser user)
        {
            lock (_lock)
            {
                var room = GetRoomByGroupName(groupName);
                if (room == null)
                {   //생성
                    room = new Room(_context, _mapper, groupName);
                    room.OnGameEnd = (groupName) => Delete(groupName);
                }

                _rooms.Add(room);

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

                var result = room.Exit(id);

                if (room.UserCount() <= 0)
                {   //유저가 아무도 없다 방 삭제
                    Delete(room.GroupName());
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

                room.Release();

                _rooms.Remove(room);

                return true;
            }
        }

        public IRoom GetRoomByGroupName(string groupName)
        {
            return _rooms.Find(p => p.GroupName() == groupName);
        }

        public IRoom GetRoomById(string id)
        {
            return _rooms.Find(p => p.GetUserById(id) != null);
        }

        public IRoom GetRoomByConnectionId(string connectionId)
        {
            return _rooms.Find(p => p.GetUserByConnectionId(connectionId) != null);
        }
    }
}
