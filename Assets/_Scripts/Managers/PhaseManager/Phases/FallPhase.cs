﻿namespace CandyCrushREM.Managers.Phases
{
    using CandyCrushREM.Candies;
    using CandyCrushREM.Managers.Grid;
    using Extension.Patterns.StateMachine;
    using System.Collections;
    using UnityEngine;

    public class FallPhase : State
    {
        public float SecondsBeforeNextState { get; private set; }

        public FallPhase(string name, float secondsBeforeNextState) : base(name)
        {
            SecondsBeforeNextState = secondsBeforeNextState;
        }

        public override void Enter()
        {
            Debug.Log("ENTERING FALLPHASE");
            GameManager.Instance.FallManager.LetCandiesFallFor(SecondsBeforeNextState);
            GameManager.Instance.PhaseManager.NextStateIn(SecondsBeforeNextState);
        }

        public override void Exit()
        {
        }

        public override void Process()
        {
        }
    }
}