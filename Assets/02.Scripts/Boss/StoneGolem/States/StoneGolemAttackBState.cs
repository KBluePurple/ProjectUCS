using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackBState : AttackState {
    private BossStoneGolem _bossStoneGolem;
    private CooldownTimer _idleTransitionTimer;
    private CooldownTimer _attackCooldownTimer;

    private Entity _target;

    public StoneGolemAttackBState(Entity entity) : base(entity) {
        _bossStoneGolem = entity as BossStoneGolem;
    }

    public override void OnStateEnter() {
        Debug.Log("AttackB");

        _target = _bossStoneGolem.Target;

        _bossStoneGolem.LookTarget();

        _bossStoneGolem.AttackACondition.Delay();
        _bossStoneGolem.AttackBCondition.StartCooldown();
        _bossStoneGolem.AttackCCondition.Delay();

        _attackCooldownTimer = new CooldownTimer(0.8f);
        _attackCooldownTimer.StartCooldown();

        _idleTransitionTimer = new CooldownTimer(2f);
        _idleTransitionTimer.StartCooldown();

        _bossStoneGolem.Animator.SetTrigger(BossStoneGolem.StateType.AttackB.ToString());
    }
    public override void OnStateUpdate() {
        _attackCooldownTimer?.UpdateTimer();
        _idleTransitionTimer?.UpdateTimer();

        if (_attackCooldownTimer != null && _attackCooldownTimer.IsCooldownReady()) {
            _attackCooldownTimer = null;

            // 원거리 공격
            _bossStoneGolem.StoneGolemAttackB.Attack(_target);
        }

        // 일정 시간 지나면 Idle  
        if (_idleTransitionTimer.IsCooldownReady()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
        }
    }
    public override void OnStateExit() { }
}

