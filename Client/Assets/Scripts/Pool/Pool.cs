using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public PoolObject poolObject;

    protected readonly Queue<GameObject> entries = new Queue<GameObject>();

    public Pool(PoolObject poolObject)
    {
        this.poolObject = poolObject;
    }

    private void Create()
    {
        var entity = Object.Instantiate(poolObject.dummy);
        entries.Enqueue(entity);
    }

    public GameObject Get()
    {
        if (entries.Count <= 0)
            Create();

        return entries.Dequeue();
    }

    public void Return(GameObject entity)
    {
        entity.gameObject.SetActive(false);
        entries.Enqueue(entity);
    }
}

//using System.Collections.Generic;

//public class Pool<T> where T : Entity
//{
//    public PoolObject poolObject;

//    protected readonly Queue<T> entries = new Queue<T>();

//    public Pool(PoolObject poolObject)
//    {
//        this.poolObject = poolObject;
//    }

//    private void Create()
//    {
//        var entity = UnityEngine.Object.Instantiate(poolObject.dummy) as T;
//        entries.Enqueue(entity);
//    }

//    public T Get()
//    {
//        if (entries.Count <= 0)
//            Create();

//        return entries.Dequeue();
//    }

//    public void Return(T entity)
//    {
//        entity.gameObject.SetActive(false);
//        entries.Enqueue(entity);
//    }
//}