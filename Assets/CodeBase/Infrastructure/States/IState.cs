namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitable
    {
        void Enter();
    }

    public interface IPayloadState<TPayload> : IExitable
    {
        void Enter(TPayload payload);
    }

    public interface IExitable
    {
        void Exit();
    }
}