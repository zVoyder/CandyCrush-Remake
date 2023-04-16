namespace CandyCrushREM.SO
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    using static CandyCrushREM.SO.LevelPreset;

    [System.Serializable]
    public struct CandyRow
    {
        public SO_Candy[] candies;
    }

    [CreateAssetMenu]
    public class LevelPreset : ScriptableObject
    {
        public Vector2Int GridSize => new Vector2Int(CandieRows[0].candies.Length, CandieRows.Length);

        public CandyRow[] CandieRows;
    }
}