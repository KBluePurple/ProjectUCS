using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoneGolemIdleState : IdleState {
    private BossStoneGolem _bossStoneGolem;

    public StoneGolemIdleState(Entity entity) : base(entity) {
        _bossStoneGolem = entity as BossStoneGolem;
    }

    public override void OnStateEnter() {
        Debug.Log("Idle");
    }
    public override void OnStateUpdate() {
        if (_bossStoneGolem.Target != null) {
            if (_bossStoneGolem.MoveCooldownTimer.IsCooldownReady()) {
                _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Move);
                _bossStoneGolem.MoveCooldownTimer.StartCooldown();
                return;
            }
        }

        if (_bossStoneGolem.AttackACondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackA);
            return;
        }

        if (_bossStoneGolem.AttackBCondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackB);
            return;
        }

        if (_bossStoneGolem.AttackCCondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackC);
            return;
        }
    }
    public override void OnStateExit() { }
}
