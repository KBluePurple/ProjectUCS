using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable, IHealthSystem {
    public float Health => _health;

    private Entity _entity;

    private float _health;
    private float _maxHealth;

    public void Init(Entity entity, float maxHealth = 0f) {
        _entity = entity;
        _health = _maxHealth = maxHealth;
    }

    public float GetNormalizedHealth() => _health / _maxHealth;

    public void TakeDamage(float damageAmount) {
        _health = Mathf.Clamp(_health - damageAmount, 0, _maxHealth);

        Debug.Log(_health);

        if (_health <= 0f) {
            _entity.Die();
        }
    }
}
