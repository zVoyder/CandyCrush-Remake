namespace CandyCrushREM.Managers.Phases
{
    using Extension.Patterns.StateMachine;
    using System;
    using UnityEngine;

    public class CheckGameConditionPhase : State
    {
        /// <inheritdoc/>
        public CheckGameConditionPhase(string name) : base(name)
        {
        }

        /// <inheritdoc/>
        public override void Enter()
        {
            if (GameManager.Instance.ScoreManager.IsScoreAchieved)
            {
                GameManager.Instance.UIManager.ShowLevelCompletedPanel();
                return;
            }

            if (GameManager.Instance.SwapManager.AreMovesOver)
            {
                GameManager.Instance.UIManager.ShowGameoverPanel();
                return;
            }

            GameManager.Instance.PhaseManager.NextState();
        }

        /// <inheritdoc/>
        public override void Exit()
        {   
        }

        /// <inheritdoc/>
        public override void Process()
        {
        }
    }
}