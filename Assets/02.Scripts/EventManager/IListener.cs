using UnityEngine;

// Event �߰�
public enum EventType
{
    None
}

public interface IListener
{
    void OnEvent(EventType eventType, Component sender, object param = null);
}