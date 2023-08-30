using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : Entity
{
    public override void Init()
    {
        base.Init();
        _healthSystem.Init(this, 1000f);
    }

    public void Damage(float damage)
    {
        
    }

    public override void Die()
    {
        
    }
}
