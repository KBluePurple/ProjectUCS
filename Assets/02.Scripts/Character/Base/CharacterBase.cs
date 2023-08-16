using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    private float _hp = 0f;
    [SerializeField]
    private float _mp = 0f;
    [SerializeField]
    private float _def = 0f;
    [SerializeField]
    private float _atk = 0f;
    [SerializeField]
    private float _moveSpeed = 0f;
    [SerializeField]
    private float _jumpPower = 0f;
    [SerializeField]
    private float _crt = 0f;


    private Rigidbody2D _rigidbody2D = null;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        _rigidbody2D.velocity = new Vector2(direction.x * _moveSpeed, _rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }
}
