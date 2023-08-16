using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterBase _characterBase = null;
    private Keyboard _keyboard = null;

    private void Awake()
    {
        _characterBase = GetComponentInChildren<CharacterBase>();
        _keyboard = Keyboard.current;
    }

    private void Update()
    {
        if (_keyboard.spaceKey.wasPressedThisFrame)
        {
            _characterBase.Jump();
        }

        var direction = new Vector2(_keyboard.leftArrowKey.isPressed ? -1 : _keyboard.rightArrowKey.isPressed ? 1 : 0, 0);
        _characterBase.Move(direction);

        if (_keyboard.xKey.wasPressedThisFrame)
        {
            _characterBase.Attack();
        }
    }
}
