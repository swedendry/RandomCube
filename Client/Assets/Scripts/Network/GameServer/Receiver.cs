using Network;
using Network.GameServer;
using System;

public partial class GameServer
{
    public static Action<Payloader<SC_Login>> ActionLogin;
    public static Action<Payloader<SC_EnterMatch>> ActionEnterMatch;
    public static Action<Payloader<SC_ExitMatch>> ActionExitMatch;
    public static Action<Payloader<SC_SuccessMatch>> ActionSuccessMatch;
    public static Action<Payloader<SC_EnterRoom>> ActionEnterRoom;
    public static Action<Payloader<SC_ExitRoom>> ActionExitRoom;

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
            case "EnterMatch":
                {
                    var payloader = new Payloader<SC_EnterMatch>();
                    ActionEnterMatch?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "ExitMatch":
                {
                    var payloader = new Payloader<SC_ExitMatch>();
                    ActionExitMatch?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "SuccessMatch":
                {
                    var payloader = new Payloader<SC_SuccessMatch>();
                    ActionSuccessMatch?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "EnterRoom":
                {
                    var payloader = new Payloader<SC_EnterRoom>();
                    ActionEnterRoom?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "ExitRoom":
                {
                    var payloader = new Payloader<SC_ExitRoom>();
                    ActionExitRoom?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
        }
    }
}
