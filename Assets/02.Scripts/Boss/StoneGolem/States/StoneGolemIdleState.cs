using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoneGolemIdleState : IdleState {
    public StoneGolemIdleState(Entity entity) : base(entity) { }

    public override void OnStateEnter() {
        Debug.Log("Idle");
    }
    public override void OnStateUpdate() {
        if (InputSystem.GetDevice<Keyboard>().qKey.wasPressedThisFrame) {
            BossStoneGolem bossStoneGolem = _entity as BossStoneGolem;
            bossStoneGolem.SetNextState(BossStoneGolem.StateType.Move);
        }
    }
    public override void OnStateExit() { }
}
