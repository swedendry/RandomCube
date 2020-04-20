using Game.Services;
using Microsoft.AspNetCore.SignalR;

namespace Game.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameService _gameService;

        public GameHub(IGameService gameService)
        {
            _gameService = gameService;
        }
    }
}
