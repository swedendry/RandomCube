using BestHTTP;
using Newtonsoft.Json;
using System;

namespace Network
{
    public enum ParsingType
    {
        Default,
        Json,
        Protocol,
    }

    public class PayloadSignalr : BaseSignalr
    {
        public void Call<T>(Payloader<T> payloader, ParsingType type, object[] arguments) where T : class
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
                            var data = default(T);
                            switch (type)
                            {
                                case ParsingType.Json: data = JsonConvert.DeserializeObject<T>(arguments[1].ToString()); break;
                                case ParsingType.Protocol: data = (T)connection.Protocol.ConvertTo(typeof(T), arguments[1]); break;
                                default: data = (T)arguments[1]; break;
                            }
                            //var data = isLocal ? (T)arguments[1] : (T)connection.Protocol.ConvertTo(typeof(T), arguments[1]);
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