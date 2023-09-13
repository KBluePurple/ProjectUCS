using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoneGolemIdleState : StoneGolemBaseState {
    public StoneGolemIdleState(Entity entity) : base(entity) { }

    public override void OnStateEnter() {
        //Debug.Log("Idle");

        _bossStoneGolem.Animator.SetBool("isMove", false);
    }
    public override void OnStateUpdate() {
        if (_bossStoneGolem.Target != null) {
            if (_bossStoneGolem.MoveCooldownTimer.IsCooldownReady()) {
                _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Move);
                _bossStoneGolem.MoveCooldownTimer.StartCooldown();
                return;
            }
        }

        _bossStoneGolem.AttackACondition?.UpdateTimer();
        if (_bossStoneGolem.AttackACondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackA);
            return;
        }

        _bossStoneGolem.AttackBCondition?.UpdateTimer();
        if (_bossStoneGolem.AttackBCondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackB);
            return;
        }

        _bossStoneGolem.AttackCCondition?.UpdateTimer();
        if (_bossStoneGolem.AttackCCondition.CanAttack()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.AttackC);
            return;
        }

        _bossStoneGolem.HealCondition?.UpdateTimer();
        if (_bossStoneGolem.HealCondition.CanSkill()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Heal);
            return;
        }
    }
    public override void OnStateExit() { }
}
