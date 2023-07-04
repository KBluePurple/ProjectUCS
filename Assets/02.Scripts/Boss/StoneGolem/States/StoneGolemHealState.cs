using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemHealState : BaseState {
    public StoneGolemHealState(Entity entity) : base(entity) {
    }

    public override void OnStateEnter() {
        Debug.Log("Heal");
    }
    public override void OnStateUpdate() {
    }
    public override void OnStateExit() { }
}
