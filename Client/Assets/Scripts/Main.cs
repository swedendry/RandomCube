using BestHTTP.SignalRCore;
using Network;
using Network.GameServer;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    private void Start()
    {
        GameServer.ActionConnected = Connected;
        GameServer.ActionLogin = Login;
        GameServer.ActionEnterMatch = EnterMatch;
        GameServer.ActionExitMatch = ExitMatch;
        GameServer.ActionSuccessMatch = SuccessMatch;
        GameServer.ActionEnterRoom = EnterRoom;
        GameServer.ActionExitRoom = ExitRoom;
        GameServer.ActionLoading = Loading;

        XmlFactory.Load();

        Router.CloseAndOpen("LoginView");
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
                    Debug.Log("SuccessMatch");

                    var slots = ServerInfo.User.Entry.Slots.ToList().Select((x, i) =>
                    {
                        var cube = ServerInfo.User.Cubes.Find(c => c.CubeId == x);

                        return new GameSlot()
                        {
                            SlotIndex = (byte)i,
                            CubeId = cube.CubeId,
                            Lv = cube.Lv,
                        };
                    }).ToList();

                    GameServer.sInstance.EnterRoom(data.GroupName, new RoomUser()
                    {
                        Id = ServerInfo.User.Id,
                        Slots = slots,
                    });
                });
    }

    private void EnterRoom(Payloader<SC_EnterRoom> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("EnterRoom");
                });
    }

    private void ExitRoom(Payloader<SC_ExitRoom> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Router.CloseAndOpen("LobbyView");
                });
    }

    private void Loading(Payloader<SC_Loading> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    ServerInfo.GameUsers = data.Users;

                    SceneManager.LoadScene("Game", LoadSceneMode.Single);
                });
    }
}
