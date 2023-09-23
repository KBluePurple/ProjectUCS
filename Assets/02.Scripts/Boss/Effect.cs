using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    Animator _animator;

    public void Init()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Effect");
    }
}
