namespace Network.LobbyServer
{
    public class CubeViewModel
    {
        public int CubeId { get; set; }
        public byte Lv { get; set; }
        public int Parts { get; set; }

        public CubeDataViewModel CubeData { get; set; }
    }
}