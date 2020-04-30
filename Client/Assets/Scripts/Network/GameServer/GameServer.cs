using BestHTTP.SignalRCore;
using BestHTTP.SignalRCore.Messages;
using Network;
using System;
using UnityEngine;

public partial class GameServer : MonoBehaviour
{
    public enum Section
    {
        Cloud,
        Local,
    }

    public static Action<HubConnection> ActionConnected;

    public static GameServer sInstance;

    //public string BaseUri = "https://test-game.azurewebsites.net/game";
    //public string BaseUri = "https://localhost:44341/game";
    public Section section;

    private readonly PayloadSignalr signalr = new PayloadSignalr();

    private void Awake()
    {
        if (sInstance != null)
        {
            if (sInstance != this)
            {
                Destroy(gameObject);
            }
            return;
        }
        sInstance = this;

        DontDestroyOnLoad(this);

        Init();
    }

    private void OnApplicationQuit()
    {
        sInstance = null;

        Release();
    }

    private void Release()
    {
        signalr.OnConnected -= OnConnected;
        signalr.OnClosed -= OnClosed;
        signalr.OnError -= OnError;
        signalr.OnMessage -= OnMessage;

        signalr.Close();
    }

    private void Init()
    {
        signalr.OnConnected += OnConnected;
        signalr.OnClosed += OnClosed;
        signalr.OnError += OnError;
        signalr.OnMessage += OnMessage;
    }

    private void OnConnected(HubConnection connection)
    {
        ActionConnected?.Invoke(connection);
    }

    private void OnClosed(HubConnection connection)
    {
        Debug.Log("OnClosed");
    }

    private void OnError(HubConnection connection, string error)
    {
        Debug.Log("OnError : " + error);
    }

    public void Connect()
    {
        signalr.Connect(GetUri());
    }

    public void Close()
    {
        Release();
    }

    private string GetBaseUri()
    {
        switch (section)
        {
            case Section.Local: return "https://localhost:44341/game";
            default: return "https://test-game.azurewebsites.net/game";
        }
    }

    private Uri GetUri()
    {
        return new Uri(GetBaseUri());
    }

    private bool OnMessage(HubConnection connection, Message message)
    {
        switch (message.type)
        {
            case MessageTypes.Invocation:
                {
                    OnInvocation(message.target, message.arguments);
                }
                break;
        }

        return true;
    }

    private void Send(string method, params object[] args)
    {
        signalr.Send(method, args);
    }
}
