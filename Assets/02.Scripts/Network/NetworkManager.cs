using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using ProjectUCS.Common;
using ProjectUCS.Common.Data;
using ProjectUCS.Common.Data.RpcHandler;
using UnityEngine;

// ReSharper disable UnusedMember.Local

public class NetworkManager : MonoBehaviour
{
    public TestNetworkPlayer localPlayer;

    private readonly Dictionary<int, TestNetworkPlayer> _players = new();
    public readonly Queue<Action> Actions = new();
    private Connection _connection;
    private Socket _socket;
    public static NetworkManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var moveManager = new PacketManager();

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _socket.Connect("localhost", 7777);

        _connection = new Connection(_socket);
        _connection.OnPacketReceived += (sender, packet) => { Actions.Enqueue(() => Debug.Log(packet.GetType())); };
        _connection.OnDisconnected += (sender, args) => { Debug.Log("Disconnected!"); };

        Debug.Log($"Connected: {_socket.Connected}");

        StartCoroutine(SendMovePacketCoroutine());
    }

    private void Update()
    {
        while (Actions.Count > 0) Actions.Dequeue().Invoke();
    }

    private IEnumerator SendMovePacketCoroutine()
    {
        while (true)
        {
            yield return null;
            SendMovePacket(localPlayer.transform.position);
        }
    }

    public void SendMovePacket(Vector2 position)
    {
        var movePacket = new C2S.Room.MovePacket
        {
            Position = new Position { X = position.x, Y = position.y }
        };
        _connection.Send(movePacket);
    }

    public void OnMovePacket(int userId, Position position)
    {
        if (!_players.ContainsKey(userId)) return;
        var player = _players[userId];
        player.transform.position = new Vector3(position.X, position.Y, 0f);
    }

    public void OnPlayerJoinedPacket(int userId)
    {
        var player = Instantiate(localPlayer, Vector3.zero, Quaternion.identity);
        player.UserId = userId;
        _players.Add(userId, player);
    }
    
    public void OnPlayerLeftPacket(int userId)
    {
        if (!_players.ContainsKey(userId)) return;
        var player = _players[userId];
        _players.Remove(userId);
        Destroy(player.gameObject);
    }
}

public class PacketManager : RpcHandler
{
    private int _userId;

    [RpcHandler(typeof(S2C.ChatPacket))]
    private void OnChatPacket(Connection connection, S2C.ChatPacket packet)
    {
        NetworkManager.Instance.Actions.Enqueue(() => Debug.Log($"Message: {packet.Message}"));
    }

    [RpcHandler(typeof(S2C.WelcomePacket))]
    private void OnWelcomePacket(Connection connection, S2C.WelcomePacket packet)
    {
        _userId = packet.UserId;
        NetworkManager.Instance.localPlayer.UserId = packet.UserId;
        NetworkManager.Instance.localPlayer.IsLocalPlayer = true;
        NetworkManager.Instance.Actions.Enqueue(() => Debug.Log($"Welcome: {packet.UserId}"));
    }

    [RpcHandler(typeof(S2C.Room.MovePacket))]
    private void OnMovePacket(Connection connection, S2C.Room.MovePacket packet)
    {
        NetworkManager.Instance.Actions.Enqueue(() =>
        {
            NetworkManager.Instance.OnMovePacket(packet.UserId, packet.Position);
        });
    }

    [RpcHandler(typeof(S2C.Room.PlayerJoinedPacket))]
    private void OnPlayerJoinedPacket(Connection connection, S2C.Room.PlayerJoinedPacket packet)
    {
        if (packet.UserId == _userId) return;
        NetworkManager.Instance.Actions.Enqueue(() => { NetworkManager.Instance.OnPlayerJoinedPacket(packet.UserId); });
    }
    
    [RpcHandler(typeof(S2C.Room.PlayerLeftPacket))]
    private void OnPlayerLeftPacket(Connection connection, S2C.Room.PlayerLeftPacket packet)
    {
        NetworkManager.Instance.Actions.Enqueue(() =>
        {
            NetworkManager.Instance.OnPlayerLeftPacket(packet.UserId);
        });
    }
}