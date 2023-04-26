namespace CandyCrushREM.Managers.Phases
{
    using Extension.Patterns.StateMachine;
    using UnityEngine;

    public class RefillPhase : State
    {
        public float SecondsBeforeNextState { get; private set; }

        /// <summary>
        /// Initializes a new instance of the FallPhase class with the specified name and duration of the falling.
        /// </summary>
        /// <param name="name">Name of the state.</param>
        /// <param name="secondsBeforeNextState">Time to wait before going to the next state in seconds.</param>
        public RefillPhase(string name, float secondsBeforeNextState) : base(name)
        {
            SecondsBeforeNextState = secondsBeforeNextState;
        }

        /// <inheritdoc/>
        public override void Enter()
        {
#if DEBUG
            Debug.Log("ENTERING REFILLPHASE");
#endif
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