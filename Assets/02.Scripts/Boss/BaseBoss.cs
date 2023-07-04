using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBoss : Entity {
    protected HealthSystem _healthSystem;
    protected Fsm _fsm;

    protected int _phaseIndex;

    private void Awake() {
        Init();
    }

    public void Init() {
        _healthSystem = GetComponent<HealthSystem>();

        _healthSystem.Init();
    }

    public abstract void Die();
}
