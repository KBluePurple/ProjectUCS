using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemHealState : StoneGolemBaseState {
    private CooldownTimer _healExecutionTimer;

    public StoneGolemHealState(Entity entity) : base(entity) { }

    public override void OnStateEnter() {
        Debug.Log("Heal");

        // TODO: ����¼� ���ӽð� ��?
        // ����¼��� �ٸ� ���� �� �� �մ���?
        _healExecutionTimer = new CooldownTimer(5f);
        _healExecutionTimer.StartCooldown();

        _bossStoneGolem.StoneGolemHealthSystem.Heal();
    }

    public override void OnStateUpdate() {
        // Todo: ���� ��������ŭ ü�� ȸ��
        // Ÿ�̸� �߰�

        _healExecutionTimer?.UpdateTimer();

        if (_healExecutionTimer.IsCooldownReady()) {
            _bossStoneGolem.StoneGolemHealthSystem.EndHeal();
            _bossStoneGolem.SetNextState(BossStoneGolem.StateType.Idle);
        }
    }

    public override void OnStateExit() { }
}
