using UI;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Team blue;
    public Team red;

    protected virtual void Start()
    {
        Loading();
    }

    protected virtual void Loading()
    {
        blue?.Create(ServerInfo.MyGameUser(), Map.blue);
        red?.Create(ServerInfo.EnemyGameUser(), Map.red);

        Router.CloseAndOpen("GameView");
    }
}
