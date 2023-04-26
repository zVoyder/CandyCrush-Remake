namespace Extension.Patterns.StateMachine
{
    using Extension.Interfaces;

    public abstract class State : IEventState
    {
        public string StateName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the State class with the specified name.
        /// </summary>
        /// <param name="name">Name of the state.</param>
        protected State(string name)
        {
            StateName = name;
        }

        /// <inheritdoc/>
        public abstract void Enter();

        /// <inheritdoc/>
        public abstract void Exit();

        /// <inheritdoc/>
        public abstract void Process();
    }
}