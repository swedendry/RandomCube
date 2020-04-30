using Extension;
using Network;
using Network.LobbyServer;
using System;

public partial class LobbyServer
{
    public static Action<Payloader<UpdateCubeLvBody>> ActionUpdateCubeLv;

    public Payloader<UpdateCubeLvBody> UpdateCubeLv(string userId, int cubeId)
    {
        var url = string.Format("api/users/{0}/cubes/{1}/lv", userId, cubeId);

        var payloader = http.Put<UpdateCubeLvBody>(GetUri(url), null).Callback(
            success: (data) =>
            {
                ServerInfo.User.Cubes.Upsert(data.Cube, x => x.CubeId == data.Cube.CubeId);
                ServerInfo.User.Money = data.Money;
            });

        ActionUpdateCubeLv?.Invoke(payloader);

        return payloader;
    }
}
