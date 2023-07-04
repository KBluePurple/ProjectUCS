using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackBState : AttackState {
    private BossStoneGolem _bossStoneGolem; 
    public StoneGolemAttackBState(Entity entity) : base(entity) {
        _bossStoneGolem = entity as BossStoneGolem;
    }

    public override void OnStateEnter() {
        Debug.Log("AttackB");
    }
    public override void OnStateUpdate() {
        // ���Ÿ� ���� 
        _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
    }
    public override void OnStateExit() { }
}

