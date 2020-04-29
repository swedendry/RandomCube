﻿using Network.GameServer;
using System.Collections.Generic;

public partial class GameServer
{
    public void Login(string id, string name)
    {
        var cs = new CS_Login()
        {
            Id = id,
            Name = name
        };

        Send("Login", cs);
    }

    public void EnterMatch(string id)
    {
        var cs = new CS_EnterMatch()
        {
            Id = id,
        };

        Send("EnterMatch", cs);
    }

    public void ExitMatch(string id)
    {
        var cs = new CS_ExitMatch()
        {
            Id = id,
        };

        Send("ExitMatch", cs);
    }

    public void EnterRoom(string groupName, RoomUser user)
    {
        var cs = new CS_EnterRoom()
        {
            User = user,
            GroupName = groupName,
        };

        Send("EnterRoom", cs);
    }

    public void ExitRoom(string id)
    {
        var cs = new CS_ExitRoom()
        {
            Id = id,
        };

        Send("ExitRoom", cs);
    }

    public void CompleteLoading(string id)
    {
        var cs = new CS_CompleteLoading()
        {
            Id = id,
        };

        Send("CompleteLoading", cs);
    }

    public void CreateCube(string id, GameCube cube)
    {
        var cs = new CS_CreateCube()
        {
            Id = id,
            NewCube = cube,
        };

        Send("CreateCube", cs);
    }

    public void MoveCube(string id, int cubeSeq, int positionX, int positionY)
    {
        var cs = new CS_MoveCube()
        {
            Id = id,
            CubeSeq = cubeSeq,
            PositionX = positionX,
            PositionY = positionY
        };

        Send("MoveCube", cs);
    }

    public void CombineCube(string id, GameCube cube, List<int> deleteSeq)
    {
        var cs = new CS_CombineCube()
        {
            Id = id,
            NewCube = cube,
            DeleteCubes = deleteSeq,
        };

        Send("CombineCube", cs);
    }

    public void DieMonster(string id, int monsterSeq)
    {
        var cs = new CS_DieMonster()
        {
            Id = id,
            MonsterSeq = monsterSeq,
        };

        Send("DieMonster", cs);
    }

    public void EscapeMonster(string id, int monsterSeq)
    {
        var cs = new CS_EscapeMonster()
        {
            Id = id,
            MonsterSeq = monsterSeq,
        };

        Send("EscapeMonster", cs);
    }
}
