﻿using System.Collections.Generic;
using UnityEngine;

public class PoolRegister : MonoBehaviour
{
    [SerializeField]
    private List<PoolObject> poolObjects = new List<PoolObject>();

    private void Awake()
    {
        PoolFactory.Register(transform);

        poolObjects.ForEach(x => PoolFactory.Register(x));
    }

    private void OnDestroy()
    {
        PoolFactory.UnRegister();
    }
}
