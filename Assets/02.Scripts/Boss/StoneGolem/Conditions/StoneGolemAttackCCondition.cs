using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackCCondition : CooldownTimer, IAttackCondition {
    protected BossStoneGolem _bossStoneGolem;

    public StoneGolemAttackCCondition(BossStoneGolem bossStoneGolem, float cooldownDuration) : base(cooldownDuration) {
        _bossStoneGolem = bossStoneGolem;
    }

    public bool CanAttack() {
        if (IsCooldownReady()) {
            return false;
        }

        return false;
    }
}
