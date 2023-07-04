using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackCState : AttackState {
    public StoneGolemAttackCState(Entity entity) : base(entity) {
    }

    public override void OnStateEnter() { 
        Debug.Log("AttackC");
    }
    public override void OnStateUpdate() {
    }
    public override void OnStateExit() { }
}
