namespace CandyCrushREM.Managers.Grid
{
    using CandyCrushREM.Candies;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(Collider))]
    public class TileSlot : MonoBehaviour
    {
        public Candy AssociatedCandy { get; set; }

        public Vector2Int Position { get; private set; }

        public Color OveringColor { get; private set; }

        private SpriteRenderer _sprite;
        private Color _spriteBaseColor;

        public void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _spriteBaseColor = _sprite.color;
        }

        public void Init(Vector2Int gridPosition, Color overingColor)
        {
            Position = gridPosition;
            OveringColor = overingColor;
            transform.name = Position.ToString();
        }

        public void OnMouseEnter()
        {
            ChangeTileColorTo(OveringColor);
        }

        public void OnMouseExit()
        {
            ChangeTileColorTo(_spriteBaseColor);
        }


        private void OnTriggerStay2D(Collider2D other)
        {
            if (GameManager.Instance.FallManager.AreFalling)
            {
                if (other.TryGetComponent(out Candy candy))
                {
                    AssociatedCandy = candy;
                    AssociatedCandy.transform.SetParent(transform);
                }
            }
        }

        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (GameManager.Instance.FallManager.AreFalling)
            {
                if (other.TryGetComponent(out Candy candy))
                {
                    if (AssociatedCandy == candy)
                        AssociatedCandy = null;
                }
            }
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