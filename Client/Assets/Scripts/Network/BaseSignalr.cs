using BestHTTP;
using BestHTTP.SignalRCore;
using BestHTTP.SignalRCore.Encoders;
using BestHTTP.SignalRCore.Messages;
using System;

namespace Network
{
    public class BaseSignalr
    {
        protected HubConnection connection;

        public event Action<HubConnection> OnConnected;
        public event Action<HubConnection> OnClosed;
        public event Action<HubConnection, string> OnError;
        public event Func<HubConnection, Message, bool> OnMessage;

        public void Close()
        {
            if (connection != null)
            {
                connection.OnConnected -= OnConnected;
                connection.OnClosed -= OnClosed;
                connection.OnError -= OnError;
                connection.OnMessage -= OnMessage;

                connection.StartClose();
            }
        }

        public void Connect(Uri uri)
        {
            var encoder = new LitJsonEncoder();
            var protocol = new JsonProtocol(encoder);
            connection = new HubConnection(uri, protocol);

            connection.OnConnected += OnConnected;
            connection.OnClosed += OnClosed;
            connection.OnError += OnError;
            connection.OnMessage += OnMessage;

            connection.StartConnect();
        }

        public void Send(string target, params object[] args)
        {
            if (connection != null)
                connection.Send(target, args);
        }

        public virtual T GetRealArguments<T>(object[] arguments)
        {
            try
            {
                Type[] types = { typeof(T) };
                var realArgs = connection.Protocol.GetRealArguments(types, arguments);
                return (T)realArgs[0];
            }
            catch (Exception ex)
            {
                HTTPManager.Logger.Error("BaseSignalR", "GetRealArguments: " + ex);
            }

            return default(T);
        }
    }
}