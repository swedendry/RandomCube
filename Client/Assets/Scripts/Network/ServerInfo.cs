using Network.GameServer;
using Network.LobbyServer;
using System.Collections.Generic;

public static class ServerInfo
{
    public static bool isLogin = false;
    public static UserViewModel User = new UserViewModel();
    public static List<GameUser> GameUsers = new List<GameUser>();

    public static GameUser MyGameUser()
    {
        return GameUsers.Find(x => x.Id == User.Id);
    }

    public static GameUser EnemyGameUser()
    {
        return GameUsers.Find(x => x.Id != User.Id);
    }
}
