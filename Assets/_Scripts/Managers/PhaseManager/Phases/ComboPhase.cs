namespace CandyCrushREM.Managers.Phases
{
    using Extension.Patterns.StateMachine;
    using UnityEngine;

    public class ComboPhase : State
    {
        /// <inheritdoc/>
        public ComboPhase(string name) : base(name)
        {
        }

        /// <inheritdoc/>
        public override void Enter()
        {
#if DEBUG
            Debug.Log("ENTERING DESTROY");
#endif
            GameManager.Instance.ComboManager.DestroyAllCombos(GameManager.Instance.CurrentTiles);
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