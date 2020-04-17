using UnityEngine;

public enum GameState
{
    Ready,
    Start,
    Play,
    End,
}

public class Game : MonoBehaviour
{
    private GameState gameState = GameState.Ready;

    public GameUser my;
    public GameUser enemy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeState(GameState.Ready);
        }
    }

    private void GameReady()
    {
        my.Create(true, Map.blue);
        enemy.Create(false, Map.red);
    }

    private void GameStart()
    {

    }

    private void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Ready:
                {
                    GameReady();
                }
                break;
            case GameState.Start:
                {
                    GameStart();
                }
                break;
            case GameState.Play:
                {

                }
                break;
            case GameState.End:
                {

                }
                break;
        }

        gameState = state;
    }
}
