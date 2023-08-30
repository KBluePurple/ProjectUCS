using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemProjectile : MonoBehaviour {

    private Rigidbody2D _rigidbody;

    private float _projectileSpeed;
    private float _attackDamage;

    public void Init(float projectileSpeed, float attackDamage, Vector2 direction) {
        _rigidbody = GetComponent<Rigidbody2D>();

        _projectileSpeed = projectileSpeed;
        _attackDamage = attackDamage;

        _rigidbody.velocity = direction * projectileSpeed;

        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Test player = other.GetComponent<Test>();
        //if (player != null) {
        //    player.HealthSystem.TakeDamage(_attackDamage);
        //}

        Player player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            player.HealthSystem.TakeDamage(_attackDamage);
        }
    }
}
