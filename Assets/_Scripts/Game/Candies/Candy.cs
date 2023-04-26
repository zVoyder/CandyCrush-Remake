namespace CandyCrushREM.Candies
{
    using CandyCrushREM.Managers;
    using CandyCrushREM.SO;
    using Extension.Interfaces;
    using Extension.Patterns.ObjectPool;
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    public class Candy : MonoBehaviour, IPooledObject
    {
        public SO_Candy SO_Candy { get; private set; }

        private SpriteRenderer _spriteRender;
        private Pool _relatedPool;

        public SO_Candy.CandyType CandyType => SO_Candy.type;

        public Pool RelatedPool { get; private set; }

        private void Awake()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Initializes candy.
        /// </summary>
        /// <param name="soCandy">ScriptableObject of the candy.</param>
        /// <param name="pool">Associated Pool.</param>
        public void Init(SO_Candy soCandy, Pool pool)
        {
            transform.name = $"Candy (ID {GetInstanceID()})";
            SO_Candy = soCandy;
            _spriteRender.sprite = SO_Candy.spriteImage;
            AssociatePool(pool);
        }

        public void AssociatePool(Pool associatedPool)
        {
            RelatedPool = associatedPool;
        }

        public void Dispose()
        {
            SO_Candy = null;
            RelatedPool.Dispose(gameObject);
        }
    }
}