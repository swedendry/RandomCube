//using Game.Hubs;
//using Microsoft.AspNetCore.SignalR;
//using System.Collections.Generic;
//using System.Timers;

//namespace Game.Contents
//{
//    public enum GameState
//    {
//        Wait = 0,   //대기중
//        Loading,    //로딩
//        Play,       //게임 중
//        Result,     //게임 결과
//        End,        //게임 종료
//    }

//    public enum ExitId
//    {
//        Default,
//        DisConnected,
//        SyncTimeOut,
//        LoadingTimeOut,
//    }

//    public class BaseGame
//    {
//        public GameState state { get; set; }
//        public string groupName { get; set; }
//    }

//    public class Game : BaseGame
//    {
//        private readonly IHubContext<GameHub> _context;

//        //private readonly List<GameUser> _users = new List<GameUser>();
//        private Timer _timer = new Timer();

//        public Game(string groupName, IHubContext<GameHub> context)
//        {
//            _context = context;
//            this.groupName = groupName;
//            Reset();
//        }

//        private void Release()
//        {
//            state = GameState.End;
//        }

//        private void OnTimedEvent(object source, ElapsedEventArgs e)
//        {
//            switch (state)
//            {
//                case GameState.Loading:
//                    {

//                    }
//                    break;
//            }
//        }

//        public bool Enter(RoomUser user)
//        {
//            if (state != GameState.Wait)
//                return false;

//            if (GetUserById(user.Id) != null)
//                return false;

//            _users.Add(new GameUser()
//            {
//                Id = user.Id,
//                Name = user.Name,
//                ConnectionId = user.ConnectionId,
//                Entry = user.Entry,
//                Cubes = user.Cubes,
//                Life = 3,
//                SP = 100,
//            });

//            if (_users.Count == 2)
//            {   //시작
//                Start();
//            }

//            return true;
//        }

//        public bool Exit(string id, ExitId exitId)
//        {
//            var user = GetUserById(id);
//            if (user == null)
//                return false;

//            _users.Remove(user);

//            return true;
//        }

//        private void Start()
//        {
//            Reset();

//            SCID_Start();

//            state = GameState.Loading;

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

//        private void Reset()
//        {
//            state = GameState.Wait;
//        }

//        private void SCID_Start()
//        {

//        }

//        public void CSID_Loading(string connectionId, object args)
//        {

//        }

//        private void SCID_Play()
//        {
//            state = GameState.Play;
//        }

//        private void SCID_Round()
//        {

//        }
//    }
//}
