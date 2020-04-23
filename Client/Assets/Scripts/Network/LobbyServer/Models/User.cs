using System.Collections.Generic;

namespace Network.LobbyServer
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }

        public EntryViewModel Entry { get; set; }
        public List<CubeViewModel> Cubes { get; set; }
    }
}