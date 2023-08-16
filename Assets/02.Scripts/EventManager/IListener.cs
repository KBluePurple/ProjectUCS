using UnityEngine;

// Event Ãß°¡
public enum EventType {
    None,
    BossStoneGolem,
}

public interface IListener {
    void OnEvent<TEventType>(TEventType eventType, Component sender, object param = null) where TEventType : System.Enum;
}