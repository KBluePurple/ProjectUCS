using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackBCondition : CooldownTimer, ICondition {
    protected BossStoneGolem _bossStoneGolem;

    public StoneGolemAttackBCondition(BossStoneGolem bossStoneGolem, float cooldownDuration) : base(cooldownDuration) {
        _bossStoneGolem = bossStoneGolem;
    }

    public bool CanAttack() {
        if (IsCooldownReady()) {
            // 원거리 공격
            int randomValue = Random.Range(0, 10);

            // 70%
            if (randomValue < 7) {
                return true;
            }
            StartCooldown();
            return false;
        }
            
        return false;
    }
}
