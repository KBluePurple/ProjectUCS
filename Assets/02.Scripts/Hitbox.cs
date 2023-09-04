using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseBoss boss = collision.GetComponent<BaseBoss>();
        if (boss == null)
        {
            return;
        }

        float damage = 0;

        int[] stateHashes = new int[3];
        stateHashes[0] = Animator.StringToHash("Base Layer.Attack.Gordon_ComboSwing1");
        stateHashes[1] = Animator.StringToHash("Base Layer.Attack.Gordon_ComboSwing2");
        stateHashes[2] = Animator.StringToHash("Base Layer.Attack.Gordon_ComboSwing3");

        var stateInfoHash = _animator.GetCurrentAnimatorStateInfo(0).fullPathHash;

        if (stateInfoHash == stateHashes[0])
        {
            damage = 10f;
        }
        else if (stateInfoHash == stateHashes[1])
        {
            damage = 20f;
        }
        else if (stateInfoHash == stateHashes[2])
        {
            damage = 30f;
        }

        boss.HealthSystem.TakeDamage(damage);

        //collision.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
    }
}
