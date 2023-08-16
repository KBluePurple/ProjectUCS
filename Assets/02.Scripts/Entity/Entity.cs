using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {
    public HealthSystem HealthSystem => _healthSystem;
    protected HealthSystem _healthSystem;

    private void Awake() {
        Init();
    }

    public virtual void Init() {
        _healthSystem = GetComponent<HealthSystem>();
    }

    public abstract void Die();
}
