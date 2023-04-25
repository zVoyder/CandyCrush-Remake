namespace CandyCrushREM.Managers
{
    using CandyCrushREM.Candies;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
    public class TileSlotTriggerAllocator : MonoBehaviour
    {
        public TileSlot LinkedTile { get; private set; }

        public delegate bool OnTriggerEnabled();

        [SerializeField]
        private OnTriggerEnabled _triggerEnabled;

        public void Init(OnTriggerEnabled triggerEnabled, TileSlot linkedTile)
        {
            _triggerEnabled = triggerEnabled;
            LinkedTile = linkedTile;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_triggerEnabled())
            {
                if (other.TryGetComponent(out Candy candy))
                {
                    LinkedTile.AssociatedCandy = candy;
                    LinkedTile.AssociatedCandy.transform.SetParent(LinkedTile.transform);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_triggerEnabled())
            {
                if (other.TryGetComponent(out Candy candy))
                {
                    if (LinkedTile.AssociatedCandy == candy)
                    {
                        LinkedTile.AssociatedCandy = null;
                    }
                }
            }
        }
    }
}