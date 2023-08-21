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
    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;

    private bool _isLeft = false;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _spriteRenderer = _animator.GetComponent<SpriteRenderer>();
    }

    public void Move(Vector2 direction)
    {
        _animator.SetBool("IsMove", direction.x != 0);

        _rigidbody2D.velocity = new Vector2(direction.x * _moveSpeed, _rigidbody2D.velocity.y);

        if(direction.x != 0)
        {
            _isLeft = direction.x < 0;
        }

        transform.localScale = new Vector3(_isLeft ? -1 : 1, 1, 1);
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("IsGround", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("IsGround", false);
        }
    }
}

