using Network.GameServer;
using Network.LobbyServer;
using System.Collections.Generic;

public static class ServerInfo
{
    public static string userId;

    //Lobby 정보
    public static bool isLogin = false;
    public static UserViewModel User = new UserViewModel();

    //Game 정보
    public static Room Room = new Room();
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
