using System;
using UnityEngine;

namespace Network
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

    public class Payloader
    {
        private Action<PayloadCode> fail;
        private Action<string> error;

        public Payloader OnFail(PayloadCode code)
        {
            Debug.Log(code);

            fail?.Invoke(code);

            return this;
        }

        public Payloader OnError(string msg)
        {
            Debug.Log(msg);

            error?.Invoke(msg);

            return this;
        }

        public Payloader Callback(Action<PayloadCode> fail, Action<string> error)
        {
            if (fail != null)
                this.fail += fail;
            if (error != null)
                this.error += error;

            return this;
        }
    }

    public class Payloader<T> : Payloader
    {
        private Action<T> complete;
        private Action<T> success;

        public Payloader<T> OnComplete(T data)
        {
            complete?.Invoke(data);

            return this;
        }

        public Payloader<T> OnSuccess(T data)
        {
            success?.Invoke(data);

            return this;
        }

        public Payloader<T> Callback(Action<T> complete = null, Action<T> success = null, Action<PayloadCode> fail = null, Action<string> error = null)
        {
            Callback(fail, error);
            if (complete != null)
                this.complete += complete;
            if (success != null)
                this.success += success;

            return this;
        }
    }
}