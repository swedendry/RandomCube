﻿using BestHTTP.SignalRCore;
using Network;
using Network.GameServer;
using System;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    private void SettingEnvironment()
    {
        try
        {
            //멀티 테스트용
            var arguments = Environment.GetCommandLineArgs();
            var keyIndex = arguments.ToList().FindIndex(x => x.Equals("-u"));
            if (keyIndex >= 0)
            {
                var valueIndex = keyIndex + 1;
                ServerInfo.userId = arguments[valueIndex];
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void Start()
    {
        GameServer.sInstance.isLockSend = false;

        if (!ServerInfo.isLogin)
        {   //첫 로그인
            ServerInfo.userId = SystemInfo.deviceUniqueIdentifier;
#if !UNITY_EDITOR
            SettingEnvironment();
#endif

            MapperFactory.Register();
            XmlFactory.Load();

            GameServer.ActionConnected = Connected;
            GameServer.ActionLogin = Login;
            GameServer.ActionEnterMatch = EnterMatch;
            GameServer.ActionExitMatch = ExitMatch;
            GameServer.ActionSuccessMatch = SuccessMatch;
            GameServer.ActionEnterRoom = EnterRoom;
            GameServer.ActionExitRoom = ExitRoom;
            GameServer.ActionLoading = Loading;

            Router.CloseAndOpen("LoginView");
        }
        else
        {
            Router.CloseAndOpen("LobbyView/PlayView");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Router.Open("PopupView");
        }
    }

    private void Connected(HubConnection connection)
    {
        GameServer.sInstance?.Login(ServerInfo.User.Id, ServerInfo.User.Name);
    }

    private void Login(Payloader<SC_Login> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    ServerInfo.isLogin = true;

                    Router.CloseAndOpen("LobbyView/PlayView");
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
                    var slots = ServerInfo.User.Entry.Slots.ToList().Select((x, i) =>
                    {
                        var cube = ServerInfo.User.Cubes.Find(c => c.CubeId == x);

                        return new GameSlot()
                        {
                            SlotIndex = (byte)i,
                            CubeId = cube.CubeId,
                            CubeLv = cube.Lv,
                        };
                    }).ToList();

                    GameServer.sInstance?.EnterRoom(data.GroupName, new RoomUser()
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

                });
    }

    private void ExitRoom(Payloader<SC_ExitRoom> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Router.CloseAndOpen("LobbyView/PlayView");
                });
    }

    private void Loading(Payloader<SC_Loading> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    ServerInfo.GameUsers = data.Users;

                    SceneManager.LoadScene("MultiGame", LoadSceneMode.Single);
                });
    }
}
