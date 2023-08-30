using System;
using ProjectUCS.Common;
using ProjectUCS.Common.Data;
using ProjectUCS.Common.Data.RpcHandler;
using UnityEngine;

// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedMember.Local

public class NetworkRoom : MonoBehaviour
{
    public static NetworkRoom Instance { get; private set; }
    public static bool IsInRoom { get; private set; }
    public static int MemberCount { get; private set; }
    public static int MaxMemberCount { get; private set; }
    
    public event Action OnRoomWelcome;
    public event Action<int, int> OnRoomInfo;
    public event Action<int> OnPlayerJoined;
    public event Action<int> OnPlayerLeft;
    
    private void Awake()
    {
        Instance = this;
        _ = new RpcManager();
    }
    
    public void LeaveRoom()
    {
        throw new NotImplementedException();
    }
    
    private class RpcManager : RpcHandler
    {
        [RpcHandler(typeof(S2C.Room.RoomWelcomePacket))]
        private void OnRoomWelcomePacket(Connection connection, S2C.Room.RoomWelcomePacket packet)
        {
            MainThread.Invoke(() =>
            {
                IsInRoom = true;
                Instance.OnRoomWelcome?.Invoke();
            });
        }
        
        [RpcHandler(typeof(S2C.Room.RoomInfoPacket))]
        private void OnRoomInfoPacket(Connection connection, S2C.Room.RoomInfoPacket packet)
        {
            MainThread.Invoke(() =>
            {
                MemberCount = packet.CurrentPlayers;
                MaxMemberCount = packet.MaxPlayers;
                Instance.OnRoomInfo?.Invoke(packet.CurrentPlayers, packet.MaxPlayers);
            });
        }
        
        [RpcHandler(typeof(S2C.Room.PlayerJoinedPacket))]
        private void OnPlayerJoinedPacket(Connection connection, S2C.Room.PlayerJoinedPacket packet)
        {
            MainThread.Invoke(() =>
            {
                Instance.OnPlayerJoined?.Invoke(packet.UserId);
            });
        }
        
        [RpcHandler(typeof(S2C.Room.PlayerLeftPacket))]
        private void OnPlayerLeftPacket(Connection connection, S2C.Room.PlayerLeftPacket packet)
        {
            MainThread.Invoke(() =>
            {
                Instance.OnPlayerLeft?.Invoke(packet.UserId);
            });
        }

        [RpcHandler(typeof(S2C.Room.MovePacket))]
        private void OnMovePacket(Connection connection, S2C.Room.MovePacket packet)
        {
            MainThread.Invoke(() =>
            {
                var player = PlayerManager.Instance.GetPlayer(packet.UserId);
                player.Move(packet.Position, packet.Horizontal);
            });
        }
    }
}