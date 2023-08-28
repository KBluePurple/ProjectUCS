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
        var player = Instantiate(Resources.Load<GameObject>($"Prefabs/{nameof(Player)}"));
        player.name = $"Player {userId}";
        player.transform.position = new Vector3(0, 0, 0);
        _players.Add(userId, player);
    }
    
    private void OnPlayerLeft(int userId)
    {
        var player = _players[userId];
        Destroy(player);
        _players.Remove(userId);
    }
}