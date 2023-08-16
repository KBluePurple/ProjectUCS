using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemHealState : StoneGolemBaseState {
    private CooldownTimer _healExecutionTimer;

    public StoneGolemHealState(Entity entity) : base(entity) { }

    public override void OnStateEnter() {
        Debug.Log("Heal");

        // TODO: 방어태세 지속시간 몇?
        // 방어태세면 다른 공격 할 수 잇는지?
        _healExecutionTimer = new CooldownTimer(5f);
        _healExecutionTimer.StartCooldown();

        _bossStoneGolem.StoneGolemHealthSystem.Heal();
    }

    public override void OnStateUpdate() {
        // Todo: 받은 데미지만큼 체력 회복
        // 타이머 추가

        _healExecutionTimer?.UpdateTimer();

        if (_healExecutionTimer.IsCooldownReady()) {
            _bossStoneGolem.StoneGolemHealthSystem.EndHeal();
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
        }
    }

    public override void OnStateExit() { }
}
