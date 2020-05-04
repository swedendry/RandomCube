using Network.GameServer;
using Network.LobbyServer;
using System.Linq;

namespace Extension
{
    public static class UserExtension
    {
        public static GameUser ToGameUser(this UserViewModel user)
        {
            var slots = user.Entry.Slots.ToList().Select((x, i) =>
            {
                var cube = user.Cubes.Find(c => c.CubeId == x);

                return new GameSlot()
                {
                    SlotIndex = (byte)i,
                    CubeId = cube.CubeId,
                    CubeLv = cube.Lv,
                };
            }).ToList();

            return new GameUser()
            {
                Id = user.Id,
                Name = user.Name,
                Slots = slots,
            };
        }
    }
}