using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : Entity
{
    [SerializeField]
    private HealthBarUI _healthBarUI = null;

    public override void Init()
    {
        base.Init();
        _healthSystem.Init(this, 1000f);
        _healthBarUI.Init(_healthSystem);
    }

    public void Damage(float damage)
    {
        
    }

    public override void Die()
    {
        
    }
}
