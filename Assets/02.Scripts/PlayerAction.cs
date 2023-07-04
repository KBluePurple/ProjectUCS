using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    private PlayerInput playerInput = null;
    private InputAction _moveAction = null;

    private void Awake()
    {
        playerInput = new PlayerInput();
        _moveAction = playerInput.Player.Move;
    }

    private void Start()
    {
        playerInput.Enable();

        _moveAction.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Update()
    {
        Vector2 moveInput = _moveAction.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    private void Move(Vector2 moveInput)
    {
        Debug.Log(moveInput);
    }
}
