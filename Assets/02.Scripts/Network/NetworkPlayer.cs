using System;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    public int UserId { get; set; }
    public bool IsLocalPlayer { get; set; }

    private void Start()
    {
        if (IsLocalPlayer)
            NetworkManager.Instance.localPlayer = this;
        else
            GetComponent<Player>().enabled = false;
    }
}