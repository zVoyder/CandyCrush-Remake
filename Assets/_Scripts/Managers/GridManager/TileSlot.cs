namespace CandyCrushREM.Managers
{
    using CandyCrushREM.Candies;
    using Extension.Generic.Structures;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
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

        public void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _spriteBaseColor = _sprite.color;
        }

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

        private void ChangeTileColorTo(Color color)
        {
            StopCoroutine("LerpColorRoutine");
            StartCoroutine(LerpColorRoutine(.2f, color));
        }

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