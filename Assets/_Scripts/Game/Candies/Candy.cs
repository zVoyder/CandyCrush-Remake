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
            _relatedPool = pool;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            SO_Candy = null;
            _relatedPool.Dispose(gameObject);
        }
    }
}