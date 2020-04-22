using GameServer.Models;
using Service.Services;

namespace GameServer.Services
{
    public class GameProfile : ServerProfile
    {
        public GameProfile()
        {
            CreateMap<User, MatchUser>();
            CreateMap<RoomUser, GameUser>();
        }
    }
}
