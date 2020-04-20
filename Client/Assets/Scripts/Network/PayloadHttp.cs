using BestHTTP;
using Newtonsoft.Json;
using System;

namespace Network
{
    public class PayloadHttp : BaseHttp
    {
        public Payloader<T> Get<T>(Uri url)
        {
            var payloader = new Payloader<T>();

            Get(url, (req, res) =>
            {
                OnPayload(payloader, res);
            });

            return payloader;
        }

        public Payloader<T> Post<T>(Uri url, object field)
        {
            var payloader = new Payloader<T>();

            Post(url, field, (req, res) =>
            {
                OnPayload(payloader, res);
            });

            return payloader;
        }

        public Payloader<T> Put<T>(Uri url, object field)
        {
            var payloader = new Payloader<T>();

            Put(url, field, (req, res) =>
            {
                OnPayload(payloader, res);
            });

            return payloader;
        }

        public Payloader<T> Delete<T>(Uri url)
        {
            var payloader = new Payloader<T>();

            Delete(url, (req, res) =>
            {
                OnPayload(payloader, res);
            });

            return payloader;
        }

        private void OnPayload<T>(Payloader<T> payloader, HTTPResponse res)
        {
            try
            {
                if (res == null)
                {
                    payloader.OnError("OnPayload HTTPResponse null");
                    return;
                }

                if (res.IsSuccess)
                {
                    var payload = JsonConvert.DeserializeObject<Payload<T>>(res.DataAsText);
                    if (payload.code == PayloadCode.Success)
                    {   //성공
                        payloader.OnSuccess(payload.data);
                    }
                    else
                    {   //실패
                        payloader.OnFail(payload.code);
                    }

                    payloader.OnComplete(payload.data);
                }
                else
                {   //에러
                    payloader.OnError(res.DataAsText);
                }
            }
            catch (Exception ex)
            {
                payloader.OnError(ex.Message);
            }
        }
    }
}
