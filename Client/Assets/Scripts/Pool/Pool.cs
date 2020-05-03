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

        public GameObject Get(Transform parent)
        {
            var entry = Get();
            entry.transform.parent = parent;

            return entry;
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            var entry = Get();
            entry.transform.localPosition = position;
            entry.transform.localRotation = rotation;

            return entry;
        }

        public GameObject Get(Vector3 position, Quaternion rotation, Transform parent)
        {
            var entry = Get();
            entry.transform.parent = parent;
            entry.transform.localPosition = position;
            entry.transform.localRotation = rotation;

            return entry;
        }

        public void Return(GameObject entity, Transform parent)
        {
            entity.transform.parent = parent;
            entity.gameObject?.SetVisible(false);
            entries.Enqueue(entity);
        }

        public void Delete(GameObject entity, Transform parent)
        {
            Return(entity, parent);
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