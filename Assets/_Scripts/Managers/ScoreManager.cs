namespace CandyCrushREM.Managers
{
    using System;
    using UnityEngine;

    public class ScoreManager : MonoBehaviour
    {
        public int ScoreToAchieve { get; private set; }
        public Action<int, int> OnScoreChange { get; set; }

        [field: SerializeField]
        public int ScorePerCandy { get; private set; }

        public bool IsScoreAchieved { get; private set; }
        public int CurrentScore { get; private set; }

        public void Init(int scoreToAchieve)
        {
            ScoreToAchieve = scoreToAchieve;
        }

        public void AddScore(int numberOfCandies)
        {
            CurrentScore += ScorePerCandy * numberOfCandies;

            if (CurrentScore >= ScoreToAchieve)
            {
                CurrentScore = ScoreToAchieve;
                IsScoreAchieved = true;
            }

            OnScoreChange?.Invoke(CurrentScore, ScoreToAchieve);
        }
    }
}