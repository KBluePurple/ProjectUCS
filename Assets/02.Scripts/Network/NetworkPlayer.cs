using System;
using ProjectUCS.Common.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private CharacterBase characterBase;
    private Keyboard _keyboard = null;

    public int UserId { get; set; }
    public bool IsLocalPlayer { get; set; }

    private void Awake()
    {
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
    }

    private void Update()
    {
        if (!IsLocalPlayer || !NetworkRoom.IsInRoom) return;

        NetworkManager.Instance.Send(new C2S.Room.MovePacket
        {
            Position = new Position
            {
                X = transform.position.x,
                Y = transform.position.y
            },
            Horizontal = _keyboard.leftArrowKey.isPressed ? -1 : _keyboard.rightArrowKey.isPressed ? 1 : 0
        });
    }

    public void Move(Position packetPosition, int packetHorizontal)
    {
        transform.position = new Vector3(packetPosition.X, packetPosition.Y, 0);
        characterBase.Move(new Vector2(packetHorizontal, 0));

        Debug.Log($"Move: {packetPosition.X}, {packetPosition.Y}, {packetHorizontal}");
    }
}