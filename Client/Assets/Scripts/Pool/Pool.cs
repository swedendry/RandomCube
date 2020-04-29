using Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public class Pool
    {
        public PoolObject poolObject;

        protected readonly Queue<GameObject> entries = new Queue<GameObject>();

        private int seq = 0;

        public Pool(PoolObject poolObject)
        {
            this.poolObject = poolObject;
        }

        private void Create()
        {
            var entity = Object.Instantiate(poolObject.dummy);
            entity.name += seq;
            seq++;
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

        public void Delete(GameObject entity)
        {
            Return(entity);
            Object.Destroy(entity);
        }

        public void Delete()
        {
            entries.ForEach((x, i) =>
            {
                Object.Destroy(x);
            });

            entries.Clear();
        }
    }
}