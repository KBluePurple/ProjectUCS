using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    public float Health => _health;

    private float _health;
    private float _maxHealth;

    public void Init(float maxHealth = 0f) {
        _health = _maxHealth = maxHealth;
    }

    public float GetNormalizedHealth() => _health / _maxHealth;
}
