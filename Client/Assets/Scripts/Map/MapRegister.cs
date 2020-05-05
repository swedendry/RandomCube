using UnityEngine;

public class MapRegister : MonoBehaviour
{
    [SerializeField]
    private Zone blue;
    [SerializeField]
    private Zone red;

    private void Awake()
    {
        Map.blue = blue;
        Map.red = red;
    }
}
