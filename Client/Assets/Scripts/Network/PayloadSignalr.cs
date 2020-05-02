using BestHTTP;
using System;

namespace Network
{
    public class PayloadSignalr : BaseSignalr
    {
        public void Call<T>(Payloader<T> payloader, bool isLocal, object[] arguments) where T : class
        {
            try
            {
                var code = (PayloadCode)arguments[0];

                switch (code)
                {
                    case PayloadCode.Error:
                        {
                            var error = arguments[1].ToString();
                            payloader.OnError(error);
                        }
                        break;
                    case PayloadCode.Success:
                        {
                            var data = isLocal ? (T)arguments[1] : (T)connection.Protocol.ConvertTo(typeof(T), arguments[1]);
                            payloader.OnSuccess(data);
                            payloader.OnComplete(data);
                        }
                        break;
                    default:
                        {
                            payloader.OnFail(code);
                            payloader.OnComplete(null);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                HTTPManager.Logger.Error("BaseSignalR", "GetRealArguments: " + ex);
            }
        }
    }
}