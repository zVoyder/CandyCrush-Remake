namespace CandyCrushREM.SO
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [System.Serializable]
    public class CandyRow
    {
        public SO_Candy[] candies;
    }

    [CreateAssetMenu]
    public class SO_LevelPreset : ScriptableObject
    {
        public Vector2Int GridSize => new Vector2Int(CandieRows[0].candies.Length, CandieRows.Length);

        public CandyRow[] CandieRows;
    }
}