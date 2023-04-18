namespace CandyCrushREM.Managers
{
    using UnityEngine.UI;
    using UnityEngine;
    using TMPro;
    using System;
    using UnityEngine.SceneManagement;

    public class UIManager : MonoBehaviour
    {
        [field: SerializeField, Header("Win Loose Conditions")]
        public GameObject GameOverPanel { get; private set; }

        [field: SerializeField]
        public GameObject LevelCompletedPanel { get; private set; }

        [field: SerializeField, Header("Score")]
        public TMP_Text ScoreText { get; private set; }

        [field: SerializeField]
        public ScoreManager ScoreManager { get; private set; }

        [field: SerializeField, Header("Remaning Moves")]
        public TMP_Text MovesText { get; private set; }

        [field: SerializeField]
        public SwapManager SwapManager { get; private set; }

        public void InitUI()
        {
            ScoreManager.OnScoreChange += UpdateScoreText;
            UpdateScoreText(ScoreManager.CurrentScore, ScoreManager.ScoreToAchieve);
            SwapManager.OnMovesChange += UpdateMovesText;
            UpdateMovesText(SwapManager.CurrentMoves, SwapManager.MaxMoves);
        }

        public void ShowGameoverPanel()
        {
            GameOverPanel.SetActive(true);
        }

        public void ShowLevelCompletedPanel()
        {
            LevelCompletedPanel.SetActive(true);
        }

        private void UpdateScoreText(int score, int scoreToAchieve)
        {
            ScoreText.text = $"{score.ToString()} / {scoreToAchieve.ToString()}";
        }

        private void UpdateMovesText(int remaningMoves, int maxMoves)
        {
            MovesText.text = $"{remaningMoves.ToString()} / {maxMoves.ToString()}";
        }

    }
}
