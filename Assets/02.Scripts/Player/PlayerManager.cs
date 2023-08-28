using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    
    private readonly Dictionary<int, GameObject> _players = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        NetworkRoom.Instance.OnPlayerJoined += OnPlayerJoined;
        NetworkRoom.Instance.OnPlayerLeft += OnPlayerLeft;
    }

    private void OnPlayerJoined(int userId)
    {
        var playerObject = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        playerObject.name = $"Player {userId}";
        playerObject.transform.position = new Vector3(0, 0, 0);
        var player = playerObject.GetComponent<NetworkPlayer>();
        player.UserId = userId;
        player.IsLocalPlayer = userId == NetworkManager.Instance.UserId;
        _players.TryAdd(userId, playerObject);
        Debug.Log($"Player {userId} joined");
    }
    
    private void OnPlayerLeft(int userId)
    {
        var player = _players[userId];
        Destroy(player);
        _players.Remove(userId);
    }
}