using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackBState : AttackState {
    private BossStoneGolem _bossStoneGolem;
    private CooldownTimer _idleTransitionTimer;

    public StoneGolemAttackBState(Entity entity) : base(entity) {
        _bossStoneGolem = entity as BossStoneGolem;
    }

    public override void OnStateEnter() {
        Debug.Log("AttackB");
        _idleTransitionTimer = new CooldownTimer(2f);
        _idleTransitionTimer.StartCooldown();

        // ���Ÿ� ����
        _bossStoneGolem.StoneGolemAttackB.Attack(_bossStoneGolem.Target);
    }
    public override void OnStateUpdate() {
        _idleTransitionTimer?.UpdateTimer();

        // ���� �ð� ������ Idle  
        if (_idleTransitionTimer.IsCooldownReady()) {
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
        }
    }
    public override void OnStateExit() { }
}

