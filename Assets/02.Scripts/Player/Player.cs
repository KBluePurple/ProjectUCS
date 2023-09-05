using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField]
    private CharacterBase _characterBase = null;

    private Keyboard _keyboard = null;
    private int _jumpCount = 0;

    public override void Init()
    {
        base.Init();

        _characterBase = Instantiate(_characterBase.gameObject, transform.position, Quaternion.identity).GetComponent<CharacterBase>();
        _keyboard = Keyboard.current;

        _healthSystem.Init(this, 100);
    }

    private void Update()
    {
        if (_keyboard.cKey.wasPressedThisFrame && _jumpCount < _characterBase.JumpMaxCount)
        {
            _characterBase.Jump();
            _jumpCount++;
        }

        var direction = new Vector2(_keyboard.leftArrowKey.isPressed ? -1 : _keyboard.rightArrowKey.isPressed ? 1 : 0, 0);
        _characterBase.Move(direction);

        if (_keyboard.xKey.wasPressedThisFrame)
        {
            _characterBase.Attack();
        }

        if (_characterBase.IsGround)
        {
            ResetJumpCount();
        }
    }

    private void ResetJumpCount() => _jumpCount = 0;

    public override void Die() { }

}
