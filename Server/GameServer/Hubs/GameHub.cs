using GameServer.Models;
using GameServer.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace GameServer.Hubs
{
    public class GameHub : Hub
    {
        private readonly IMainService _mainService;
        private readonly IRoomService _roomService;

        public GameHub(IMainService mainService, IRoomService roomService)
        {
            _mainService = mainService;
            _roomService = roomService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _mainService.Logout(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public void Login(CS_Login cs)
        {
            _mainService.Login(Context.ConnectionId, cs.Id, cs.Name);
        }

        public void EnterMatch(CS_EnterMatch cs)
        {
            _mainService.EnterMatch(Context.ConnectionId, cs.Id);
        }

        public void ExitMatch(CS_ExitMatch cs)
        {
            _mainService.ExitMatch(Context.ConnectionId, cs.Id);
        }

        public void EnterRoom(CS_EnterRoom cs)
        {
            _mainService.EnterRoom(Context.ConnectionId, cs);
        }

        public void ExitRoom(CS_ExitRoom cs)
        {
            _mainService.ExitRoom(Context.ConnectionId, cs.Id);
        }

        public void CompleteLoading(CS_CompleteLoading cs)
        {
            var room = _roomService.GetRoomById(cs.Id);
            room?.CSID_CompleteLoading(cs.Id);
        }

        public void CreateCube(CS_CreateCube cs)
        {
            var room = _roomService.GetRoomById(cs.Id);
            room?.CSID_CreateCube(cs);
        }

        public void MoveCube(CS_MoveCube cs)
        {
            var room = _roomService.GetRoomById(cs.Id);
            room?.CSID_MoveCube(cs);
        }
    }
}
