using Network.GameServer;

public class AITeam : Team
{
    private Record record;
    private RecordPack curRecordPack;

    public override void Register(GameUser user, Zone zone, Record record)
    {
        Register(user, zone);

        this.record = record;
        curRecordPack = this.record.Packs.Dequeue();
    }

    public override void UnRegister()
    {
        base.UnRegister();

        record.Packs.Clear();
    }

    private void Update()
    {
        if (record == null || curRecordPack == null)
            return;

        if (curRecordPack.Time > ServerInfo.Room.ProgressTime)
            return;

        //실행
        Send(curRecordPack);
        curRecordPack = record.Packs.Count <= 0 ? null : record.Packs.Dequeue();
    }

    private void Send(RecordPack pack)
    {
        GameServer.sInstance?.SendLocalAI(pack.Method, pack.Args);
    }

    protected override void OnShot(Cube owner)
    {
        var target = GetShotTarget(owner);
        if (!target)
            return;

        GameServer.sInstance?.ShotMissile(user.Id, owner.gameCube.CubeSeq, target.seq);
    }

    protected override void OnDie(Monster target)
    {
        GameServer.sInstance?.DieMonster(user.Id, target.seq);
    }

    protected override void OnEscape(Monster target)
    {
        GameServer.sInstance?.EscapeMonster(user.Id, target.seq);
    }
}
