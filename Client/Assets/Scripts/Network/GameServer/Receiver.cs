using Network;
using Network.GameServer;
using System;

public partial class GameServer
{
    public static Action<Payloader<SC_Login>> ActionLogin;

    private void OnInvocation(string target, object[] arguments)
    {
        switch (target)
        {
            case "Login":
                {
                    var payloader = new Payloader<SC_Login>();
                    ActionLogin?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
        }
    }
}
