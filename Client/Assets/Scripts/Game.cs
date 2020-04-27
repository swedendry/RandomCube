using Network;
using Network.GameServer;
using UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Team blue;
    public Team red;

    private void Start()
    {
        GameServer.ActionCompleteLoading = CompleteLoading;
        GameServer.ActionPlay = Play;
        GameServer.ActionWave = Wave;

        Loading();
    }

    private void Loading()
    {
        blue.Create(ServerInfo.MyGameUser(), Map.blue);
        red.Create(ServerInfo.EnemyGameUser(), Map.red);

        Router.CloseAndOpen("GameView");

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
