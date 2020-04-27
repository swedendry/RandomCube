using AutoMapper;
using GameServer.Hubs;
using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using Service.Services;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace GameServer.Contents
{
    public enum RoomState
    {
        Ready,
        Loading,
        Play,
        Result,
        End
    }

    public interface IRoom
    {
        void Release();

        bool Enter(RoomUser user);
        bool Exit(string id);

        void CSID_CompleteLoading(string id);

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
        private RoomState _state;
        private readonly List<GameUser> _users = new List<GameUser>();

        private readonly Timer _timer = new Timer();

        public Room(IHubContext<GameHub> context, IMapper mapper, string groupName)
        {
            _context = context;
            _mapper = mapper;
            _groupName = groupName;

            _timer = new Timer
            {
                Interval = 100,
                Enabled = true,
            };

            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            Reset();
        }

        public void Release()
        {
            _state = RoomState.End;
            _timer.Stop();
            _users.Clear();
        }

        private void Reset()
        {
            _state = RoomState.Ready;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            switch (_state)
            {
                case RoomState.Ready:
                    {
                        if (_users.Count >= ServerDefine.MAX_GAME_USER)
                        {
                            Start();
                        }
                    }
                    break;
                case RoomState.Loading:
                    {

                    }
                    break;
            }
        }

        public bool Enter(RoomUser user)
        {
            if (_state != RoomState.Ready)
                return false;

            if (GetUserById(user.Id) != null)
                return false;

            var newUser = _mapper.Map<GameUser>(user);

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

        private void Start()
        {
            Reset();

            SCID_Loading();
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

        private void SetUserState(string id, UserState state)
        {
            var user = GetUserById(id);
            if (user == null)
                return;

            user.State = state;
        }

        private void SetUserState(UserState state)
        {
            _users.ForEach(x => x.State = state);
        }

        private bool CheckUserState(UserState state)
        {
            return _users.TrueForAll(x => x.State == state);
        }



        private void SCID_Loading()
        {
            _state = RoomState.Loading;

            _context.Clients.Clients(_users.Select(x => x.ConnectionId).ToArray()).SendCoreAsync("Loading", PayloadPack.Success(new SC_Loading()
            {
                Users = _users,
            }));
        }

        public void CSID_CompleteLoading(string id)
        {
            if (_state != RoomState.Loading)
            {   //플레이 중에 로딩 완료가 오면 재접속
                return;
            }

            SetUserState(id, UserState.CompleteLoading);

            SCID_Play();
        }

        private void SCID_Play()
        {
            if (!CheckUserState(UserState.CompleteLoading))
                return; //아직 전부 로딩이 안되었음

            SetUserState(UserState.Play);

            _context.Clients.Clients(_users.Select(x => x.ConnectionId).ToArray()).SendCoreAsync("Play", PayloadPack.Success(new SC_Play()
            {

            }));

            _state = RoomState.Play;

            SCID_Wave();
        }

        private void SCID_Wave()
        {
            _context.Clients.Clients(_users.Select(x => x.ConnectionId).ToArray()).SendCoreAsync("Wave", PayloadPack.Success(new SC_Wave()
            {

            }));
        }

        public void CSID_CreateCube(string id)
        {
            _context.Clients.Clients(_users.Select(x => x.ConnectionId).ToArray()).SendCoreAsync("CreateCube", PayloadPack.Success(new SC_CreateCube()
            {

            }));
        }

        public void CSID_MoveCube(string id)
        {

        }
    }
}
