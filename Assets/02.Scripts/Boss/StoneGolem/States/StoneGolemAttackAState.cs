using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackAState : AttackState {
    private BossStoneGolem _bossStoneGolem;
    public StoneGolemAttackAState(Entity entity) : base(entity) {
        _bossStoneGolem = entity as BossStoneGolem;
    }

    public override void OnStateEnter() {
        Debug.Log("AttackA");
    }   

    public override void OnStateUpdate() {
        // 근접 공격 
        _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
    }

    public override void OnStateExit() { }
}
