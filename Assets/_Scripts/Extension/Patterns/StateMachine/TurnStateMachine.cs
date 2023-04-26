namespace Extension.Patterns.StateMachine
{
    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;

    public class TurnStateMachine : StateMachine
    {
        /// <summary>
        /// Goes to the next state following the order of the states list addition.
        /// Goes back to first state if there is not a subsequent state.
        /// </summary>
        public virtual void NextState()
        {
            int nextStateKey = GetNextStateKey();
            ChangeState(nextStateKey);
        }

        /// <summary>
        /// Goes to the previous state following the order of the states list addition.
        /// Goes to last state if there is not a previous state.
        /// </summary>
        public virtual void PreviousState()
        {
            int prevStateKey = GetPreviousStateKey();
            ChangeState(prevStateKey);
        }

        /// <summary>
        /// Waits N seconds before changing state.
        /// </summary>
        /// <param name="stateKey">State key.</param>
        /// <param name="timeToWait">Time to wait in seconds.</param>
        public void ChangeStateIn(int stateKey, float timeToWait)
        {
            StartCoroutine(WaitSecondsAndGoToStateRoutine(stateKey, timeToWait));
        }

        /// <summary>
        /// Waits N seconds before going to the next state.
        /// </summary>
        /// <param name="timeToWait">Time to wait in seconds.</param>
        public void NextStateIn(float timeToWait)
        {
            int nextStateKey = GetNextStateKey();
            StartCoroutine(WaitSecondsAndGoToStateRoutine(nextStateKey, timeToWait));
        }

        /// <summary>
        /// Waits N seconds before going to the previous state.
        /// </summary>
        /// <param name="timeToWait"></param>
        public void PreviousStateIn(float timeToWait)
        {
            int prevStateKey = GetPreviousStateKey();
            StartCoroutine(WaitSecondsAndGoToStateRoutine(prevStateKey, timeToWait));
        }

        /// <summary>
        /// Gets the next state key.
        /// </summary>
        /// <returns>Next state key.</returns>
        private int GetNextStateKey()
        {
            int nextStateKey = CurrentStateKey + 1;

            if (nextStateKey >= States.Count)
                nextStateKey = 0;

            return nextStateKey;
        }

        /// <summary>
        /// Gets the previous state key.
        /// </summary>
        /// <returns>Previous state key.</returns>
        private int GetPreviousStateKey()
        {
            int prevStateKey = CurrentStateKey - 1;

            if (prevStateKey < 0)
                prevStateKey = States.Count - 1;

            return prevStateKey;
        }

        /// <summary>
        /// Coroutine wait before changing state.
        /// </summary>
        /// <param name="stateKey">State Key.</param>
        /// <param name="time">Time in Seconds.</param>
        /// <returns></returns>
        private IEnumerator WaitSecondsAndGoToStateRoutine(int stateKey, float time)
        {
            yield return new WaitForSeconds(time);
            ChangeState(stateKey);
        }
    }
}