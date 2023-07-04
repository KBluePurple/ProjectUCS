using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackBState : AttackState {
    public StoneGolemAttackBState(Entity entity) : base(entity) {
    }

    public override void OnStateEnter() {
        Debug.Log("AttackB");
    }
    public override void OnStateUpdate() {
    }
    public override void OnStateExit() { }
}

