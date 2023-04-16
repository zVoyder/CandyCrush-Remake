namespace Extension.Patterns.StateMachine
{
    using System.Collections.Generic;
    using System.Collections;
    using UnityEngine;

    public class TurnStateMachine : StateMachine
    {
        public virtual void NextState()
        {
            int nextStateKey = GetNextStateKey();
            ChangeState(nextStateKey);
        }

        public virtual void PreviousState()
        {
            int prevStateKey = GetPreviousStateKey();
            ChangeState(prevStateKey);
        }

        public void  ChangeStateIn(int nextStateKey, float timeToWait)
        {
            StartCoroutine(WaitSecondsAndGoToStateRoutine(nextStateKey, timeToWait));
        }

        public void NextStateIn(float timeToWait)
        {
            int nextStateKey = GetNextStateKey();
            StartCoroutine(WaitSecondsAndGoToStateRoutine(nextStateKey, timeToWait));
        }

        public void PreviousStateIn(float timeToWait)
        {
            int prevStateKey = GetPreviousStateKey();
            StartCoroutine(WaitSecondsAndGoToStateRoutine(prevStateKey, timeToWait));
        }

        private int GetNextStateKey()
        {
            int nextStateKey = CurrentStateKey + 1;

            if (nextStateKey >= States.Count)
                nextStateKey = 0;

            return nextStateKey;
        }

        private int GetPreviousStateKey()
        {
            int prevStateKey = CurrentStateKey - 1;

            if (prevStateKey < 0)
                prevStateKey = States.Count - 1;

            return prevStateKey;
        }

        private IEnumerator WaitSecondsAndGoToStateRoutine(int stateKey, float time)
        {
            yield return new WaitForSeconds(time);
            ChangeState(stateKey);
        }
    }
}