using Network.GameServer;

public class MultiGame : Game
{
    protected override void Start()
    {
        GameServer.sInstance.isLockSend = false;
        ServerInfo.Room.GameMode = GameMode.Multi;

        base.Start();
    }

    protected override void Loading()
    {
        base.Loading();

        GameServer.sInstance?.CompleteLoading(ServerInfo.MyGameUser().Id);
    }
}
