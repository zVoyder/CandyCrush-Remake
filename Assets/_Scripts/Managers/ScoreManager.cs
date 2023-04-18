namespace CandyCrushREM.Managers
{
    using System;
    using UnityEngine;

    public class ScoreManager : MonoBehaviour
    {
        [field: SerializeField]
        public int ScorePerCandy { get; private set; }

        public int ScoreToAchieve { get; set; }
        public Action<int, int> OnScoreChange { get; set; }
        public bool IsScoreAchieved { get; private set; }
        public int CurrentScore { get; private set; }

        public void AddScore(int numberOfCandies)
        {
            CurrentScore += ScorePerCandy * numberOfCandies;

            if(CurrentScore >= ScoreToAchieve)
            {
                CurrentScore = ScoreToAchieve;
                IsScoreAchieved = true;
            }

            OnScoreChange?.Invoke(CurrentScore, ScoreToAchieve);
        }
    }
}