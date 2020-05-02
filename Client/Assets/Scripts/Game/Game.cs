using Network;
using Network.GameServer;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    public List<Team> teams = new List<Team>();

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
        GameServer.ActionDieMonster = DieMonster;
        GameServer.ActionEscapeMonster = EscapeMonster;
        GameServer.ActionUpdateSlot = UpdateSlot;

        Loading();
    }

    private List<Team> GetTeams()
    {
        return teams.FindAll(x => x.user != null);
    }

    private Team GetTeam(string userId)
    {
        return GetTeams().Find(x => x.user.Id == userId);
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

                    StartCoroutine(WaveMonster());
                });
    }

    private void Result(Payloader<SC_Result> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    GetTeams().ForEach(x => x.UnRegister());

                    ServerInfo.GameUsers = data.Users;

                    Router.Open("ResultView");
                });
    }

    private void CreateCube(Payloader<SC_CreateCube> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("CreateCube");

                    GetTeam(data.Id)?.CreateCube(data.NewCube);

                    Router.Refresh();
                });
    }

    private void MoveCube(Payloader<SC_MoveCube> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("MoveCube");

                    GetTeam(data.Id)?.MoveCube(data.CubeSeq, data.PositionX, data.PositionY);
                });
    }

    private void CombineCube(Payloader<SC_CombineCube> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("MoveCube");

                    GetTeam(data.Id)?.CombineCube(data.OwnerSeq, data.TargetSeq);
                });
    }

    private void DeleteCube(Payloader<SC_DeleteCube> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("DeleteCube");

                    GetTeam(data.Id)?.DeleteCube(data.DeleteCubes);
                });
    }

    private void DieMonster(Payloader<SC_DieMonster> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("DieMonster");

                    GetTeam(data.Id)?.DieMonster(data.MonsterSeq);

                    Router.Refresh();
                });
    }

    private void EscapeMonster(Payloader<SC_EscapeMonster> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("EscapeMonster");

                    GetTeam(data.Id)?.EscapeMonster(data.MonsterSeq);

                    Router.Refresh();

                    CheckResult();
                });
    }

    private void UpdateSlot(Payloader<SC_UpdateSlot> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("UpdateSlot");

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

    protected IEnumerator WaveMonster()
    {
        yield return new WaitForSeconds(5f);

        CreateMonster();
        yield return new WaitForSeconds(1f);
        CreateMonster();
        yield return new WaitForSeconds(1f);
        CreateMonster();

        StartCoroutine(WaveMonster());
    }

    private void CreateMonster()
    {
        GetTeams().ForEach(x => x.CreateMonster());
    }
}
