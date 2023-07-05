using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemHealCondition : StoneGolemBaseCondition, ISkillCondition {
    public StoneGolemHealCondition(BossStoneGolem bossStoneGolem, float cooldownDuration) : base(bossStoneGolem, cooldownDuration) { }

    public bool CanSkill() {
        if (IsCooldownReady()) {
            // 자가 회복
            float randomValue = Random.Range(0f, 10f);

            // 25%
            if (randomValue < 2.5f) {
                return true;
            }
            StartCooldown();
            return false;
        }

        return false;
    }
}
