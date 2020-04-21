using Microsoft.AspNetCore.Mvc;
using Service.Extensions;
using System;

namespace Service.Services
{
    public enum PayloadCode
    {
        Success = 0,
        Failure,    //결과값 실패
        Unknown,    //알수 없음

        DbNull,    //DB NULL
        Duplication, //중복
        Mismatch, //불일치
        Not, //돈 티켓등부족이나 사용불가
        Max, //최대
    }

    public class Payload
    {
        public PayloadCode code { get; set; }
    }

    public class Payload<T> : Payload
    {
        public T data { get; set; }
    }

    public static class PayloadPack
    {
        public static Payload<T> Success<T>(T data)
        {
            return new Payload<T>()
            {
                code = PayloadCode.Success,
                data = data,
            };
        }

        public static Payload Fail(PayloadCode code)
        {
            return new Payload()
            {
                code = code,
            };
        }

        public static string Error(Exception ex)
        {
            return ex.ToMessage();
        }
    }

    public static class Payloader
    {
        public static OkObjectResult Success<T>(T data)
        {
            return new OkObjectResult(PayloadPack.Success(data));
        }

        public static OkObjectResult Fail(PayloadCode code)
        {
            return new OkObjectResult(PayloadPack.Fail(code));
        }

        public static NotFoundObjectResult Error(Exception ex)
        {
            return new NotFoundObjectResult(PayloadPack.Error(ex));
        }
    }
}
