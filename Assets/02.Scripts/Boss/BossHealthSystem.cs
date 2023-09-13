using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthSystem : HealthSystem
{
    public BaseBoss Boss => _entity as BaseBoss;

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        Boss.UpdateHealthBar();
    }
}
