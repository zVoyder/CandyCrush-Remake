namespace CandyCrushREM.Managers
{
    using CandyCrushREM.Candies;
    using System.Collections.Generic;
    using UnityEngine;

    public class ComboManager : MonoBehaviour
    {
        [field: SerializeField, Header("Score")]
        public ScoreManager ScoreManager { get; private set; }

        [field: SerializeField, Header("Combination"), Tooltip("How long a combination must be.")]
        public uint CombinationCount { get; private set; } = 3;

        // Effect on destroy
        [SerializeField]
        private GameObject _effectOnDestroy;

        public void DestroyAllCombos(TileSlot[,] gridTiles)
        {
            List<List<TileSlot>> combos = GetAllCombinations(gridTiles);

            foreach (List<TileSlot> singleCombo in combos)
            {
                foreach (TileSlot tile in singleCombo)
                {
                    this.ScoreManager.AddScore(1);
                    Instantiate(_effectOnDestroy, tile.transform.position, Quaternion.identity);

                    if (tile.AssociatedCandy)
                    {
                        tile.AssociatedCandy.Dispose();
                        tile.AssociatedCandy = null;
                    }
                }
            }
        }

        public List<List<TileSlot>> GetAllCombinations(TileSlot[,] gridTiles)
        {
            List<List<TileSlot>> allCombinations = new List<List<TileSlot>>();

            foreach (List<TileSlot> verticalCombos in GetVerticalCombinations(gridTiles))
            {
                allCombinations.Add(verticalCombos);
            }

            foreach (List<TileSlot> horizontalCombos in GetHorizontalCombinations(gridTiles))
            {
                allCombinations.Add(horizontalCombos);
            }

            return allCombinations;
        }

        private List<List<TileSlot>> GetVerticalCombinations(TileSlot[,] gridTiles)
        {
            List<List<TileSlot>> allVerticalCombinations = new List<List<TileSlot>>();

            for (int x = 0; x < gridTiles.GetLength(0); x++)
            {
                foreach (List<TileSlot> candiesCombo in GetCombinationsOfColumn(gridTiles, x))
                {
                    allVerticalCombinations.Add(candiesCombo);
                }
            }

            return allVerticalCombinations;
        }

        private List<List<TileSlot>> GetHorizontalCombinations(TileSlot[,] gridTiles)
        {
            List<List<TileSlot>> allHorizontalCombinations = new List<List<TileSlot>>();

            for (int y = 0; y < gridTiles.GetLength(1); y++)
            {
                foreach (List<TileSlot> candiesCombo in GetCombinationsOfRow(gridTiles, y))
                {
                    allHorizontalCombinations.Add(candiesCombo);
                }
            }

            return allHorizontalCombinations;
        }

        private List<List<TileSlot>> GetCombinationsOfColumn(TileSlot[,] gridTiles, int columnIndex)
        {
            List<List<TileSlot>> verticalCombinations = new List<List<TileSlot>>();
            List<TileSlot> candySingleCombo = new List<TileSlot>();

            candySingleCombo.Clear();
            candySingleCombo.Add(gridTiles[columnIndex, 0]);

            for (int y = 1; y < gridTiles.GetLength(1); y++)
            {
                if (gridTiles[columnIndex, y].AssociatedCandy.CandyType == gridTiles[columnIndex, y - 1].AssociatedCandy.CandyType)
                {
                    candySingleCombo.Add(gridTiles[columnIndex, y]);
                }
                else
                {
                    if (candySingleCombo.Count >= CombinationCount)
                    {
                        verticalCombinations.Add(new List<TileSlot>(candySingleCombo));
                    }

                    candySingleCombo.Clear();
                    candySingleCombo.Add(gridTiles[columnIndex, y]);
                }
            }

            if (candySingleCombo.Count >= CombinationCount)
            {
                verticalCombinations.Add(new List<TileSlot>(candySingleCombo));
            }

            return verticalCombinations;
        }

        private List<List<TileSlot>> GetCombinationsOfRow(TileSlot[,] gridTiles, int rowIndex)
        {
            List<List<TileSlot>> horizontalCombinations = new List<List<TileSlot>>();
            List<TileSlot> candySingleCombo = new List<TileSlot>();

            candySingleCombo.Clear();
            candySingleCombo.Add(gridTiles[0, rowIndex]);

            for (int x = 1; x < gridTiles.GetLength(0); x++)
            {
                if (gridTiles[x, rowIndex].AssociatedCandy.CandyType == gridTiles[x - 1, rowIndex].AssociatedCandy.CandyType)
                {
                    candySingleCombo.Add(gridTiles[x, rowIndex]);
                }
                else
                {
                    if (candySingleCombo.Count >= CombinationCount)
                    {
                        horizontalCombinations.Add(new List<TileSlot>(candySingleCombo));
                    }

                    candySingleCombo.Clear();
                    candySingleCombo.Add(gridTiles[x, rowIndex]);
                }
            }

            if (candySingleCombo.Count >= CombinationCount)
            {
                horizontalCombinations.Add(new List<TileSlot>(candySingleCombo));
            }

            return horizontalCombinations;
        }
    }
}