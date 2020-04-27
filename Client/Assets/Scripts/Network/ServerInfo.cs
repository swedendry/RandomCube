using Network.LobbyServer;
using System.Collections.Generic;

public class GameUserViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int SP { get; set; }
    public int Life { get; set; }

    public EntryViewModel Entry { get; set; }
}

public static class ServerInfo
{
    public static UserViewModel User = new UserViewModel();

    public static List<GameUser> GameUsers = new List<GameUser>();

    public static GameUserViewModel MyUser = new GameUserViewModel();
    public static GameUserViewModel EnemyUser = new GameUserViewModel();
}
