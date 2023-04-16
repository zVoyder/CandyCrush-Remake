namespace CandyCrushREM.Managers.Phases
{
    using Extension.Patterns.StateMachine;
    using UnityEngine;

    public class RefillPhase : State
    {
        public float SecondsBeforeNextState { get; private set; }


        public RefillPhase(string name, float secondsBeforeNextState) : base(name)
        {
            SecondsBeforeNextState = secondsBeforeNextState;
        }

        public override void Enter()
        {
            Debug.Log("ENTERING REFILLPHASE");
            GameManager.Instance.FallManager.RefillGridWithCandies(GameManager.Instance.CurrentTiles);

            if(GameManager.Instance.ComboManager.GetAllCombinations(GameManager.Instance.CurrentTiles).Count > 0)
            {
                GameManager.Instance.PhaseManager.ChangeStateIn(1, SecondsBeforeNextState);
            }
            else
            {
                GameManager.Instance.PhaseManager.NextState();
            }
        }

        public override void Exit()
        {
        }

        public override void Process()
        {
        }
    }
}