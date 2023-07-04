using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemDieState : DieState {
    public StoneGolemDieState(Entity entity) : base(entity) {
    }

    public override void OnStateEnter() { 
        Debug.Log("Die");
    }
    public override void OnStateUpdate() {
    }
    public override void OnStateExit() { }
}
