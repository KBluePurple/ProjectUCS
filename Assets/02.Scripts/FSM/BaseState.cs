public abstract class BaseState {
    protected Entity _entity;
    public BaseState(Entity entity) {
        _entity = entity;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}