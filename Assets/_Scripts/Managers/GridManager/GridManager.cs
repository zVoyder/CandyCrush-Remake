namespace CandyCrushREM.Managers
{
    using System.Collections;
    using CandyCrushREM.Candies;
    using UnityEngine;
    using CandyCrushREM.SO;
    using UnityEngine.UI;


    public class GridManager : MonoBehaviour
    {
        [field: SerializeField]
        public GridLayoutGroup GridLayout { get; private set; }

        [field: SerializeField, Header("Tile")]
        public GameObject TilePrefab { get; private set; }

        [field: SerializeField]
        public Color TileOveringColor { get; private set; } = Color.yellow;

        [field: SerializeField, Header("MovesManager")]
        public SwapManager MovesManager { get; private set; }

        public TileSlot[,] GenerateCandiesWithGrid(SO_LevelPreset LevelPreset)
        {
            TileSlot[,] tiles = GenerateGrid(LevelPreset);

            RectTransform rectTransform = GridLayout.GetComponent<RectTransform>();
            rectTransform.sizeDelta = LevelPreset.gridSize;

            for (int y = 0; y < LevelPreset.gridSize.y; y++)
            {
                for (int x = 0; x < LevelPreset.gridSize.x; x++)
                {
                    if (Instantiate(LevelPreset.candieRows[y].candies[x].candyBase, tiles[x, y].transform.position, Quaternion.identity, tiles[x, y].transform).TryGetComponent(out Candy candy))
                    {
                        candy.Init(LevelPreset.candieRows[y].candies[x]);
                        tiles[x, y].AssociatedCandy = candy;
                    }
                }
            }

            return tiles;
        }

        public void GenerateCandyIn(SO_Candy soCandy, Vector2Int gridPosition, TileSlot[,] gridTiles)
        {
            if (Instantiate(soCandy.candyBase, gridTiles[gridPosition.x, gridPosition.y].transform.position, Quaternion.identity, gridTiles[gridPosition.x, gridPosition.y].transform).TryGetComponent(out Candy candy))
            {
                candy.Init(soCandy);
                gridTiles[gridPosition.x, gridPosition.y].AssociatedCandy = candy;
            }
        }

        public bool IsGridFullOfCandy(TileSlot[,] gridTiles)
        {
            foreach (TileSlot tile in gridTiles)
            {
                if (tile == null) return false;
            }

            return true;
        }

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

        private TileSlot[,] GenerateGrid(SO_LevelPreset LevelPreset)
        {
            Vector2Int size = LevelPreset.gridSize;
            TileSlot[,] tiles = new TileSlot[size.x, size.y];

            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    if (Instantiate(TilePrefab, GridLayout.transform.position, Quaternion.identity, GridLayout.transform).TryGetComponent(out TileSlot tile))
                    {
                        tile.Init(new Vector2Int(x, y), TileOveringColor);
                        tiles[x, y] = tile;
                    }
                }
            }

            return tiles;
        }
    }
}