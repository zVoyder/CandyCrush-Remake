namespace CandyCrushREM.Managers
{
    using CandyCrushREM.Candies;
    using CandyCrushREM.Pools;
    using CandyCrushREM.SO;
    using Extension.Generic.Structures;
    using UnityEngine;

    public class GridManager : Grid<TileSlot>
    {
        public SO_LevelPreset LevelPreset { get; private set; }

        [SerializeField]
        private Color _tileOveringColor = Color.yellow;

        [SerializeField, Header("Pools")]
        private CandyPool _candyPool;

        [SerializeField, Header("Managers")]
        private FallManager _fallManager;

        /// <summary>
        /// Initializes Grid.
        /// </summary>
        /// <param name="levelPreset">ScriptableObject LevelPreset.</param>
        public void Init(SO_LevelPreset levelPreset)
        {
            base.Init(levelPreset.gridSize);
            LevelPreset = levelPreset;
        }

        /// <inheritdoc/>
        public override TileSlot[,] GenerateGrid()
        {
            RectTransform rectTransform = GridLayout.GetComponent<RectTransform>();
            rectTransform.sizeDelta = Size;
            return base.GenerateGrid();
        }

        /// <inheritdoc/>
        protected override void GenerateTile(TileSlot[,] slots, Vector2Int position)
        {
            base.GenerateTile(slots, position);
            slots[position.x, position.y].Init(this, _fallManager, position, _tileOveringColor);

            if (_candyPool.Get(slots[position.x, position.y].transform).TryGetComponent(out Candy candy))
            {
                candy.Init(LevelPreset.candieRows[position.y].candies[position.x], _candyPool);
                slots[position.x, position.y].AssociatedCandy = candy;
            }
        }
    }
}