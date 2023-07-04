using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackAState : AttackState {
    public StoneGolemAttackAState(Entity entity) : base(entity) {
    }

    public override void OnStateEnter() {
        Debug.Log("AttackA");
    }
    public override void OnStateUpdate() {
    }
    public override void OnStateExit() { }
}
