using System;
using System.Collections.Generic;
using UnityEngine;

public class MainThread : MonoBehaviour
{
    public static MainThread Instance { get; private set; }
    
    public static void Invoke(Action action)
    {
        if (Instance == null)
            return;

        Instance.Actions.Enqueue(action);
    }
    
    private readonly Queue<Action> Actions = new();
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Update()
    {
        while (Actions.Count > 0) Actions.Dequeue()?.Invoke();
    }
    
    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        var go = new GameObject("MainThread");
        DontDestroyOnLoad(go);
        go.AddComponent<MainThread>();
    }
}