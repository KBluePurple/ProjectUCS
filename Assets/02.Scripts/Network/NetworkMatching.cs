using System;
using ProjectUCS.Common;
using ProjectUCS.Common.Data;
using ProjectUCS.Common.Data.RpcHandler;
using UnityEngine;

// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedMember.Local

public class NetworkMatching : MonoBehaviour
{
    public static NetworkMatching Instance { get; private set; }
    
    public event Action OnMatchingStarted;
    public event Action OnMatchingStopped;
    public event Action OnMatchingEnded;
    public event Action<int, int> OnMatchInfo;
    
    private void Awake()
    {
        Instance = this;
        _ = new RpcManager();
    }
    
    private class RpcManager : RpcHandler
    {
        [RpcHandler(typeof(S2C.MatchingStartedPacket))]
        private void OnMatchingStartedPacket(Connection connection, S2C.MatchingStartedPacket packet)
        {
            MainThread.Invoke(() => Instance.OnMatchingStarted?.Invoke());
        }

        [RpcHandler(typeof(S2C.MatchingStoppedPacket))]
        private void OnMatchingStoppedPacket(Connection connection, S2C.MatchingStoppedPacket packet)
        {
            MainThread.Invoke(() => Instance.OnMatchingStopped?.Invoke());
        }

        [RpcHandler(typeof(S2C.MatchingEndedPacket))]
        private void OnMatchingEndedPacket(Connection connection, S2C.MatchingEndedPacket packet)
        {
            MainThread.Invoke(() => Instance.OnMatchingEnded?.Invoke());
        }

        [RpcHandler(typeof(S2C.MatchInfoPacket))]
        private void OnMatchInfoPacket(Connection connection, S2C.MatchInfoPacket packet)
        {
            MainThread.Invoke(() => Instance.OnMatchInfo?.Invoke(packet.CurrentPlayers, packet.MaxPlayers));
        }
    }
}