using System.Collections.Generic;
using UnityEngine;

public static class ResourceFactory
{
    private static readonly List<ResourceObject> resourceObjects = new List<ResourceObject>();

    public static void Register(ResourceObject resourceObject)
    {
        resourceObjects.Add(resourceObject);
    }

    public static Object Get(string key)
    {
        var resourceObject = resourceObjects.Find(x => x.key == key);
        return resourceObject?.dummy;
    }

    public static T Get<T>(string key) where T : Object
    {
        var obj = Get(key);
        return (T)obj;
    }
}
