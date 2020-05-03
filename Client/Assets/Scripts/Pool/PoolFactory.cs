using Pools;
using System.Collections.Generic;
using UnityEngine;

public static class PoolFactory
{
    private static Transform root;
    private static readonly List<Pool> pools = new List<Pool>();

    public static void Register(Transform parent)
    {
        root = parent;
    }

    public static void Register(PoolObject poolObject)
    {
        pools.Add(new Pool(poolObject));
    }

    public static void UnRegister()
    {
        pools.ForEach(x => x.Delete());
        pools.Clear();
    }

    public static GameObject Get(string key)
    {
        var pool = pools.Find(x => x.poolObject.key == key);
        return pool?.Get();
    }

    public static GameObject Get(string key, Transform parent)
    {
        var pool = pools.Find(x => x.poolObject.key == key);
        return pool?.Get(parent);
    }

    public static GameObject Get(string key, Vector3 position, Quaternion rotation)
    {
        var pool = pools.Find(x => x.poolObject.key == key);
        return pool?.Get(position, rotation);
    }

    public static GameObject Get(string key, Vector3 position, Quaternion rotation, Transform parent)
    {
        var pool = pools.Find(x => x.poolObject.key == key);
        return pool?.Get(position, rotation, parent);
    }

    public static void Return(string key, GameObject entity)
    {
        var pool = pools.Find(x => x.poolObject.key == key);
        pool?.Return(entity, root);
    }

    public static void Delete(string key, GameObject entity)
    {
        var pool = pools.Find(x => x.poolObject.key == key);
        pool?.Delete(entity, root);
    }

    public static T Get<T>(string key) where T : Entity
    {
        var obj = Get(key);
        var component = obj?.GetComponent<T>();
        if (component == null)
            component = obj?.AddComponent<T>();

        return component;
    }

    public static T Get<T>(string key, Transform parent) where T : Entity
    {
        var obj = Get(key, parent);
        var component = obj?.GetComponent<T>();
        if (component == null)
            component = obj?.AddComponent<T>();

        return component;
    }

    public static T Get<T>(string key, Vector3 position, Quaternion rotation) where T : Entity
    {
        var obj = Get(key, position, rotation);
        var component = obj?.GetComponent<T>();
        if (component == null)
            component = obj?.AddComponent<T>();

        return component;
    }

    public static T Get<T>(string key, Vector3 position, Quaternion rotation, Transform parent) where T : Entity
    {
        var obj = Get(key, position, rotation, parent);
        var component = obj?.GetComponent<T>();
        if (component == null)
            component = obj?.AddComponent<T>();

        return component;
    }

    public static void Return<T>(string key, T entity) where T : Entity
    {
        Return(key, entity.gameObject);
    }

    public static void Delete<T>(string key, T entity) where T : Entity
    {
        Delete(key, entity.gameObject);
    }
}