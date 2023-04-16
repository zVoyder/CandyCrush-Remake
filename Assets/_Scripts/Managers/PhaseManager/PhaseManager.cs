namespace CandyCrushREM.Managers
{
    using CandyCrushREM.Candies;
    using CandyCrushREM.Managers.Grid;
    using CandyCrushREM.Managers.Phases;
    using Extension.Patterns.StateMachine;
    using UnityEditor.VersionControl;
    using UnityEngine;

    public class PhaseManager : TurnStateMachine
    {
        protected override void InitStates()
        {
            base.InitStates();
            States.Add(new SwapPhase("SwapPhase", .2f));
            States.Add(new DestroyCombosPhase("ComboPhase"));
            States.Add(new FallPhase("FallPhase", 1f));
            States.Add(new RefillPhase("RefillPhase", 1f));
        }

        public void Begin()
        {
            ChangeState(0);
        }
    }
}