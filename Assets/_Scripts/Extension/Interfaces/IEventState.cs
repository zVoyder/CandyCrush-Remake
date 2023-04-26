﻿namespace Extension.Interfaces
{
    internal interface IEventState
    {
        /// <summary>
        /// On Enter event state.
        /// </summary>
        public void Enter();

        /// <summary>
        /// On Exit event state.
        /// </summary>
        public void Exit();

        /// <summary>
        /// On Processing event state.
        /// </summary>
        public void Process();
    }
}