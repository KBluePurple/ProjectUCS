using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolemLaser : MonoBehaviour {

    private float _attackDamage;

    public void Init(float attackDamage) {
        _attackDamage = attackDamage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Test player = other.GetComponent<Test>();
        if (player != null) {
            player.HealthSystem.TakeDamage(_attackDamage);
        }
    }

    public void Attack() {
        GetComponent<Animator>().SetTrigger("Launch");
    }

    public void EndAttack() {
        GetComponent<Animator>().SetTrigger("EndLaunch");
    }
}
