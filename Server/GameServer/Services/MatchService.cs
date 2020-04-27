using AutoMapper;
using GameServer.Hubs;
using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace GameServer.Services
{
    public interface IMatchService
    {
        bool Enter(User user);
        bool Exit(string Id);
    }

    public class MatchService : IMatchService
    {
        private readonly IHubContext<GameHub> _context;
        private readonly IMapper _mapper;

        private readonly List<MatchUser> _users = new List<MatchUser>();
        private readonly object _lock = new object();
        private readonly Timer _timer = new Timer();

        public MatchService(IHubContext<GameHub> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _timer.Interval = 1000; _timer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            lock (_lock)
            {
                if (_users.Count <= 0)
                {   //매칭 플레이어 없음
                    _timer.Stop();
                    return;
                }

                CheckMatching();
            }
        }

        private void CheckMatching()
        {
            var curtime = DateTime.UtcNow;

            const int MAX_USER = 2;
            var users = _users.OrderBy(x => x.MatchTime).Take(MAX_USER).ToList();

            if (users.Count >= MAX_USER)
            {   //매칭 성공
                var groupName = Guid.NewGuid().ToString("d");
                var connectionIds = users.Select(x => x.ConnectionId).ToArray();
                _context.Clients.Clients(connectionIds).SendCoreAsync("SuccessMatch", PayloadPack.Success(new SC_SuccessMatch()
                {
                    GroupName = groupName,
                }));

                users.ForEach(x => _users.Remove(x));
            }
        }

        public bool Enter(User user)
        {
            lock (_lock)
            {
                var matchUser = GetUserById(user.Id);
                if (matchUser != null)  //이미 존재함
                    _users.Remove(matchUser);

                var newUser = _mapper.Map<MatchUser>(user);
                newUser.MatchTime = DateTime.UtcNow;

                _users.Add(newUser);
                _timer.Start();

                return true;
            }
        }

        public bool Exit(string id)
        {
            lock (_lock)
            {
                var matchUser = GetUserById(id);
                if (matchUser == null)
                    return false;

                _users.Remove(matchUser);
                return true;
            }
        }

        private MatchUser GetUserById(string id)
        {
            return _users.Find(p => p.Id == id);
        }
    }
}
