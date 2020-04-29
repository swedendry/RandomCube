using System.Collections.Generic;
using UnityEngine;

public class PoolRegister : MonoBehaviour
{
    public List<PoolObject> poolObjects = new List<PoolObject>();

    private void Awake()
    {
        poolObjects.ForEach(x => PoolFactory.Register(x));
    }

    private void OnDestroy()
    {
        PoolFactory.UnRegister();
    }
}
