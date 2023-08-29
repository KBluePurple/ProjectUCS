using System;
using System.Collections.Generic;
using System.Net.Sockets;
using ProjectUCS.Common;
using ProjectUCS.Common.Data;
using ProjectUCS.Common.Data.RpcHandler;
using UnityEngine;

// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedMember.Local

public enum NetworkState
{
    Disconnected,
    Connected,
}

public class NetworkManager : MonoBehaviour
{
    public NetworkPlayer localPlayer;

    private Connection _connection;
    private Socket _socket;
    public int UserId { get; private set; }
    public NetworkState State { get; private set; } = NetworkState.Disconnected;
    public static NetworkManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _ = new RpcManager();
    }

    private void Start()
    {
        InitializeSocket();
        InitializeConnection();

        State = NetworkState.Connected;
        Debug.Log($"Connected: {_socket.Connected}");
    }

    private void InitializeConnection()
    {
        _connection = new Connection(_socket);

        _connection.OnDisconnected += (sender, args) =>
        {
            State = NetworkState.Disconnected;
            Debug.Log("Disconnected!");
        };
    }

    private void InitializeSocket()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _socket.Connect("172.31.1.130", 7777);
    }

    private class RpcManager : RpcHandler
    {
        [RpcHandler(typeof(S2C.WelcomePacket))]
        private void OnWelcomePacket(Connection connection, S2C.WelcomePacket packet)
        {
            Instance.UserId = packet.UserId;
            Instance.localPlayer.UserId = packet.UserId;
            Instance.localPlayer.IsLocalPlayer = true;
            MainThread.Invoke(() => Debug.Log($"Welcome: {packet.UserId}"));
        }
    }

    public void Send<T>(T packet) where T : IPacket
    {
        _connection.Send(packet);
    }
}