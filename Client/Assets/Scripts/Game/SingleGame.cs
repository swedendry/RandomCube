﻿using Extension;
using Network.GameServer;
using Network.LobbyServer;
using System.Linq;
using UnityEngine;

public class SingleGame : Game
{
    protected override void Start()
    {
        MapperFactory.Register();
        XmlFactory.Load();

        if (string.IsNullOrEmpty(ServerInfo.User.Id))
            ServerInfo.User = DummyUser(SystemInfo.deviceUniqueIdentifier, SystemInfo.deviceName);

        ServerInfo.GameUsers.Clear();
        ServerInfo.GameUsers.Add(DummyGameUser(ServerInfo.User));
        ServerInfo.GameUsers.Add(DummyGameUser(DummyUser("ai", "ai")));

        base.Start();

        StartCoroutine(WaveMonster());
    }

    private UserViewModel DummyUser(string id, string name)
    {
        var allCubes = XmlKey.CubeData.FindAll<CubeDataXml.Data>();
        var cubes = allCubes.Random(ServerDefine.MAX_ENTRY_SLOT).Select((x, i) => new CubeViewModel()
        {
            CubeId = x.CubeId,
            CubeData = x.Map<CubeDataViewModel>(),
            Lv = 1,
            Parts = 0,
        }).ToList();

        var entry = new EntryViewModel()
        {
            Slots = cubes.Select(x => x.CubeId).ToArray(),
        };

        return new UserViewModel()
        {
            Id = id,
            Name = name,
            Money = 1000,
            Cubes = cubes,
            Entry = entry
        };
    }

    private GameUser DummyGameUser(UserViewModel user)
    {
        var slots = user.Entry.Slots.ToList().Select((x, i) =>
        {
            var cube = user.Cubes.Find(c => c.CubeId == x);

            return new GameSlot()
            {
                SlotIndex = (byte)i,
                CubeId = cube.CubeId,
                CubeLv = cube.Lv,
            };
        }).ToList();

        return new GameUser()
        {
            Id = user.Id,
            Name = user.Name,
            Slots = slots,
        };
    }
}
