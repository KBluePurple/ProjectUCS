using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemMoveState : MoveState {
    public StoneGolemMoveState(Entity entity) : base(entity) {
    }

    public override void OnStateEnter() {
        Debug.Log("Move");
    }
    public override void OnStateUpdate() {
    }
    public override void OnStateExit() { }
}
