using Network;
using System;
using UnityEngine;

public class LobbyServer : MonoBehaviour
{
    public static LobbyServer sInstance;

    public string BaseUri = "http://thegido-lobby.azurewebsites.net/";

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
