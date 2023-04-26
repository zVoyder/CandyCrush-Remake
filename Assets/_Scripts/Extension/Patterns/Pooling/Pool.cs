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

        /// <summary>
        /// Gets a GameObject from the pool list.
        /// </summary>
        /// <returns>GameObject from the pool.</returns>
        public GameObject Get()
        {
            if (IsEmpty())
            {
                GameObject pooledObject = Instantiate(BasePrefab);
                Instances.Enqueue(pooledObject);
            }

            GameObject deq = Instances.Dequeue();
            deq.SetActive(true);

            return deq;
        }

        /// <summary>
        /// Gets a GameObject from the pool list and changes its parent.
        /// </summary>
        /// <param name="parent">The new GameObject transform parent.</param>
        /// <param name="resetTransform">True to reset its transform, False to not reset its transform.</param>
        /// <returns>GameObject from the pool.</returns>
        public GameObject Get(Transform parent, bool resetTransform = true)
        {
            GameObject deq = Get();
            deq.transform.SetParent(parent);

            if(resetTransform)
                deq.transform.ResetTransform();

            return deq;
        }

        /// <summary>
        /// Disposes a GameObject and returns it to the pool list.
        /// </summary>
        /// <param name="pooledObject">Object to Pool.</param>
        public void Dispose(GameObject pooledObject)
        {
            pooledObject.transform.SetParent(transform);
            pooledObject.transform.ResetTransform();
            pooledObject.SetActive(false);
            Instances.Enqueue(pooledObject);
        }

        /// <summary>
        /// Check if the pool list is empty.
        /// </summary>
        /// <returns>True if it is empty, False if not.</returns>
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
