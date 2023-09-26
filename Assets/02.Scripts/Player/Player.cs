using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField] public CharacterBase characterBase;

    private Keyboard _keyboard = null;
    private int _jumpCount = 0;


    public bool IsCrouching => _isCrouching;
    private bool _isCrouching = false;

    public override void Init()
    {
        base.Init();

        characterBase = Instantiate(characterBase.gameObject, transform.position, Quaternion.identity).GetComponent<CharacterBase>();
        characterBase.Init(this);

        var hpBar = Instantiate(Resources.Load<HealthBarUI>("Prefabs/HealthHpBar"), characterBase.transform);
        _keyboard = Keyboard.current;

        _healthSystem.Init(this, 100);
        hpBar.Init(_healthSystem);
        hpBar.transform.localPosition = new Vector3(0, 1.55f, 0);
    }

    private void Update()
    {
        if (_keyboard.cKey.wasPressedThisFrame && _jumpCount < characterBase.JumpMaxCount)
        {
            characterBase.Jump();
            _jumpCount++;
        }

        var direction = new Vector2(_keyboard.leftArrowKey.isPressed ? -1 : _keyboard.rightArrowKey.isPressed ? 1 : 0, 0);
        characterBase.Move(direction);

        if (_keyboard.xKey.wasPressedThisFrame)
        {
            characterBase.Attack();
            OnPlayerAttack?.Invoke();
        }

        if (characterBase.IsGround)
        {
            ResetJumpCount();
        }

        _isCrouching = _keyboard.downArrowKey.isPressed;
    }

    private void ResetJumpCount() => _jumpCount = 0;

    public override void Die() {
        Application.Quit();
    }

    public event System.Action OnPlayerAttack;
}
