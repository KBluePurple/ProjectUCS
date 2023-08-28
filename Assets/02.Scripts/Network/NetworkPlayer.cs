using ProjectUCS.Common.Data;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    public int UserId { get; set; }
    public bool IsLocalPlayer { get; set; }

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
        if (IsLocalPlayer && NetworkRoom.IsInRoom)
            NetworkManager.Instance.Send(new C2S.Room.MovePacket
            {
                Position = new Position
                {
                    X = transform.position.x,
                    Y = transform.position.y
                }
            });
    }
}