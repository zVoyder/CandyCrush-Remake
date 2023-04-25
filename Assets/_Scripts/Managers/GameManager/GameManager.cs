namespace CandyCrushREM.Managers
{
    using System.Collections.Generic;
    using CandyCrushREM.Candies;
    using CandyCrushREM.SO;
    using Extension.Patterns.Singleton;
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
            SwapManager.Init(Level.maxMoves);
            ScoreManager.Init(Level.scoreToAchieve);
            UIManager.InitUI();

            Grid.Init(Level);
            CurrentTiles = Grid.GenerateGrid();
            PhaseManager.Begin();
        }
    }
}