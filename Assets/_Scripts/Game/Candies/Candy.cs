namespace CandyCrushREM.Candies
{
    using CandyCrushREM.SO;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    public class Candy : MonoBehaviour
    {
        public SO_Candy SO_Candy { get; private set; }
        private SpriteRenderer _spriteRender;

        public SO_Candy.CandyType CandyType => SO_Candy.type;

        private void Awake()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
        }

        public void Init(SO_Candy soCandy)
        {
            transform.name = $"Candy (ID {GetInstanceID()})";
            SO_Candy = soCandy;
            _spriteRender.sprite = SO_Candy.spriteImage;
        }
    }
}