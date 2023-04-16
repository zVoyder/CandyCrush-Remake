namespace Extension.Patterns.StateMachine
{
    using Extension.Interfaces;

    public abstract class State : IEventState
    {
        public string StateName { get; private set; }

        protected State(string name)
        {
            StateName = name;
        }

        public abstract void Enter();

        public abstract void Exit();

        public abstract void Process();
    }
}