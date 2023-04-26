namespace Extension.Interfaces
{
    public interface IPooledObject
    {
        /// <summary>
        /// Dispose the object and return it to the pool.
        /// </summary>
        void Dispose();
    }
}