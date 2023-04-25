namespace Extension.Patterns.ObjectPool
{
    using System.Collections.Generic;
    using UnityEngine;
    using Extension.Transform;

    public class Pool : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject BasePrefab { get; private set; }
        public Queue<GameObject> Instances { get; private set; }

        private void Awake()
        {
            Instances = new Queue<GameObject>();
        }

        public GameObject Get(Transform parent)
        {
            if (IsEmpty())
            {
                GameObject pooledObject = Instantiate(BasePrefab);
                Instances.Enqueue(pooledObject);
            }

            GameObject deq = Instances.Dequeue();
            deq.transform.SetParent(parent);
            deq.transform.ResetTransform();
            deq.SetActive(true);

            return deq;
        }

        public void Dispose(GameObject pooledObject)
        {
            pooledObject.transform.SetParent(transform);
            pooledObject.transform.ResetTransform();
            pooledObject.SetActive(false);
            Instances.Enqueue(pooledObject);
        }

        private bool IsEmpty()
        {
            return Instances.Count == 0;
        }

#if DEBUG
        [ContextMenu("Debug Log Instances Count")]
        private void PrintCount()
        {
            Debug.Log(Instances.Count);
        }
#endif
    }
}
