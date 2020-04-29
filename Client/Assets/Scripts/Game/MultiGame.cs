using Network;
using Network.GameServer;
using UI;
using UnityEngine;

public class MultiGame : Game
{
    protected override void Start()
    {
        GameServer.ActionCompleteLoading = CompleteLoading;
        GameServer.ActionPlay = Play;
        GameServer.ActionWave = Wave;
        GameServer.ActionResult = Result;
        GameServer.ActionCreateCube = CreateCube;
        GameServer.ActionMoveCube = MoveCube;
        GameServer.ActionCombineCube = CombineCube;
        GameServer.ActionDieMonster = DieMonster;
        GameServer.ActionEscapeMonster = EscapeMonster;

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

    private void Result(Payloader<SC_Result> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    blue.Result();
                    red.Result();

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

                    var isMe = (ServerInfo.User.Id == data.Id);
                    if (!isMe)
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

                    var isMe = (ServerInfo.User.Id == data.Id);
                    if (!isMe)
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
                    Debug.Log("CombineCube");

                    var isMe = (ServerInfo.User.Id == data.Id);
                    if (!isMe)
                    {
                        red.CombineCube(data.NewCube, data.DeleteCubes);
                    }
                });
    }

    private void DieMonster(Payloader<SC_DieMonster> payloader)
    {
        payloader.Callback(
                success: (data) =>
                {
                    Debug.Log("DieMonster");

                    var isMe = (ServerInfo.User.Id == data.Id);
                    if (!isMe)
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

                    var isMe = (ServerInfo.User.Id == data.Id);
                    if (!isMe)
                    {
                        ServerInfo.EnemyGameUser().Life -= 1;
                        red.EscapeMonster(data.MonsterSeq);
                    }
                    else
                    {
                        ServerInfo.MyGameUser().Life -= 1;
                    }
                });
    }
}
