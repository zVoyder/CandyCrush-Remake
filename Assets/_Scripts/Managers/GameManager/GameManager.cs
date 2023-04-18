namespace CandyCrushREM.Managers
{
    using System.Collections.Generic;
    using CandyCrushREM.Candies;
    using CandyCrushREM.SO;
    using Extension.Classes.Serializable.Singleton;
    using UnityEngine;

    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField, Header("Level Preset")]
        public SO_LevelPreset Level { get; private set; }

        [field: SerializeField, Header("Managers")]
        public GridManager Grid { get; private set; }

        [field: SerializeField]
        public PhaseManager PhaseManager { get; private set; }

        [field: SerializeField]
        public SwapManager SwapManager { get; private set; }

        [field: SerializeField]
        public ComboManager ComboManager { get; private set; }

        [field: SerializeField]
        public FallManager FallManager { get; private set; }

        [field: SerializeField]
        public UIManager UIManager { get; private set; }

        [field: SerializeField]
        public ScoreManager ScoreManager { get; private set; }

        public TileSlot[,] CurrentTiles { get; private set; }

        private void Start()
        {
            SwapManager.MaxMoves = Level.maxMoves;
            ScoreManager.ScoreToAchieve = Level.scoreToAchieve;
            UIManager.InitUI();

            CurrentTiles = Grid.GenerateCandiesWithGrid(Level);
            PhaseManager.Begin();
        }

        #region DEBUG
#if DEBUG

        [field: SerializeField, Header("Debug")]
        public int RowToCheck { get; private set; }
        [field: SerializeField]
        public int ColumnToCheck { get; private set; }

        [ContextMenu("Debug Log Combo Count")]
        public void DebugLogComboCount() 
        {
            Debug.Log(ComboManager.GetAllCombinations(CurrentTiles).Count);
        }

        [ContextMenu("DebugLog Combos Of Selected Column")]
        public void DebugLogCombosOfSelectedColumn()
        {
            List<List<Candy>> combos = ComboManager.GetCombinationsOfColumn(CurrentTiles, ColumnToCheck);

            foreach (List<Candy> candies in combos)
            {
                Debug.Log($"COLUMN {ColumnToCheck} COMBO:");
                foreach (Candy c in candies)
                {
                    Debug.Log(c.transform.name);
                }
            }
        }

        [ContextMenu("DebugLog Combos Of Selected Row")]
        public void DebugLogCombosOfSelectedRow()
        {
            List<List<Candy>> combos = ComboManager.GetCombinationsOfRow(CurrentTiles, RowToCheck);

            foreach (List<Candy> candies in combos)
            {
                Debug.Log($"COLUMN {RowToCheck} COMBO:");
                foreach (Candy c in candies)
                {
                    Debug.Log(c.transform.name);
                }
            }
        }

        [ContextMenu("DebugLog All Vertical Combos")]
        public void DebugLogAllVerticalCombos()
        {
            List<List<Candy>> combos = ComboManager.GetVerticalCombinations(CurrentTiles);

            foreach (List<Candy> candies in combos)
            {
                Debug.Log("COMBO:");
                foreach (Candy c in candies)
                {
                    Debug.Log(c.transform.name);
                }
            }
        }

        [ContextMenu("DebugLog All Horizontal Combos")]
        public void DebugLogAllHorizontalCombos()
        {
            List<List<Candy>> combos = ComboManager.GetHorizontalCombinations(CurrentTiles);

            foreach (List<Candy> candies in combos)
            {
                Debug.Log("COMBO:");
                foreach (Candy c in candies)
                {
                    Debug.Log(c.transform.name);
                }
            }
        }

        [ContextMenu("DebugLog Tiles With Candies")]
        public void DebugLogTilesWithCandies()
        {
            foreach(TileSlot tile in CurrentTiles) 
            {
                if (tile.AssociatedCandy != null)
                {
                    Debug.Log($"{tile.transform.name} : {tile.AssociatedCandy.transform.name}");
                }
                else
                {
                    Debug.Log($"{tile.transform.name} : candy is null");
                }
            }
        }
#endif
        #endregion
    }
}