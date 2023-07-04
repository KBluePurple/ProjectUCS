using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM {
    private BaseState _curState = null;

    public FSM(BaseState initState) {
        _curState = initState;
        ChangeState(_curState);
    }

    public void ChangeState(BaseState nextState) {
        if (_curState == nextState)
            return;

        _curState?.OnStateExit();

        _curState = nextState;
        _curState.OnStateExit();
    }

    public void UpdateState() => _curState?.OnStateUpdate();

    public void LateUpdateState() => _curState?.OnStateLateUpdate();
}
