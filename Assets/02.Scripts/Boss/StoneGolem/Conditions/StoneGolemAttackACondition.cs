using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemAttackACondition : CooldownTimer, ICondition {
    protected BossStoneGolem _bossStoneGolem;

    private float _attackRange = 2f;

    public StoneGolemAttackACondition(BossStoneGolem bossStoneGolem, float cooldownDuration) : base(cooldownDuration) {
        _bossStoneGolem = bossStoneGolem;
    }

    public bool CanAttack() {
        if (IsCooldownReady() && _bossStoneGolem.Target != null) {

            float distance = Vector2.Distance(_bossStoneGolem.transform.position, _bossStoneGolem.Target.transform.position);

            //근거리 공격
            if (distance <= _attackRange) {
                return true;
            }
        }

        return false;
    }
}
