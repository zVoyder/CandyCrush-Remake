namespace CandyCrushREM.Managers
{
    using CandyCrushREM.Candies;
    using Extension.Generic.Structures;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
    public class TileSlot : MonoBehaviour
    {
        public Candy AssociatedCandy { get; set; }

        public Grid<TileSlot> AssociatedGrid { get; private set; }

        public Vector2Int Position { get; private set; }

        public Color OveringColor { get; private set; }

        [SerializeField]
        private TileSlotTriggerAllocator _tileTriggerAllocator;
        private SpriteRenderer _sprite;
        private Color _spriteBaseColor;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _spriteBaseColor = _sprite.color;
        }

        /// <summary>
        /// Initializes the TileSlot.
        /// </summary>
        /// <param name="associatedGrid">Associated Grid of TileSlot components.</param>
        /// <param name="fallManager">Associated FallManager.</param>
        /// <param name="gridPosition">Position in the grid.</param>
        /// <param name="overingColor">Color when the cursor is overing the cell.</param>
        public void Init(Grid<TileSlot> associatedGrid, FallManager fallManager, Vector2Int gridPosition, Color overingColor)
        {
            Position = gridPosition;
            OveringColor = overingColor;
            transform.name = Position.ToString();
            AssociatedGrid = associatedGrid;

            _tileTriggerAllocator.Init(() => fallManager.AreFalling, this);
        }

        public void OnMouseEnter()
        {
            ChangeTileColorTo(OveringColor);
        }

        public void OnMouseExit()
        {
            ChangeTileColorTo(_spriteBaseColor);
        }

        /// <summary>
        /// Changes the TileColor.
        /// </summary>
        /// <param name="color">Target color.</param>
        private void ChangeTileColorTo(Color color)
        {
            StopCoroutine("LerpColorRoutine");
            StartCoroutine(LerpColorRoutine(.2f, color));
        }

        /// <summary>
        /// Lerping Color Coroutine.
        /// </summary>
        /// <param name="duration">Transition duration.</param>
        /// <param name="targetColor">Target Color.</param>
        /// <returns></returns>
        private IEnumerator LerpColorRoutine(float duration, Color targetColor)
        {
            Color startColor = _sprite.color;

            for (float timeElapsed = 0; timeElapsed < duration; timeElapsed += Time.deltaTime)
            {
                float t = Mathf.Clamp01(timeElapsed / duration);
                _sprite.color = Color.Lerp(startColor, targetColor, t);
                yield return null;
            }

            _sprite.color = targetColor;
        }
    }
}