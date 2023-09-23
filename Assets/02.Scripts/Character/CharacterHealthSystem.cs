using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthSystem : HealthSystem
{
    public override void TakeDamage(float damageAmount)
    {
        Player player = _entity as Player;
        if (player.IsCrouching)
        {
            damageAmount *= 0.1f;
        }

        _health = Mathf.Clamp(_health - damageAmount, 0, _maxHealth);

        Debug.Log(gameObject.name + " : " + _health);

        if (_health <= 0f)
        {
            _entity.Die();
        }
    }
}
