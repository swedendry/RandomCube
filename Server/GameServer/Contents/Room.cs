using AutoMapper;
using GameServer.Hubs;
using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Timers;

namespace GameServer.Contents
{
    public enum RoomState
    {
        Wait = 0,   //대기중
        Loading,    //로딩
        Play,       //게임 중
        Result,     //게임 결과
        End,        //게임 종료
    }

    public interface IRoom
    {
        void Release();

        bool Enter(RoomUser user);
        bool Exit(string id);

        string GroupName();
        int UserCount();
        GameUser GetUserById(string id);
        GameUser GetUserByConnectionId(string connectionId);
    }

    public class Room : IRoom
    {
        private readonly IHubContext<GameHub> _context;
        private readonly IMapper _mapper;
        private readonly string _groupName;
        private GameState _state;
        private readonly List<GameUser> _users = new List<GameUser>();

        private Timer _timer = new Timer();

        public Room(IHubContext<GameHub> context, IMapper mapper, string groupName)
        {
            _context = context;
            _mapper = mapper;
            _groupName = groupName;

            Reset();
        }

        public void Release()
        {
            _state = GameState.End;
            _timer.Stop();
            _users.Clear();
        }

        private void Reset()
        {
            _state = GameState.Wait;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            switch (_state)
            {
                case GameState.Loading:
                    {

                    }
                    break;
            }
        }

        public bool Enter(RoomUser user)
        {
            if (_state != GameState.Wait)
                return false;

            if (GetUserById(user.Id) != null)
                return false;

            var newUser = _mapper.Map<GameUser>(user);
            newUser.Life = 3;
            newUser.SP = 100;

            _users.Add(newUser);

            return true;
        }

        public bool Exit(string id)
        {
            var user = GetUserById(id);
            if (user == null)
                return false;

            _users.Remove(user);

            return true;
        }

        public void Start()
        {
            Reset();

            _state = GameState.Loading;

            _timer = new Timer
            {
                Interval = 100,
                Enabled = true,
            };

            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        }

        public string GroupName()
        {
            return _groupName;
        }

        public int UserCount()
        {
            return _users.Count;
        }

        public GameUser GetUserById(string id)
        {
            return _users.Find(p => p.Id == id);
        }

        public GameUser GetUserByConnectionId(string connectionId)
        {
            return _users.Find(p => p.ConnectionId == connectionId);
        }
    }
}
