//using Game.Contents;
//using Game.Hubs;
//using Game.Models;
//using Microsoft.AspNetCore.SignalR;
//using Service.Services;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Game.Services
//{
//    public interface IGameService
//    {
//        Task Login(string connectionId, string id);
//        Task Logout(string connectionId);
//    }

//    public class GameService : IGameService
//    {
//        private readonly IHubContext<GameHub> _context;
//        private readonly List<BaseUser> _users = new List<BaseUser>();

//        public GameService(IHubContext<GameHub> context)
//        {
//            _context = context;
//        }

//        public async Task Login(string connectionId, string id)
//        {
//            var duplicationuser = GetUserById(id);
//            if (duplicationuser != null)
//            {   //중복
//                _users.Remove(duplicationuser);
//            }

//            var user = new BaseUser()
//            {
//                ConnectionId = connectionId,
//                Id = id,
//            };

//            _users.Add(user);

//            var sc = PayloadPack.Success(new SC_Login()
//            {
//                User = user,
//            });

//            await _context.Clients.Client(connectionId).SendAsync("Login", sc);
//        }

//        public async Task Logout(string connectionId)
//        {
//            var user = GetUserByConnectionId(connectionId);
//            if (user != null)
//            {
//                _users.Remove(user);
//            }

//            await Task.CompletedTask;
//        }

//        private BaseUser GetUserById(string id)
//        {
//            return _users.Find(p => p.Id == id);
//        }

//        private BaseUser GetUserByConnectionId(string connectionId)
//        {
//            return _users.Find(p => p.ConnectionId == connectionId);
//        }
//    }
//}