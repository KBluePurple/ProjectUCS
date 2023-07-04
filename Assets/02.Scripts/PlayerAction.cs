using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    private InputAction _moveAction;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _moveAction = _playerInput.Player.Move;
    }

    private void Start()
    {
        _playerInput.Enable();

        _moveAction.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Update()
    {
        var moveInput = _moveAction.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    private void Move(Vector2 moveInput)
    {
        Debug.Log(moveInput);
    }
}