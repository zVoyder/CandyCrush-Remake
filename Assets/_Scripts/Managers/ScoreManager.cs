namespace CandyCrushREM.Managers
{
    using System;
    using Unity.VisualScripting;
    using UnityEngine;
    using static UnityEditor.Progress;
    using UnityEngine.SocialPlatforms.Impl;

    public class ScoreManager : MonoBehaviour
    {
        public int ScoreToAchieve { get; private set; }
        public Action<int, int> OnScoreChange { get; set; }

        [field: SerializeField]
        public int ScorePerItem { get; private set; }

        public bool IsScoreAchieved { get; private set; }
        public int CurrentScore { get; private set; }

        /// <summary>
        /// Initilizes the score.
        /// </summary>
        /// <param name="scoreToAchieve">Score to achieve.</param>
        public void Init(int scoreToAchieve)
        {
            ScoreToAchieve = scoreToAchieve;
        }

        /// <summary>
        /// Adds score by multiplying the ScorePerItem and the number of items.
        /// </summary>
        /// <param name="itemsNumber">items number.</param>
        public void AddScore(int itemsNumber)
        {
            CurrentScore += ScorePerItem * itemsNumber;

            if (CurrentScore >= ScoreToAchieve)
            {
                CurrentScore = ScoreToAchieve;
                IsScoreAchieved = true;
            }

            OnScoreChange?.Invoke(CurrentScore, ScoreToAchieve);
        }
    }
}