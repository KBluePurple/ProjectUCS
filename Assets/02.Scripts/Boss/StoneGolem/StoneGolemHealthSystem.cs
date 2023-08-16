using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemHealthSystem : HealthSystem, IListener {
    private bool _isHeal = false;
    private bool _isDanDanMukZic = false;

    public override void Init(Entity entity, float maxHealth = 0f) {
        base.Init(entity, maxHealth);

        EventManager.Instance.AddListener(BossStoneGolemEventType.DanDanMukZic, this);
    }

    public override void TakeDamage(float damageAmount) {
        if (_isHeal) {
            _health = Mathf.Clamp(_health + damageAmount, 0, _maxHealth);
        }
        else {
            if (_isDanDanMukZic) {
                damageAmount *= 0.5f;
            }
            _health = Mathf.Clamp(_health - damageAmount, 0, _maxHealth);
        }

        Debug.Log(_health);

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

    public void OnEvent<TEventType>(TEventType eventType, Component sender, object param = null) where TEventType : Enum {
        if (eventType.Equals(BossStoneGolemEventType.DanDanMukZic)) {
            _isDanDanMukZic = true;
        }
    }
}
