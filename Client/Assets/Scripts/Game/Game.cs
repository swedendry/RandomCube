﻿using Network;
using Network.GameServer;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    protected List<Team> teams = new List<Team>();

    protected virtual void Start()
    {
        GameServer.ActionCompleteLoading = CompleteLoading;
        GameServer.ActionPlay = Play;
        GameServer.ActionWave = Wave;
        GameServer.ActionResult = Result;
        GameServer.ActionCreateCube = CreateCube;
        GameServer.ActionMoveCube = MoveCube;
        GameServer.ActionCombineCube = CombineCube;
        GameServer.ActionDeleteCube = DeleteCube;
        GameServer.ActionShotMissile = ShotMissile;
        GameServer.ActionDieMonster = DieMonster;
        GameServer.ActionEscapeMonster = EscapeMonster;
        GameServer.ActionUpdateSlot = UpdateSlot;

        ServerInfo.Room.ProgressTime = 0f;

        Loading();
    }

    private void Update()
    {
        ServerInfo.Room.ProgressTime += Time.deltaTime;
    }

    protected List<Team> GetTeams()
    {
        return teams.FindAll(x => x.user != null);
    }

    protected Team GetTeam(string userId)
    {
        return GetTeams().Find(x => x.user.Id == userId);
    }

    protected void CompleteLoading(Payloader<SC_CompleteLoading> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {

                });
    }

    protected void Play(Payloader<SC_Play> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {

                });
    }

    protected void Wave(Payloader<SC_Wave> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    StartCoroutine(WaveMonster(8f));
                });
    }

    protected virtual void Result(Payloader<SC_Result> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GetTeams().ForEach(x => x.UnRegister());

                    ServerInfo.GameUsers = data.Users;

                    Router.Open("ResultView");

                    var my = ServerInfo.MyGameUser();
                    LobbyServer.sInstance.UpdateMoney(my.Id, my.Money);
                });
    }

    protected void CreateCube(Payloader<SC_CreateCube> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GetTeam(data.Id)?.CreateCube(data.NewCube);

                    Router.Refresh();
                });
    }

    protected void MoveCube(Payloader<SC_MoveCube> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GetTeam(data.Id)?.MoveCube(data.CubeSeq, data.PositionX, data.PositionY);
                });
    }

    protected void CombineCube(Payloader<SC_CombineCube> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GetTeam(data.Id)?.CombineCube(data.OwnerSeq, data.TargetSeq);
                });
    }

    protected void DeleteCube(Payloader<SC_DeleteCube> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GetTeam(data.Id)?.DeleteCube(data.DeleteCubes);
                });
    }

    protected void ShotMissile(Payloader<SC_ShotMissile> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GetTeam(data.Id)?.ShotMissile(data.CubeSeq, data.MonsterSeq);
                });
    }

    protected void DieMonster(Payloader<SC_DieMonster> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GetTeam(data.Id)?.DieMonster(data.MonsterSeq);

                    Router.Refresh();
                });
    }

    protected void EscapeMonster(Payloader<SC_EscapeMonster> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GetTeam(data.Id)?.EscapeMonster(data.MonsterSeq);

                    Router.Refresh();

                    CheckResult();
                });
    }

    protected void UpdateSlot(Payloader<SC_UpdateSlot> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GetTeam(data.Id)?.UpdateSlot(data.SlotIndex, data.SlotLv);

                    Router.Refresh();
                });
    }

    protected virtual void Loading()
    {
        teams[0].Register(ServerInfo.MyGameUser(), Map.blue);
        teams[1].Register(ServerInfo.EnemyGameUser(), Map.red);

        Router.CloseAndOpen("GameView");
    }

    protected virtual void CheckResult()
    {

    }

    protected virtual IEnumerator WaveMonster(float delay)
    {
        yield return new WaitForSeconds(delay);

        CreateMonster();
        yield return new WaitForSeconds(1f);
        CreateMonster();
        yield return new WaitForSeconds(1f);
        CreateMonster();

        StartCoroutine(WaveMonster(3f));
    }

    protected virtual void CreateMonster()
    {
        GetTeams().ForEach(x => x.CreateMonster());
    }
}
