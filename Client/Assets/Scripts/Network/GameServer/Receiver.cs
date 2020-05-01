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
    public static Action<Payloader<SC_DeleteRoom>> ActionDeleteRoom;
    public static Action<Payloader<SC_Loading>> ActionLoading;
    public static Action<Payloader<SC_CompleteLoading>> ActionCompleteLoading;
    public static Action<Payloader<SC_Play>> ActionPlay;
    public static Action<Payloader<SC_Wave>> ActionWave;
    public static Action<Payloader<SC_Result>> ActionResult;
    public static Action<Payloader<SC_CreateCube>> ActionCreateCube;
    public static Action<Payloader<SC_MoveCube>> ActionMoveCube;
    public static Action<Payloader<SC_CombineCube>> ActionCombineCube;
    public static Action<Payloader<SC_DeleteCube>> ActionDeleteCube;
    public static Action<Payloader<SC_DieMonster>> ActionDieMonster;
    public static Action<Payloader<SC_EscapeMonster>> ActionEscapeMonster;

    protected void OnInvocation(string target, object[] arguments)
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
            case "DeleteRoom":
                {
                    var payloader = new Payloader<SC_DeleteRoom>();
                    ActionDeleteRoom?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "Loading":
                {
                    var payloader = new Payloader<SC_Loading>();
                    ActionLoading?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "CompleteLoading":
                {
                    var payloader = new Payloader<SC_CompleteLoading>();
                    ActionCompleteLoading?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "Play":
                {
                    var payloader = new Payloader<SC_Play>();
                    ActionPlay?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "Wave":
                {
                    var payloader = new Payloader<SC_Wave>();
                    ActionWave?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "Result":
                {
                    var payloader = new Payloader<SC_Result>();
                    ActionResult?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "CreateCube":
                {
                    var payloader = new Payloader<SC_CreateCube>();
                    ActionCreateCube?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "MoveCube":
                {
                    var payloader = new Payloader<SC_MoveCube>();
                    ActionMoveCube?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "CombineCube":
                {
                    var payloader = new Payloader<SC_CombineCube>();
                    ActionCombineCube?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "DeleteCube":
                {
                    var payloader = new Payloader<SC_DeleteCube>();
                    ActionDeleteCube?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "DieMonster":
                {
                    var payloader = new Payloader<SC_DieMonster>();
                    ActionDieMonster?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
            case "EscapeMonster":
                {
                    var payloader = new Payloader<SC_EscapeMonster>();
                    ActionEscapeMonster?.Invoke(payloader);
                    signalr.Call(payloader, arguments);
                }
                break;
        }
    }
}
