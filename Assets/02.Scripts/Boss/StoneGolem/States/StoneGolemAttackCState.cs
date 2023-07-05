using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackCState : AttackState {
    private BossStoneGolem _bossStoneGolem;
    public StoneGolemAttackCState(Entity entity) : base(entity) {
        _bossStoneGolem = entity as BossStoneGolem;
    }

    public override void OnStateEnter() {
        Debug.Log("AttackC");

        _bossStoneGolem.LookTarget();

        _bossStoneGolem.AttackACondition.Delay();
        _bossStoneGolem.AttackBCondition.Delay();
        _bossStoneGolem.AttackCCondition.StartCooldown();
    }
    public override void OnStateUpdate() {
        // 레이저 공격 
        _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
    }
    public override void OnStateExit() { }
}
