using System.Collections.Generic;

public static class PoolFactory
{
    private static readonly List<Pool<Entity>> pools = new List<Pool<Entity>>();

    public static void Register(PoolObject poolObject)
    {
        pools.Add(new Pool<Entity>(poolObject));
    }

    public static T Get<T>(string key) where T : Entity
    {
        var pool = pools.Find(x => x.poolObject.key == key);
        return (T)pool?.Get();
    }

    public static void Return<T>(string key, T entity) where T : Entity
    {
        var pool = pools.Find(x => x.poolObject.key == key);
        pool?.Return(entity);
    }
}
