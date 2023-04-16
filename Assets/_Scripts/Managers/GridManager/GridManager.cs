namespace CandyCrushREM.Managers.Grid
{
    using System.Collections;
    using CandyCrushREM.Candies;
    using UnityEngine;
    using CandyCrushREM.SO;
    using UnityEngine.UI;


    public class GridManager : MonoBehaviour
    {
        [field: SerializeField, Header("Level")]
        public SO_LevelPreset LevelPreset { get; private set; }

        [field: SerializeField]
        public GridLayoutGroup GridLayout { get; private set; }

        [field: SerializeField, Header("Tile")]
        public GameObject TilePrefab { get; private set; }

        [field: SerializeField]
        public Color TileOveringColor { get; private set; } = Color.yellow;

        [field: SerializeField, Header("Combo Manager")]
        public ComboManager ComboManager { get; private set; }

        public bool IsSwapping { get; private set; } = false;

        public TileSlot[,] GenerateCandiesWithGrid()
        {
            TileSlot[,] tiles = GenerateGrid();

            for (int y = 0; y < LevelPreset.GridSize.y; y++)
            {
                for (int x = 0; x < LevelPreset.GridSize.x; x++)
                {
                    if (Instantiate(LevelPreset.CandieRows[y].candies[x].candyBase, tiles[x, y].transform.position, Quaternion.identity, tiles[x, y].transform).TryGetComponent(out Candy candy))
                    {
                        candy.Init(LevelPreset.CandieRows[y].candies[x]);
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

        public bool CheckTileAdjacency(TileSlot tileA, TileSlot tileB)
        {
            return IsTileVerticallyAdjacent(tileA, tileB) || IsTileHorizontallyAdjacent(tileA, tileB);
        }
        
        public bool TrySwap(TileSlot tileA, TileSlot tileB, TileSlot[,] gridTiles)
        {
            if (!IsSwapping)
            {
                bool isValid = IsSwapMoveValid(tileA, tileB, gridTiles);

                if (CheckTileAdjacency(tileA, tileB))
                {
                    StartCoroutine(SwapRoutine(tileA, tileB, gridTiles, isValid, .2f));

                    if (isValid)
                    {
                        //Debug.Log(isValid);

                        // Swapping the candies
                        Candy candyA = tileA.AssociatedCandy;
                        Candy candyB = tileB.AssociatedCandy;
                        tileA.AssociatedCandy = candyB;
                        tileB.AssociatedCandy = candyA;

                        // Change the parent of the first candy's transform
                        candyA.transform.SetParent(tileB.transform);
                        candyB.transform.SetParent(tileA.transform);
                    }
                }

                return isValid;
            }

            return false;
        }

        public bool IsGridFullOfCandy(TileSlot[,] gridTiles)
        {
            foreach (TileSlot tile in gridTiles)
            {
                if (tile == null) return false;
            }

            return true;
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

        private IEnumerator SwapRoutine(TileSlot tileA, TileSlot tileB, TileSlot[,] gridTiles, bool isValid, float duration)
        {
            IsSwapping = true;
            yield return LerpSwap(tileA.AssociatedCandy.transform, tileB.AssociatedCandy.transform, duration);

            if (!isValid)
            {
                yield return LerpSwap(tileB.AssociatedCandy.transform, tileA.AssociatedCandy.transform, duration);
            }

            IsSwapping = false;
            yield break;
        }

        private IEnumerator LerpSwap(Transform transformA, Transform transformB, float duration)
        {
            Vector3 startPosA = transformA.position;
            Vector3 endPosA = transformB.position;

            Vector3 startPosB = transformB.position;
            Vector3 endPosB = transformA.position;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / duration;
                transformA.position = Vector3.Lerp(startPosA, endPosA, t);
                transformB.position = Vector3.Lerp(startPosB, endPosB, t);
                yield return null;
            }
        }

        private bool IsSwapMoveValid(TileSlot tileA, TileSlot tileB, TileSlot[,] gridTiles)
        {
            int beforeComboCount = ComboManager.GetAllCombinations(gridTiles).Count;
            //Debug.Log("BEFORE " + beforeComboCount);

            FastSwap(tileA, tileB);

            int afterComboCount = ComboManager.GetAllCombinations(gridTiles).Count;
            //Debug.Log("AFTER " + afterComboCount);

            FastSwap(tileA, tileB); // Reset the swap

            return beforeComboCount < afterComboCount;
        }

        private void FastSwap(TileSlot tileA, TileSlot tileB)
        {
            Candy toSwap = tileA.AssociatedCandy;

            tileA.AssociatedCandy = tileB.AssociatedCandy;
            tileB.AssociatedCandy = toSwap;

            // No needs to change also the position
        }

        private TileSlot[,] GenerateGrid()
        {
            Vector2Int size = LevelPreset.GridSize;
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