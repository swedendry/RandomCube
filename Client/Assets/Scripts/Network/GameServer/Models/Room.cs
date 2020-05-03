namespace Network.GameServer
{
    public enum GameMode
    {
        Single,
        Multi,
    }

    public class Room
    {
        public GameMode GameMode { get; set; }
        public float ProgressTime { get; set; }
    }
}
