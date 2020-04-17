using System.Collections.Generic;
using UnityEngine;

public enum ZoneId
{
    Blue,
    Red,
}

public class Zone : MonoBehaviour
{
    public GameObject box;
    public List<GameObject> paths;
}
