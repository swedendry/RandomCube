using BestHTTP.SignalRCore;
using Network;
using Network.GameServer;
using UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    private void Start()
    {
        GameServer.ActionConnected = Connected;
        GameServer.ActionLogin = Login;
        GameServer.ActionEnterMatch = EnterMatch;
        GameServer.ActionExitMatch = ExitMatch;
        GameServer.ActionSuccessMatch = SuccessMatch;
    }

    private void Connected(HubConnection connection)
    {
        GameServer.sInstance.Login(ServerInfo.User.Id, ServerInfo.User.Name);
    }

    private void Login(Payloader<SC_Login> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Router.CloseAndOpen("LobbyView");
                });
    }

    private void EnterMatch(Payloader<SC_EnterMatch> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Router.Open("MatchView");
                });
    }

    private void ExitMatch(Payloader<SC_ExitMatch> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Router.Close("MatchView");
                });
    }

    private void SuccessMatch(Payloader<SC_SuccessMatch> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GameServer.sInstance.EnterRoom(data.GroupName, new RoomUser()
                    {
                        Id = ServerInfo.User.Id,
                        Entry = ServerInfo.User.Entry,
                        Cubes = ServerInfo.User.Cubes,
                    });
                });
    }
}
