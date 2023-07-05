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
            return false;
        }

        return false;
    }
}
