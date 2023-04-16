namespace Extension.Interfaces
{
    internal interface IEventState
    {
        public void Enter();

        public void Exit();

        public void Process();
    }
}