using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackAState : AttackState {
    private BossStoneGolem _bossStoneGolem;
    private CooldownTimer _attackCooldownTimer;
    private CooldownTimer _attackExecutionTimer;
    private CooldownTimer _idleTransitionTimer;

    public StoneGolemAttackAState(Entity entity) : base(entity) {
        _bossStoneGolem = entity as BossStoneGolem;
    }

    public override void OnStateEnter() {
        Debug.Log("AttackA");
        _attackCooldownTimer = new CooldownTimer(0.4f);
        _attackCooldownTimer.StartCooldown();

        _idleTransitionTimer = new CooldownTimer(2f);
        _idleTransitionTimer.StartCooldown();
    }

    public override void OnStateUpdate() {
        _attackCooldownTimer?.UpdateTimer();
        _attackExecutionTimer?.UpdateTimer();
        _idleTransitionTimer?.UpdateTimer();

        // 근접 공격 
        // 땅을 치면 주변에 데미지 발생
        if (_attackCooldownTimer != null && _attackCooldownTimer.IsCooldownReady()) {
            _attackCooldownTimer = null;
            // 공격 
            // 오브젝트 생성 후 콜라이더로
            _bossStoneGolem.StoneGolemAttackA.Attack();
            _attackExecutionTimer = new CooldownTimer(0.1f);
            _attackExecutionTimer.StartCooldown();
        }

        if (_attackExecutionTimer != null && _attackExecutionTimer.IsCooldownReady()) {
            _attackExecutionTimer = null;
            _bossStoneGolem.StoneGolemAttackA.EndAttack();
        }

        // 일정 시간 지나면 Idle  
        if (_idleTransitionTimer.IsCooldownReady()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
        }
    }

    public override void OnStateExit() { }
}
