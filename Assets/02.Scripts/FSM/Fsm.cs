public class Fsm
{
    private BaseState _curState;

    public Fsm(BaseState initState)
    {
        _curState = initState;
        ChangeState(_curState);
    }

    public void ChangeState(BaseState nextState)
    {
        if (_curState == nextState)
            return;

        _curState?.OnStateExit();

        _curState = nextState;
        _curState.OnStateExit();
    }

    public void UpdateState()
    {
        _curState?.OnStateUpdate();
    }

    public void LateUpdateState()
    {
        _curState?.OnStateLateUpdate();
    }
}