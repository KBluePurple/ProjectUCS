using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemHealthSystem : HealthSystem {

    private bool _isHeal;

    public override void TakeDamage(float damageAmount) {
        if (_isHeal) {
            _health = Mathf.Clamp(_health + damageAmount, 0, _maxHealth);
        }
        else {
            _health = Mathf.Clamp(_health - damageAmount, 0, _maxHealth);
        }

        if (_health <= 0f) {
            _entity.Die();
        }
    }

    public void Heal() {
        _isHeal = true;
    }

    public void EndHeal() {
        _isHeal = false;
    }

}
