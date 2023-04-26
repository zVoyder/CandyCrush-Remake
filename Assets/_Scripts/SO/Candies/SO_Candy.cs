namespace CandyCrushREM.SO
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu]
    public class SO_Candy : ScriptableObject
    {
        public enum CandyType
        { 
            RED,
            ORANGE,
            YELLOW,
            GREEN,
            BLUE,
            PURPLE
        }

        public CandyType type;
        public Sprite spriteImage;
    }
}