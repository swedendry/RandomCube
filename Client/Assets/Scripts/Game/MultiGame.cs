﻿using Network;
using Network.GameServer;
using UnityEngine;

public class MultiGame : Game
{
    protected override void Start()
    {
        GameServer.ActionCompleteLoading = CompleteLoading;
        GameServer.ActionPlay = Play;
        GameServer.ActionWave = Wave;

        base.Start();
    }

    protected override void Loading()
    {
        base.Loading();

        GameServer.sInstance.CompleteLoading(ServerInfo.MyGameUser().Id);
    }

    private void CompleteLoading(Payloader<SC_CompleteLoading> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("CompleteLoading");
                });
    }

    private void Play(Payloader<SC_Play> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("Play");
                });
    }

    private void Wave(Payloader<SC_Wave> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("Wave");

                    StartCoroutine(blue.CreateMonster());
                    StartCoroutine(red.CreateMonster());
                });
    }
}