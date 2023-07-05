using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemBaseCondition : CooldownTimer {
    protected BossStoneGolem _bossStoneGolem;

    public StoneGolemBaseCondition(BossStoneGolem bossStoneGolem, float cooldownDuration) : base(cooldownDuration) {
        _bossStoneGolem = bossStoneGolem;
    }
}
