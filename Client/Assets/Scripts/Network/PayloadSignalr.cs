using BestHTTP;
using System;

namespace Network
{
    //public static class PayloaderExtension
    //{
    //    public static Payloader<T> Get<T>(this Payloader<T> payloader, object[] arguments)
    //    {
    //        try
    //        {
    //            var payload = (Payload)connection.Protocol.ConvertTo(typeof(Payload), arguments[0]);
    //            var code = payload.code;

    //            switch (code)
    //            {
    //                case PayloadCode.Error:
    //                    {
    //                        var payloadT = (Payload<string>)connection.Protocol.ConvertTo(typeof(Payload<string>), arguments[0]);
    //                        var data = payloadT.data;

    //                        payloader.OnError(data);
    //                    }
    //                    break;
    //                case PayloadCode.Success:
    //                    {
    //                        var payloadT = (Payload<T>)connection.Protocol.ConvertTo(typeof(Payload<T>), arguments[0]);
    //                        var data = payloadT.data;

    //                        int k = 0;
    //                        k++;
    //                    }
    //                    break;
    //                default:
    //                    {

    //                    }
    //                    break;
    //            }

    //            return payloader;
    //        }
    //        catch (Exception ex)
    //        {
    //            HTTPManager.Logger.Error("BaseSignalR", "GetRealArguments: " + ex);
    //        }

    //        return default(T);
    //    }
    //}

    public class PayloadSignalr : BaseSignalr
    {
        //private Payloader<T> Create<T>()
        //{
        //    return new Payloader<T>();
        //}

        //private Payloader<T> GetPayload<T>(this Payloader<T> payloader, object[] arguments)
        //{
        //    try
        //    {
        //        var payload = (Payload)connection.Protocol.ConvertTo(typeof(Payload), arguments[0]);
        //        var code = payload.code;

        //        switch (code)
        //        {
        //            case PayloadCode.Error:
        //                {
        //                    var payloadT = (Payload<string>)connection.Protocol.ConvertTo(typeof(Payload<string>), arguments[0]);
        //                    var data = payloadT.data;

        //                    payloader.OnError(data);
        //                }
        //                break;
        //            case PayloadCode.Success:
        //                {
        //                    var payloadT = (Payload<T>)connection.Protocol.ConvertTo(typeof(Payload<T>), arguments[0]);
        //                    var data = payloadT.data;

        //                    int k = 0;
        //                    k++;
        //                }
        //                break;
        //            default:
        //                {

        //                }
        //                break;
        //        }

        //        return payloader;
        //    }
        //    catch (Exception ex)
        //    {
        //        HTTPManager.Logger.Error("BaseSignalR", "GetRealArguments: " + ex);
        //    }

        //    return default(T);
        //}

        //public override T GetRealArguments<T>(object[] arguments)
        //{
        //    try
        //    {
        //        var payload = (Payload)connection.Protocol.ConvertTo(typeof(Payload), arguments[0]);
        //        var code = payload.code;

        //        switch (code)
        //        {
        //            case PayloadCode.Error:
        //                {
        //                    var payloadT = (Payload<string>)connection.Protocol.ConvertTo(typeof(Payload<string>), arguments[0]);
        //                    var data = payloadT.data;

        //                    int k = 0;
        //                    k++;
        //                }
        //                break;
        //            case PayloadCode.Success:
        //                {
        //                    var payloadT = (Payload<T>)connection.Protocol.ConvertTo(typeof(Payload<T>), arguments[0]);
        //                    var data = payloadT.data;

        //                    int k = 0;
        //                    k++;
        //                }
        //                break;
        //            default:
        //                {

        //                }
        //                break;
        //        }

        //        return default(T);
        //    }
        //    catch (Exception ex)
        //    {
        //        HTTPManager.Logger.Error("BaseSignalR", "GetRealArguments: " + ex);
        //    }

        //    return default(T);
        //}
    }
}