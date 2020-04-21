using Game.Extensions;
using Game.Hubs;
using Game.Models;
using Microsoft.AspNetCore.SignalR;
using Service.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Game.Contents
{
    public enum RoomState
    {
        Wait = 0,   //대기중
        Loading,    //로딩
        Play,       //게임 중
        Result,     //게임 결과
        End,        //게임 종료
    }

    public enum ExitId
    {
        Default,
        DisConnected,
        SyncTimeOut,
        LoadingTimeOut,
    }

    public class BaseRoom
    {
        public RoomState state { get; set; }
        public string groupName { get; set; }
    }

    public class Room : BaseRoom
    {
        private readonly IHubContext<GameHub> _context;

        private readonly List<RoomUser> _users = new List<RoomUser>();
        private Timer _timer = new Timer();

        public Room(IHubContext<GameHub> context, string groupName)
        {
            _context = context;
            this.groupName = groupName;
            Reset();
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            switch (state)
            {
                case RoomState.Loading:
                    {

                    }
                    break;
            }
        }

        public bool Enter(RoomBaseUser user)
        {
            if (state != RoomState.Wait)
                return false;

            if (GetUserById(user.Id) != null)
                return false;

            _users.Add(new RoomUser()
            {
                Id = user.Id,
                Name = user.Name,
                ConnectionId = user.ConnectionId,
                Entry = user.Entry,
                Cubes = user.Cubes,
                Life = 3,
                SP = 100,
            });

            //_context.Client(user.ConnectionId, "EnterRoom", PayloadPack.Success(new SC_EnterRoom()
            //{
            //    //Id = roomUser.Id,
            //}));

            _context.Clients.Client(user.ConnectionId).SendAsync("EnterRoom", PayloadPack.Success(new SC_EnterRoom()
            {
                //Id = roomUser.Id,
            }));

            if (_users.Count == 2)
            {   //시작
                Start();
            }

            return true;
        }

        public bool Exit(string id, ExitId exitId)
        {
            var user = GetUserById(id);
            if (user == null)
                return false;

            _context.Clients.Clients(_users.Select(x => x.ConnectionId).ToList()).SendAsync("ExitRoom", PayloadPack.Success(new SC_ExitRoom()
            {
                Id = user.Id
            }));

            _users.Remove(user);

            return true;
        }

        private async Task SendClient(string connectionId, string method, object arg1)
        {

        }

        private void SendClients(string method, object arg1)
        {
            _context.Clients.Clients(_users.Select(x => x.ConnectionId).ToList()).SendAsync(method, arg1);
        }

        public void Delete()
        {
            SendClients("DeleteRoom", PayloadPack.Success(new SC_DeleteRoom()
            {

            }));
            //_context.Clients.Clients(_users.Select(x => x.ConnectionId).ToList()).SendAsync("DeleteRoom", PayloadPack.Success(new SC_DeleteRoom()
            //{

            //}));

            state = RoomState.End;
            _timer.Close();
            _users.Clear();
        }

        private void Start()
        {
            Reset();

            SCID_Start();

            state = RoomState.Loading;

            _timer = new Timer
            {
                Interval = 100,
                Enabled = true,
            };

            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        }

        public int GetUserSize()
        {
            return _users.Count;
        }

        public RoomUser GetUserById(string id)
        {
            return _users.Find(p => p.Id == id);
        }

        public RoomUser GetUserByConnectionId(string connectionId)
        {
            return _users.Find(p => p.ConnectionId == connectionId);
        }

        private void Reset()
        {
            state = RoomState.Wait;
        }

        private void SCID_Start()
        {

        }

        public void CSID_Loading(string connectionId, object args)
        {

        }

        private void SCID_Play()
        {
            state = RoomState.Play;
        }

        private void SCID_Round()
        {

        }
    }
}
