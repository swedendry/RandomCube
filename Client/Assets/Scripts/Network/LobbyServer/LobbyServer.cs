using Network;
using System;
using UnityEngine;

public partial class LobbyServer : MonoBehaviour
{
    public enum Section
    {
        Cloud,
        Local,
    }

    public static LobbyServer sInstance;

    public Section section;

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

        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        sInstance = null;
    }

    private string GetBaseUri()
    {
        switch (section)
        {
            case Section.Local: return "https://localhost:44324/";
            default: return "https://test-lobby.azurewebsites.net/";
        }
    }

    private Uri GetUri(string relativeUri)
    {
        return new Uri(GetBaseUri() + relativeUri);
    }
}
