using UI;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        XmlFactory.Load();

        Router.CloseAndOpen("LoginView");
    }
}
