using Extension.Patterns.ObjectPool;
using System.Security.Cryptography;

namespace Extension.Interfaces
{
    public interface IPooledObject
    {
        Pool RelatedPool { get; }

        void AssociatePool(Pool associatedPool);

        /// <summary>
        /// Dispose the object and return it to the pool.
        /// </summary>
        void Dispose();
    }
}