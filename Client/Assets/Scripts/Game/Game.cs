using Network;
using Network.GameServer;
using System.Collections;
using UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Team blue;
    public Team red;

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

        Loading();
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
                    blue?.UnRegister();
                    red?.UnRegister();

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

                    if (ServerInfo.User.Id == data.Id)
                    {
                        blue.CreateCube(data.NewCube);
                    }
                    else
                    {
                        red.CreateCube(data.NewCube);
                    }
                });
    }

    private void MoveCube(Payloader<SC_MoveCube> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("MoveCube");

                    if (ServerInfo.User.Id == data.Id)
                    {
                        blue.MoveCube(data.CubeSeq, data.PositionX, data.PositionY);
                    }
                    else
                    {
                        red.MoveCube(data.CubeSeq, data.PositionX, data.PositionY);
                    }
                });
    }

    private void CombineCube(Payloader<SC_CombineCube> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("MoveCube");

                    if (ServerInfo.User.Id == data.Id)
                    {
                        blue.CombineCube(data.OwnerSeq, data.TargetSeq);
                    }
                    else
                    {
                        red.CombineCube(data.OwnerSeq, data.TargetSeq);
                    }
                });
    }

    private void DeleteCube(Payloader<SC_DeleteCube> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("DeleteCube");

                    if (ServerInfo.User.Id == data.Id)
                    {
                        blue.DeleteCube(data.DeleteCubes);
                    }
                    else
                    {
                        red.DeleteCube(data.DeleteCubes);
                    }
                });
    }

    private void DieMonster(Payloader<SC_DieMonster> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("DieMonster");

                    if (ServerInfo.User.Id == data.Id)
                    {
                        blue.DieMonster(data.MonsterSeq);
                    }
                    else
                    {
                        red.DieMonster(data.MonsterSeq);
                    }
                });
    }

    private void EscapeMonster(Payloader<SC_EscapeMonster> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("EscapeMonster");

                    if (ServerInfo.User.Id == data.Id)
                    {
                        blue.EscapeMonster(data.MonsterSeq);
                    }
                    else
                    {
                        red.EscapeMonster(data.MonsterSeq);
                    }

                    //var isMe = (ServerInfo.User.Id == data.Id);
                    //if (!isMe)
                    //{
                    //    ServerInfo.EnemyGameUser().Life -= 1;
                    //    red.EscapeMonster(data.MonsterSeq);
                    //}
                    //else
                    //{
                    //    ServerInfo.MyGameUser().Life -= 1;
                    //}
                });
    }

    protected virtual void Loading()
    {
        blue?.Register(ServerInfo.MyGameUser(), Map.blue);
        red?.Register(ServerInfo.EnemyGameUser(), Map.red);

        Router.CloseAndOpen("GameView");
    }

    protected IEnumerator WaveMonster()
    {
        yield return new WaitForSeconds(5f);

        blue?.CreateMonster();
        red?.CreateMonster();
        yield return new WaitForSeconds(1f);
        blue?.CreateMonster();
        red?.CreateMonster();
        yield return new WaitForSeconds(1f);
        blue?.CreateMonster();
        red?.CreateMonster();

        StartCoroutine(WaveMonster());
    }
}
