public class MultiGame : Game
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Loading()
    {
        base.Loading();

        GameServer.sInstance?.CompleteLoading(ServerInfo.MyGameUser().Id);
    }
}
