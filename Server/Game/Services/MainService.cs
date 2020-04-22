﻿using Game.Contents;
using Game.Hubs;
using Game.Models;
using Microsoft.AspNetCore.SignalR;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Game.Services
{
    public interface IMainService
    {
        Task Login(string connectionId, BaseUser user);
        Task Logout(string connectionId);
        Task EnterMatch(string connectionId, string id);
        Task ExitMatch(string connectionId, string id);
    }

    public class MainService : IMainService
    {
        private readonly IHubContext<GameHub> _context;
        private readonly IMatchService _matchService;
        private readonly IRoomService _roomService;

        private readonly List<BaseUser> _users = new List<BaseUser>();

        public MainService(IHubContext<GameHub> context, IMatchService matchService, IRoomService roomService)
        {
            _context = context;
            _matchService = matchService;
            _roomService = roomService;
        }

        public async Task Login(string connectionId, BaseUser user)
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

        public async Task EnterMatch(string connectionId, string id)
        {
            var method = "EnterMatch";

            try
            {
                var result = false;
                var user = GetUserById(id);
                if (user != null)
                    result = _matchService.Enter(user);

                if (result)
                {
                    await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Success(new SC_EnterMatch()
                    {
                        Id = id,
                    }));
                }
                else
                {
                    await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Fail(PayloadCode.Failure));
                }
            }
            catch (Exception ex)
            {
                await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Error(ex));
            }
        }

        public async Task ExitMatch(string connectionId, string id)
        {
            var method = "ExitMatch";

            try
            {
                var result = false;
                var user = GetUserById(id);
                if (user != null)
                    result = _matchService.Exit(user.Id);

                if (result)
                {
                    await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Success(new SC_ExitMatch()
                    {
                        Id = id,
                    }));
                }
                else
                {
                    await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Fail(PayloadCode.Failure));
                }
            }
            catch (Exception ex)
            {
                await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Error(ex));
            }
        }

        public async Task EnterRoom(string connectionId, string groupName, RoomBaseUser roomUser)
        {
            var method = "EnterRoom";

            try
            {
                var result = false;
                var user = GetUserById(roomUser.Id);
                if (user != null)
                    result = _roomService.Enter(groupName, roomUser);

                if (!result)
                    await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Fail(PayloadCode.Failure));
            }
            catch (Exception ex)
            {
                await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Error(ex));
            }
        }

        public async Task ExitRoom(string connectionId, string id)
        {
            var method = "ExitRoom";

            try
            {
                var result = false;
                var user = GetUserById(id);
                if (user != null)
                    result = _roomService.Exit(id);

                if (!result)
                    await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Fail(PayloadCode.Failure));
            }
            catch (Exception ex)
            {
                await _context.Clients.Client(connectionId).SendAsync(method, PayloadPack.Error(ex));
            }
        }

        //public async Task DeleteRoom(string groupName)
        //{
        //    lock (_lock)
        //    {
        //        var room = GetRoomByGroupName(groupName);
        //        if (room != null)
        //        {
        //            room.Release();
        //            _rooms.Remove(room);
        //        }
        //    }
        //}

        private BaseUser GetUserById(string id)
        {
            return _users.Find(p => p.Id == id);
        }

        private BaseUser GetUserByConnectionId(string connectionId)
        {
            return _users.Find(p => p.ConnectionId == connectionId);
        }
    }
}