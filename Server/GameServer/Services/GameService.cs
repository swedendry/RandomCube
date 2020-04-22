using GameServer.Contents;
using GameServer.Hubs;
using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameServer.Services
{
    public interface IGameService
    {
        Task Login(string connectionId, User user);
        Task Logout(string connectionId);
    }

    public class GameService
    {
        private readonly IHubContext<GameHub> _context;

        private readonly List<User> _users = new List<User>();

        private readonly Match _match;
        private readonly List<Game> _games = new List<Game>();

        public GameService(IHubContext<GameHub> context)
        {
            _context = context;

            _match = new Match(context);
        }

        public async Task Login(string connectionId, User user)
        {
            var method = "Login";

            try
            {
                var duplicationUser = GetUserById(user.Id);
                if (duplicationUser != null)
                {   //중복
                    _users.Remove(duplicationUser);
                }

                user.ConnectionId = connectionId;

                _users.Add(user);

                await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Success(new SC_Login()
                {
                    User = user,
                }));
            }
            catch (Exception ex)
            {
                await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Error(ex));
            }
        }

        public async Task Logout(string connectionId)
        {
            try
            {
                var user = GetUserByConnectionId(connectionId);
                if (user != null)
                    _users.Remove(user);
            }
            catch (Exception)
            {

            }

            await Task.CompletedTask;
        }

        private User GetUserById(string id)
        {
            return _users.Find(p => p.Id == id);
        }

        private User GetUserByConnectionId(string connectionId)
        {
            return _users.Find(p => p.ConnectionId == connectionId);
        }
    }
}
