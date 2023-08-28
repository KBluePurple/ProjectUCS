using System;
using ProjectUCS.Common;
using ProjectUCS.Common.Data;
using ProjectUCS.Common.Data.RpcHandler;
using UnityEngine;
using UnityEngine.InputSystem;

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
    
    public void StartMatch()
    {
        NetworkManager.Instance.Send(new C2S.StartMatchPacket());
        Debug.Log("StartMatch");
    }
    
    public void CancelMatch()
    {
        NetworkManager.Instance.Send(new C2S.CancelMatchPacket());
        Debug.Log("CancelMatch");
    }

    private Keyboard _keyboard;
    
    private void Start()
    {
        _keyboard = Keyboard.current;
    }
    
    private void Update()
    {
        if (_keyboard.f1Key.wasPressedThisFrame)
            StartMatch();
        if (_keyboard.f2Key.wasPressedThisFrame)
            CancelMatch();
    }

    private class RpcManager : RpcHandler
    {
        [RpcHandler(typeof(S2C.MatchingStartedPacket))]
        private void OnMatchingStartedPacket(Connection connection, S2C.MatchingStartedPacket packet)
        {
            Debug.Log("OnMatchingStartedPacket");
            MainThread.Invoke(() => Instance.OnMatchingStarted?.Invoke());
        }

        [RpcHandler(typeof(S2C.MatchingStoppedPacket))]
        private void OnMatchingStoppedPacket(Connection connection, S2C.MatchingStoppedPacket packet)
        {
            Debug.Log("OnMatchingStoppedPacket");
            MainThread.Invoke(() => Instance.OnMatchingStopped?.Invoke());
        }

        [RpcHandler(typeof(S2C.MatchingEndedPacket))]
        private void OnMatchingEndedPacket(Connection connection, S2C.MatchingEndedPacket packet)
        {
            Debug.Log("OnMatchingEndedPacket");
            MainThread.Invoke(() => Instance.OnMatchingEnded?.Invoke());
        }

        [RpcHandler(typeof(S2C.MatchInfoPacket))]
        private void OnMatchInfoPacket(Connection connection, S2C.MatchInfoPacket packet)
        {
            Debug.Log("OnMatchInfoPacket");
            MainThread.Invoke(() => Instance.OnMatchInfo?.Invoke(packet.CurrentPlayers, packet.MaxPlayers));
        }
    }
}