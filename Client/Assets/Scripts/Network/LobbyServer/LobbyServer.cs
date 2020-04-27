using Network;
using System;
using UnityEngine;

public partial class LobbyServer : MonoBehaviour
{
    public static LobbyServer sInstance;

    public string BaseUri = "https://test-lobby.azurewebsites.net/";
    //public string BaseUri = "https://localhost:44324/";

    private readonly PayloadHttp http = new PayloadHttp();

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
    }

    private void OnApplicationQuit()
    {
        sInstance = null;
    }

    private Uri GetUri(string relativeUri)
    {
        return new Uri(BaseUri + relativeUri);
    }
}
