using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState {
    public BaseState() { }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateLateUpdate();
    public abstract void OnStateExit();
}
