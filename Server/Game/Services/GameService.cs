using Game.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Game.Services
{
    public interface IGameService
    {

    }

    public class GameService : IGameService
    {
        private readonly IHubContext<GameHub> _context;

        public GameService(IHubContext<GameHub> context)
        {
            _context = context;
        }
    }
}