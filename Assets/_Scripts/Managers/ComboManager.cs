namespace CandyCrushREM.Managers
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using CandyCrushREM.Candies;
    using CandyCrushREM.Managers.Grid;

    public class ComboManager : MonoBehaviour
    {
        [field: SerializeField, Header("Combination"), Tooltip("How long a combination must be.")]
        public int CombinationCount { get; private set; } = 3;

        // Effect on destroy
        //[field: SerializeField]
        //public GameObject EffectOnDestroy { get; private set; }

        public void DestroyAllCombos(TileSlot[,] gridTiles)
        {
            List<List<Candy>> combos = GetAllCombinations(gridTiles);

            foreach (List<Candy> singleCombo in combos)
            {
                foreach (Candy candy in singleCombo)
                {
                    Destroy(candy.gameObject);
                }
            }
        }

        public List<List<Candy>> GetAllCombinations(TileSlot[,] gridTiles)
        {
            List<List<Candy>> allCombinations = new List<List<Candy>>();

            foreach (List<Candy> verticalCombos in GetVerticalCombinations(gridTiles))
            {
                allCombinations.Add(verticalCombos);
            }

            foreach (List<Candy> horizontalCombos in GetHorizontalCombinations(gridTiles))
            {
                allCombinations.Add(horizontalCombos);
            }

            return allCombinations;
        }

        public List<List<Candy>> GetVerticalCombinations(TileSlot[,] gridTiles)
        {
            List<List<Candy>> allVerticalCombinations = new List<List<Candy>>();

            for (int x = 0; x < gridTiles.GetLength(0); x++)
            {
                foreach(List<Candy> candiesCombo in GetCombinationsOfColumn(gridTiles, x))
                {
                    allVerticalCombinations.Add(candiesCombo);
                }
            }

            return allVerticalCombinations;
        }

        public List<List<Candy>> GetHorizontalCombinations(TileSlot[,] gridTiles)
        {
            List<List<Candy>> allHorizontalCombinations = new List<List<Candy>>();

            for (int y = 0; y < gridTiles.GetLength(1); y++)
            {
                foreach (List<Candy> candiesCombo in GetCombinationsOfRow(gridTiles, y))
                {
                    allHorizontalCombinations.Add(candiesCombo);
                }
            }

            return allHorizontalCombinations;
        }

        public List<List<Candy>> GetCombinationsOfColumn(TileSlot[,] gridTiles, int columnIndex)
        {
            List<List<Candy>> verticalCombinations = new List<List<Candy>>();
            List<Candy> candySingleCombo = new List<Candy>();

            candySingleCombo.Clear();
            candySingleCombo.Add(gridTiles[columnIndex, 0].AssociatedCandy);

            for (int y = 1; y < gridTiles.GetLength(1); y++)
            {
                if (gridTiles[columnIndex, y].AssociatedCandy.CandyType == gridTiles[columnIndex, y - 1].AssociatedCandy.CandyType)
                {
                    candySingleCombo.Add(gridTiles[columnIndex, y].AssociatedCandy);
                }
                else
                {
                    if (candySingleCombo.Count >= CombinationCount)
                    {
                        verticalCombinations.Add(new List<Candy>(candySingleCombo));
                    }

                    candySingleCombo.Clear();
                    candySingleCombo.Add(gridTiles[columnIndex, y].AssociatedCandy);
                }
            }

            if (candySingleCombo.Count >= CombinationCount)
            {
                verticalCombinations.Add(new List<Candy>(candySingleCombo));
            }

            return verticalCombinations;
        }

        public List<List<Candy>> GetCombinationsOfRow(TileSlot[,] gridTiles, int rowIndex)
        {
            List<List<Candy>> horizontalCombinations = new List<List<Candy>>();
            List<Candy> candySingleCombo = new List<Candy>();

            candySingleCombo.Clear();
            candySingleCombo.Add(gridTiles[0, rowIndex].AssociatedCandy);

            for (int x = 1; x < gridTiles.GetLength(0); x++)
            {
                if (gridTiles[x, rowIndex].AssociatedCandy.CandyType == gridTiles[x - 1, rowIndex].AssociatedCandy.CandyType)
                {
                    candySingleCombo.Add(gridTiles[x, rowIndex].AssociatedCandy);
                }
                else
                {
                    if (candySingleCombo.Count >= CombinationCount)
                    {
                        horizontalCombinations.Add(new List<Candy>(candySingleCombo));
                    }

                    candySingleCombo.Clear();
                    candySingleCombo.Add(gridTiles[x, rowIndex].AssociatedCandy);
                }
            }

            if (candySingleCombo.Count >= CombinationCount)
            {
                horizontalCombinations.Add(new List<Candy>(candySingleCombo));
            }

            return horizontalCombinations;
        }
    }
}