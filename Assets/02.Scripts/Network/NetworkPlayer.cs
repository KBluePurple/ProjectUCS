using System;
using ProjectUCS.Common.Data;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class NetworkPlayer : MonoBehaviour
{
    private Player _player;
    private CharacterBase _characterBase;
    private Keyboard _keyboard;

    public int UserId { get; set; }
    public bool IsLocalPlayer { get; set; }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _keyboard = Keyboard.current;
    }

    private void Start()
    {
        if (IsLocalPlayer)
        {
            NetworkManager.Instance.localPlayer = this;
        }
        else
        {
            var player = GetComponent<Player>();
            player.enabled = false;
        }

        _player.OnPlayerAttack += OnPlayerAttack;
        _characterBase = _player.characterBase;
    }

    private void OnPlayerAttack()
    {
        if (!IsLocalPlayer || !NetworkRoom.IsInRoom) return;

        NetworkManager.Instance.Send(new C2S.Room.AttackPacket());
    }

    private void Update()
    {
        if (!IsLocalPlayer || !NetworkRoom.IsInRoom) return;

        NetworkManager.Instance.Send(new C2S.Room.MovePacket
        {
            Position = new Position
            {
                X = _characterBase.transform.position.x,
                Y = _characterBase.transform.position.y
            },
            Horizontal = _keyboard.leftArrowKey.isPressed ? -1 : _keyboard.rightArrowKey.isPressed ? 1 : 0
        });
    }

    public void Move(Position packetPosition, int packetHorizontal)
    {
        if (_characterBase == null) return;
        if (IsLocalPlayer) return;

        _characterBase.transform.position = new Vector3(packetPosition.X, packetPosition.Y, 0);
        _characterBase.Move(new Vector2(packetHorizontal, 0));
    }

    public void Attack()
    {
        if (_characterBase == null) return;
        if (IsLocalPlayer) return;

        _characterBase.Attack();
        Debug.Log("Attack");
    }
}