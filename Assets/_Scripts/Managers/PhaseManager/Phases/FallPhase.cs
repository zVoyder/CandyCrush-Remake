namespace CandyCrushREM.Managers.Phases
{
    using Extension.Patterns.StateMachine;
    using UnityEngine;

    public class FallPhase : State
    {
        public float SecondsBeforeNextState { get; private set; }

        /// <summary>
        /// Initializes a new instance of the FallPhase class with the specified name and duration of the falling.
        /// </summary>
        /// <param name="name">Name of the state.</param>
        /// <param name="secondsBeforeNextState">Time to wait before going to the next state in seconds.</param>
        public FallPhase(string name, float secondsBeforeNextState) : base(name)
        {
            SecondsBeforeNextState = secondsBeforeNextState;
        }

        /// <inheritdoc/>
        public override void Enter()
        {
#if DEBUG
            Debug.Log("ENTERING FALLPHASE");
#endif
            GameManager.Instance.FallManager.LetCandiesFallFor(GameManager.Instance.CurrentTiles, SecondsBeforeNextState);
            GameManager.Instance.PhaseManager.NextStateIn(SecondsBeforeNextState);
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