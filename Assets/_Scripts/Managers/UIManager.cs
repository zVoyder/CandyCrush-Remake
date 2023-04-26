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

        /// <summary>
        /// Initilizes UI.
        /// </summary>
        public void InitUI()
        {
            ScoreManager.OnScoreChange += UpdateScoreText;
            UpdateScoreText(ScoreManager.CurrentScore, ScoreManager.ScoreToAchieve);
            SwapManager.OnMovesChange += UpdateMovesText;
            UpdateMovesText(SwapManager.CurrentMoves, SwapManager.MaxMoves);
        }

        /// <summary>
        /// Enables the GameOver Panel GameObject.
        /// </summary>
        public void ShowGameoverPanel()
        {
            GameOverPanel.SetActive(true);
        }

        /// <summary>
        /// Enables the Level Completed Panel GameObject.
        /// </summary>
        public void ShowLevelCompletedPanel()
        {
            LevelCompletedPanel.SetActive(true);
        }

        /// <summary>
        /// Updates the score text UI.
        /// </summary>
        /// <param name="score">Score points.</param>
        /// <param name="scoreToAchieve">Score to achieve.</param>
        private void UpdateScoreText(int score, int scoreToAchieve)
        {
            ScoreText.text = $"{score.ToString()} / {scoreToAchieve.ToString()}";
        }

        /// <summary>
        /// Updates the remaing moves text UI.
        /// </summary>
        /// <param name="remaningMoves">Remaining Moves.</param>
        /// <param name="maxMoves">Max allowed moves.</param>
        private void UpdateMovesText(int remaningMoves, int maxMoves)
        {
            MovesText.text = $"{remaningMoves.ToString()} / {maxMoves.ToString()}";
        }

    }
}
