public abstract class BaseState
{
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateLateUpdate();
    public abstract void OnStateExit();
}