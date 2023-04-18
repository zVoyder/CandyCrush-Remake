namespace CandyCrushREM.Managers.Phases
{
    using Extension.Patterns.StateMachine;
    using System;
    using UnityEngine;

    public class CheckGameConditionPhase : State
    {
        public CheckGameConditionPhase(string name) : base(name)
        {
        }

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

        public override void Exit()
        {   
        }

        public override void Process()
        {
        }
    }
}