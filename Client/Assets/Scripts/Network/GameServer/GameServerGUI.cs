using UnityEngine;

public class GameServerGUI : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameServer.sInstance?.Connect();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            GameServer.sInstance?.Close();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            var id = SystemInfo.deviceUniqueIdentifier;
            var name = SystemInfo.deviceName;

            GameServer.ActionLogin = (payloader) =>
            {
                payloader.Callback(
                    complete: (data) =>
                    {
                        Debug.Log(string.Format("complete2 : {0}", data));
                    }, success: (data) =>
                    {
                        Debug.Log(string.Format("success2 : {0}", data));
                    }, fail: (data) =>
                    {
                        Debug.Log(string.Format("fail2 : {0}", data));
                    }, error: (data) =>
                    {
                        Debug.Log(string.Format("error2 : {0}", data));
                    });
            };

            GameServer.sInstance?.Login(id, name);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            
        }
    }
}
