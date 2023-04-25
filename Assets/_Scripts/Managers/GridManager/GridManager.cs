namespace CandyCrushREM.Managers
{
    using System.Collections;
    using CandyCrushREM.Candies;
    using CandyCrushREM.Pools;
    using UnityEngine;
    using CandyCrushREM.SO;
    using Extension.Generic.Structures;

    public class GridManager : Grid<TileSlot>
    {
        public SO_LevelPreset LevelPreset { get; private set; }

        [SerializeField]
        private Color _tileOveringColor = Color.yellow;

        [SerializeField, Header("Pools")]
        private CandyPool _candyPool;
        [SerializeField, Header("Managers")]
        private FallManager _fallManager;

        public bool CheckTileAdjacency(TileSlot tileA, TileSlot tileB)
        {
            return IsTileVerticallyAdjacent(tileA, tileB) || IsTileHorizontallyAdjacent(tileA, tileB);
        }
        
        private bool IsTileVerticallyAdjacent(TileSlot tileA, TileSlot tileB)
        {
            int p1 = Mathf.Max(tileA.Position.x, tileB.Position.x);
            int p2 = Mathf.Min(tileA.Position.x, tileB.Position.x);

            return p1 - p2 == 1 && tileB.Position.y - tileA.Position.y == 0;
        }

        private bool IsTileHorizontallyAdjacent(TileSlot tileA, TileSlot tileB)
        {
            int p1 = Mathf.Max(tileA.Position.y, tileB.Position.y);
            int p2 = Mathf.Min(tileA.Position.y, tileB.Position.y);

            return p1 - p2 == 1 && tileB.Position.x - tileA.Position.x == 0;
        }

        public void Init(SO_LevelPreset levelPreset)
        {
            base.Init(levelPreset.gridSize);
            LevelPreset = levelPreset;
        }

        public override TileSlot[,] GenerateGrid()
        {
            RectTransform rectTransform = GridLayout.GetComponent<RectTransform>();
            rectTransform.sizeDelta = Size;
            return base.GenerateGrid();
        }

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