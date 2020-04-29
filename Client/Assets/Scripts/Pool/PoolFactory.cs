using Pools;
using System.Collections.Generic;
using UnityEngine;

public static class PoolFactory
{
    private static readonly List<Pool> pools = new List<Pool>();

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

    public static void Return(string key, GameObject entity)
    {
        var pool = pools.Find(x => x.poolObject.key == key);
        pool?.Return(entity);
    }

    public static void Delete(string key, GameObject entity)
    {
        var pool = pools.Find(x => x.poolObject.key == key);
        pool?.Delete(entity);
    }

    public static T Get<T>(string key) where T : Entity
    {
        var obj = Get(key);
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