using Network.GameServer;
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

    public KeyValuePair<string, CS_CreateCube> CreateCube(string id, GameCube cube)
    {
        var pair = new KeyValuePair<string, CS_CreateCube>("CreateCube", new CS_CreateCube()
        {
            Id = id,
            NewCube = cube,
        });

        SendLocal(pair.Key, pair.Value.Map<SC_CreateCube>());
        Send(pair.Key, pair.Value);

        return pair;
    }

    public KeyValuePair<string, CS_MoveCube> MoveCube(string id, int cubeSeq, int positionX, int positionY)
    {
        var pair = new KeyValuePair<string, CS_MoveCube>("MoveCube", new CS_MoveCube()
        {
            Id = id,
            CubeSeq = cubeSeq,
            PositionX = positionX,
            PositionY = positionY
        });

        SendLocal(pair.Key, pair.Value.Map<SC_MoveCube>());
        Send(pair.Key, pair.Value);

        return pair;
    }

    public KeyValuePair<string, CS_CombineCube> CombineCube(string id, int ownerSeq, int targetSeq)
    {
        var pair = new KeyValuePair<string, CS_CombineCube>("CombineCube", new CS_CombineCube()
        {
            Id = id,
            OwnerSeq = ownerSeq,
            TargetSeq = targetSeq,
        });

        SendLocal(pair.Key, pair.Value.Map<SC_CombineCube>());
        Send(pair.Key, pair.Value);

        return pair;
    }

    public KeyValuePair<string, CS_DeleteCube> DeleteCube(string id, List<int> deleteSeq)
    {
        var pair = new KeyValuePair<string, CS_DeleteCube>("DeleteCube", new CS_DeleteCube()
        {
            Id = id,
            DeleteCubes = deleteSeq,
        });

        SendLocal(pair.Key, pair.Value.Map<SC_DeleteCube>());
        Send(pair.Key, pair.Value);

        return pair;
    }

    public void ShotMissile(string id, int cubeSeq, int monsterSeq)
    {
        var cs = new CS_ShotMissile()
        {
            Id = id,
            CubeSeq = cubeSeq,
            MonsterSeq = monsterSeq,
        };

        SendLocal("ShotMissile", cs.Map<SC_ShotMissile>());
        Send("ShotMissile", cs);
    }

    public void DieMonster(string id, int monsterSeq)
    {
        var cs = new CS_DieMonster()
        {
            Id = id,
            MonsterSeq = monsterSeq,
        };

        SendLocal("DieMonster", cs.Map<SC_DieMonster>());
        Send("DieMonster", cs);
    }

    public void EscapeMonster(string id, int monsterSeq)
    {
        var cs = new CS_EscapeMonster()
        {
            Id = id,
            MonsterSeq = monsterSeq,
        };

        SendLocal("EscapeMonster", cs.Map<SC_EscapeMonster>());
        Send("EscapeMonster", cs);
    }

    public KeyValuePair<string, CS_UpdateSlot> UpdateSlot(string id, byte slotIndex, byte slotLv)
    {
        var pair = new KeyValuePair<string, CS_UpdateSlot>("UpdateSlot", new CS_UpdateSlot()
        {
            Id = id,
            SlotIndex = slotIndex,
            SlotLv = slotLv,
        });

        SendLocal(pair.Key, pair.Value.Map<SC_UpdateSlot>());
        Send(pair.Key, pair.Value);

        return pair;
    }
}
