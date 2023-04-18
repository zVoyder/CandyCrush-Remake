namespace CandyCrushREM.Managers.Phases
{
    using Extension.Patterns.StateMachine;
    using UnityEngine;

    public class ComboPhase : State
    {
        public ComboPhase(string name) : base(name)
        {
        }

        public override void Enter()
        {
            Debug.Log("ENTERING DESTROY");

            GameManager.Instance.ComboManager.DestroyAllCombos(GameManager.Instance.CurrentTiles);
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