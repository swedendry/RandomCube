//using GameServer.Hubs;
//using GameServer.Models;
//using Microsoft.AspNetCore.SignalR;
//using System.Collections.Generic;
//using System.Timers;

//namespace GameServer.Contents
//{
//    public enum GameState
//    {
//        Wait = 0,   //대기중
//        Loading,    //로딩
//        Play,       //게임 중
//        Result,     //게임 결과
//        End,        //게임 종료
//    }

//    public interface IGame
//    {
//        bool Enter(RoomUser user);
//        bool Exit(string id);
//    }

//    public class Game : IGame
//    {
//        private readonly IHubContext<GameHub> _context;
//        private readonly string _groupName;
//        private GameState _state;
//        private readonly List<GameUser> _users = new List<GameUser>();

//        private Timer _timer = new Timer();

//        public Game(IHubContext<GameHub> context, string groupName)
//        {
//            _context = context;
//            _groupName = groupName;

//            Reset();
//        }

//        private void Release()
//        {
//            _state = GameState.End;
//            _timer.Stop();
//            _users.Clear();
//        }

//        private void Reset()
//        {
//            _state = GameState.Wait;
//        }

//        private void OnTimedEvent(object source, ElapsedEventArgs e)
//        {
//            switch (_state)
//            {
//                case GameState.Loading:
//                    {

//                    }
//                    break;
//            }
//        }

//        public bool Enter(RoomUser user)
//        {
//            if (_state != GameState.Wait)
//                return false;

//            if (GetUserById(user.Id) != null)
//                return false;

//            _users.Add(new GameUser()
//            {
//                Id = user.Id,
//                Name = user.Name,
//                ConnectionId = user.ConnectionId,
//                Slots = user.Slots,
//            });

//            return true;
//        }

//        public bool Exit(string id)
//        {
//            var user = GetUserById(id);
//            if (user == null)
//                return false;

//            _users.Remove(user);

//            return true;
//        }

//        public void Start()
//        {
//            Reset();

//            _state = GameState.Loading;

//            _timer = new Timer
//            {
//                Interval = 100,
//                Enabled = true,
//            };

//            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
//        }

//        private GameUser GetUserById(string id)
//        {
//            return _users.Find(p => p.Id == id);
//        }

//        private GameUser GetUserByConnectionId(string connectionId)
//        {
//            return _users.Find(p => p.ConnectionId == connectionId);
//        }
//    }
//}
