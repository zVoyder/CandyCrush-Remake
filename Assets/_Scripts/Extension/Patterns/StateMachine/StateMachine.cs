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

        /// <summary>
        /// Initializes the states list.
        /// </summary>
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

        /// <summary>
        /// Changes the state to a given one.
        /// </summary>
        /// <param name="stateKey">Integer key of the target state.</param>
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