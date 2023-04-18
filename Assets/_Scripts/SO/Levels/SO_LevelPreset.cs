namespace CandyCrushREM.SO
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu]
    public class SO_LevelPreset : ScriptableObject
    {
        [System.Serializable]
        public class CandyRow
        {
            public SO_Candy[] candies;
        }
        public int maxMoves;
        public int scoreToAchieve;
        public Vector2Int gridSize => new Vector2Int(candieRows[0].candies.Length, candieRows.Length);
        public CandyRow[] candieRows;
    }
}