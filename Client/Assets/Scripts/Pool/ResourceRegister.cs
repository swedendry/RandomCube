using System.Collections.Generic;
using UnityEngine;

public class ResourceRegister : MonoBehaviour
{
    public List<ResourceObject> resourceObjects = new List<ResourceObject>();

    private void Awake()
    {
        resourceObjects.ForEach(x => ResourceFactory.Register(x));
    }
}
