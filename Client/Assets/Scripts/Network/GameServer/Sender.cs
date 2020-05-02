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

    public virtual void CreateCube(string id, GameCube cube)
    {
        var cs = new CS_CreateCube()
        {
            Id = id,
            NewCube = cube,
        };

        SendImmediate("CreateCube", cs, cs.Map<SC_CreateCube>());
    }

    public virtual void MoveCube(string id, int cubeSeq, int positionX, int positionY)
    {
        var cs = new CS_MoveCube()
        {
            Id = id,
            CubeSeq = cubeSeq,
            PositionX = positionX,
            PositionY = positionY
        };

        SendImmediate("MoveCube", cs, cs.Map<SC_MoveCube>());
    }

    public virtual void CombineCube(string id, int ownerSeq, int targetSeq)
    {
        var cs = new CS_CombineCube()
        {
            Id = id,
            OwnerSeq = ownerSeq,
            TargetSeq = targetSeq,
        };

        SendImmediate("CombineCube", cs, cs.Map<SC_CombineCube>());
    }

    public void DeleteCube(string id, List<int> deleteSeq)
    {
        var cs = new CS_DeleteCube()
        {
            Id = id,
            DeleteCubes = deleteSeq,
        };

        SendImmediate("DeleteCube", cs, cs.Map<SC_DeleteCube>());
    }

    public void DieMonster(string id, int monsterSeq)
    {
        var cs = new CS_DieMonster()
        {
            Id = id,
            MonsterSeq = monsterSeq,
        };

        SendImmediate("DieMonster", cs, cs.Map<SC_DieMonster>());
    }

    public void EscapeMonster(string id, int monsterSeq)
    {
        var cs = new CS_EscapeMonster()
        {
            Id = id,
            MonsterSeq = monsterSeq,
        };

        SendImmediate("EscapeMonster", cs, cs.Map<SC_EscapeMonster>());
    }

    public void UpdateSlot(string id, byte slotIndex, byte slotLv)
    {
        var cs = new CS_UpdateSlot()
        {
            Id = id,
            SlotIndex = slotIndex,
            SlotLv = slotLv,
        };

        SendImmediate("UpdateSlot", cs, cs.Map<SC_UpdateSlot>());
    }
}
