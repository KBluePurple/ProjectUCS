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
        // Todo: 받은 데미지만큼 체력 회복
        // 타이머 추가
    }

    public override void OnStateExit() { }
}
