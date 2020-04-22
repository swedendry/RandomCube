namespace Network.GameServer
{
    /// <summary>
    /// 로그인 정보
    /// </summary>
    public class CS_Login
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class SC_Login
    {
        public User User { get; set; }
    }

    /// <summary>
    /// 매칭 진입 정보
    /// </summary>
    public class CS_EnterMatch
    {
        public string Id { get; set; }
    }

    public class SC_EnterMatch
    {
        public string Id { get; set; }
    }

    /// <summary>
    /// 매칭 나가기 정보
    /// </summary>
    public class CS_ExitMatch
    {
        public string Id { get; set; }
    }

    public class SC_ExitMatch
    {
        public string Id { get; set; }
    }

    /// <summary>
    /// 룸 진입 정보
    /// </summary>
    public class CS_EnterRoom
    {
        public string Id { get; set; }
    }

    public class SC_EnterRoom
    {
        public string Id { get; set; }
    }

    /// <summary>
    /// 룸 나가기 정보
    /// </summary>
    public class CS_ExitRoom
    {
        public string Id { get; set; }
    }

    public class SC_ExitRoom
    {
        public string Id { get; set; }
    }

    /// <summary>
    /// 룸 삭제 정보
    /// </summary>
    public class CS_DeleteRoom
    {
        public string Id { get; set; }
    }

    public class SC_DeleteRoom
    {
        public string Id { get; set; }
    }
}