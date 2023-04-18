namespace CandyCrushREM.Managers
{
    using CandyCrushREM.Candies;
    using CandyCrushREM.Managers;
    using CandyCrushREM.Managers.Phases;
    using Extension.Patterns.StateMachine;
    using UnityEngine;

    public class PhaseManager : TurnStateMachine
    {
        [field: SerializeField]
        public float FallPhaseDuration { get; private set; } = 1f;
        [field: SerializeField]
        public float RefillPhaseDuration { get; private set; } = 1f;

        protected override void InitStates()
        {
            base.InitStates();
            States.Add(new SwapPhase("SwapPhase", .2f));
            States.Add(new ComboPhase("ComboPhase"));
            States.Add(new FallPhase("FallPhase", FallPhaseDuration));
            States.Add(new RefillPhase("RefillPhase", RefillPhaseDuration));
            States.Add(new CheckGameConditionPhase("CheckGame"));
        }

        public void Begin()
        {
            ChangeState(0);
        }
    }
}