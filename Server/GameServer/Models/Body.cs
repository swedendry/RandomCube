using System.Collections.Generic;

namespace GameServer.Models
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
    /// 매칭 성공 정보
    /// </summary>
    public class SC_SuccessMatch
    {
        public string GroupName { get; set; }
    }

    /// <summary>
    /// 룸 진입 정보
    /// </summary>
    public class CS_EnterRoom
    {
        public RoomUser User { get; set; }
        public string GroupName { get; set; }
    }

    public class SC_EnterRoom
    {
        public RoomUser User { get; set; }
        public string GroupName { get; set; }
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

    /// <summary>
    /// 로딩 정보
    /// </summary>
    public class SC_Loading
    {
        public List<GameUser> Users { get; set; }
    }

    /// <summary>
    /// 로딩 완료 정보
    /// </summary>
    public class CS_CompleteLoading
    {
        public string Id { get; set; }
    }

    public class SC_CompleteLoading
    {
        public string Id { get; set; }
    }

    /// <summary>
    /// 플레이 정보
    /// </summary>
    public class SC_Play
    {

    }

    /// <summary>
    /// 웨이브 정보
    /// </summary>
    public class SC_Wave
    {

    }

    /// <summary>
    /// 큐브 생성 정보
    /// </summary>
    public class CS_CreateCube
    {
        public string Id { get; set; }
        public GameCube NewCube { get; set; }
    }

    public class SC_CreateCube
    {
        public string Id { get; set; }
        public GameCube NewCube { get; set; }
    }

    /// <summary>
    /// 큐브 스왑 정보
    /// </summary>
    public class CS_CombineCube
    {
        public string Id { get; set; }
        public GameCube NewCube { get; set; }
        public List<GameCube> DeleteCubes { get; set; }
    }

    public class SC_CombineCube
    {
        public string Id { get; set; }
        public GameCube NewCube { get; set; }
        public List<GameCube> DeleteCubes { get; set; }
    }

    /// <summary>
    /// 큐브 이동 정보
    /// </summary>
    public class CS_MoveCube
    {
        public string Id { get; set; }
        public int CubeSeq { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }

    public class SC_MoveCube
    {
        public string Id { get; set; }
        public int CubeSeq { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }

    /// <summary>
    /// 몬스터 파괴 정보
    /// </summary>
    public class CS_DieMonster
    {
        public string Id { get; set; }
        public GameCube Cube { get; set; }
    }

    public class SC_DieMonster
    {
        public string Id { get; set; }
        public GameCube Cube { get; set; }
    }
}
