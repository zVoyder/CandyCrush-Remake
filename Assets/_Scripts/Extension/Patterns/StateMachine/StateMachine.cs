namespace Extension.Patterns.StateMachine
{
    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;

    public class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; private set; }

        public int CurrentStateKey { get; protected set; } = 0;

        protected List<State> States { get; set; }

        protected virtual void InitStates()
        {
            States = new List<State>();
        }

        protected virtual void Awake()
        {
            InitStates();
        }

        protected virtual void Start()
        {
            CurrentState?.Enter();
        }

        protected virtual void Update()
        {
            CurrentState?.Process();
        }

        public void ChangeState(int stateKey)
        {
            if (States[stateKey] != CurrentState)
            {
                CurrentState?.Exit();
                CurrentStateKey = stateKey;
                CurrentState = States[stateKey];
                CurrentState?.Enter();
            }
        }
    }
}