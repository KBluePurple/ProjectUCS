public class Fsm {
    private BaseState _curState = null;

    public Fsm(BaseState initState) => ChangeState(initState);

    public void ChangeState(BaseState nextState) {
        if (_curState == nextState)
            return;

        _curState?.OnStateExit();

        _curState = nextState;
        _curState.OnStateEnter();
    }

    public void UpdateState() => _curState?.OnStateUpdate();
}