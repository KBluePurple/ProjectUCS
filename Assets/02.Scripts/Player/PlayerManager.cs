using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    
    private readonly Dictionary<int, NetworkPlayer> _players = new();

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
        var networkPlayer = Instantiate(Resources.Load<NetworkPlayer>("Prefabs/Player"));
        networkPlayer.name = $"Player {userId}";
        networkPlayer.transform.position = new Vector3(0, 0, 0);
        networkPlayer.UserId = userId;
        networkPlayer.IsLocalPlayer = userId == NetworkManager.Instance.UserId;
        _players.TryAdd(userId, networkPlayer);
        Debug.Log($"Player {userId} joined");
    }
    
    private void OnPlayerLeft(int userId)
    {
        var player = _players[userId];
        Destroy(player);
        _players.Remove(userId);
    }

    public NetworkPlayer GetPlayer(int userId)
    {
        return _players[userId];
    }
}