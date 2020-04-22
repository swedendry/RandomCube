using GameServer.Hubs;
using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace GameServer.Contents
{
    public interface IMatch
    {
        bool Enter(User user);
        bool Exit(string Id);
    }

    public class Match : IMatch
    {
        public Action<List<MatchUser>> OnMatch;

        private readonly IHubContext<GameHub> _context;

        private readonly List<MatchUser> _users = new List<MatchUser>();
        private readonly object _lock = new object();
        private readonly Timer _timer = new Timer();

        public Match(IHubContext<GameHub> context)
        {
            _context = context;
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
                OnMatch?.Invoke(users);

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

                _users.Add(new MatchUser()
                {
                    Id = user.Id,
                    Name = user.Name,
                    ConnectionId = user.ConnectionId,
                    MatchTime = DateTime.UtcNow,
                });

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
