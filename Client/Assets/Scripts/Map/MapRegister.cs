using UnityEngine;

public class MapRegister : MonoBehaviour
{
    public Zone blue;
    public Zone red;

    private void Awake()
    {
        Map.blue = blue;
        Map.red = red;
    }
}
