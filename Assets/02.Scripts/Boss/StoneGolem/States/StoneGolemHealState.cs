using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemHealState : BaseState {
    private BossStoneGolem _bossStoneGolem;

    public StoneGolemHealState(Entity entity) : base(entity) {
        _bossStoneGolem = entity as BossStoneGolem;
    }

    public override void OnStateEnter() {
        Debug.Log("Heal");
    }

    public override void OnStateUpdate() {
        // Todo: ���� ��������ŭ ü�� ȸ��
        // Ÿ�̸� �߰�
    }

    public override void OnStateExit() { }
}
