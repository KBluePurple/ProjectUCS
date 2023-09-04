using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackCState : StoneGolemBaseState {
    private CooldownTimer _attackCooldownTimer;
    private CooldownTimer _attackExecutionTimer;
    private CooldownTimer _idleTransitionTimer;

    private GameObject _target;
    public StoneGolemAttackCState(Entity entity) : base(entity) { }

    public override void OnStateEnter() {
        Debug.Log("AttackC");

        _target = _bossStoneGolem.Target;

        if (_target == null) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
            return;
        }

        _bossStoneGolem.LookTarget();

        _bossStoneGolem.AttackACondition.Delay();
        _bossStoneGolem.AttackBCondition.Delay();
        _bossStoneGolem.AttackCCondition.StartCooldown();

        _attackCooldownTimer = new CooldownTimer(0.5f);
        _attackCooldownTimer.StartCooldown();

        _idleTransitionTimer = new CooldownTimer(7f);
        _idleTransitionTimer.StartCooldown();

        _bossStoneGolem.Animator.SetTrigger(BossStoneGolem.StateType.AttackC.ToString());
    }
    public override void OnStateUpdate() {
        _attackCooldownTimer?.UpdateTimer();
        _attackExecutionTimer?.UpdateTimer();
        _idleTransitionTimer?.UpdateTimer();

        if (_attackCooldownTimer != null && _attackCooldownTimer.IsCooldownReady()) {
            _attackCooldownTimer = null;
            _bossStoneGolem.StoneGolemAttackC.Attack(_target);

            _attackExecutionTimer = new CooldownTimer(5f);
            _attackExecutionTimer.StartCooldown();
        }

        if (_attackExecutionTimer != null && _attackExecutionTimer.IsCooldownReady()) {
            _attackExecutionTimer = null;
            _bossStoneGolem.StoneGolemAttackC.EndAttack();
            _bossStoneGolem.Animator.SetTrigger("End" + BossStoneGolem.StateType.AttackC.ToString());
        }

        if (_idleTransitionTimer != null && _idleTransitionTimer.IsCooldownReady()) {
            // 레이저 공격 
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
            return;
        }
    }
    public override void OnStateExit() { }
}
