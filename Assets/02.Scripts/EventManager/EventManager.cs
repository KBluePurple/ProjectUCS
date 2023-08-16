using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviourSingletonTemplate<EventManager> {
    private Dictionary<System.Enum, List<IListener>> _listenerDictionary = new();

    public void AddListener<TEventType>(TEventType eventType, IListener listener) where TEventType : System.Enum {
        if (_listenerDictionary.TryGetValue(eventType, out var list)) {
            list.Add(listener);
            return;
        }

        list = new List<IListener>
        {
            listener
        };

        _listenerDictionary.Add(eventType, list);
    }

    public void PostNotification<TEventType>(TEventType eventType, Component sender, object param = null) where TEventType : System.Enum {
        if (!_listenerDictionary.TryGetValue(eventType, out var list))
            return;

        for (var i = 0; i < list.Count; i++)
            list[i]?.OnEvent(eventType, sender, param);
    }

    public void RemoveEvent<TEventType>(TEventType eventType) where TEventType : System.Enum => _listenerDictionary.Remove(eventType);

    public void RemoveEvent<TEventType>(TEventType eventType, IListener listener) where TEventType : System.Enum {
        if (_listenerDictionary.TryGetValue(eventType, out var list))
            list.Remove(listener);
    }

    public void RemoveRedundancies() {
        var newListenerDictionary = new Dictionary<System.Enum, List<IListener>>();

        foreach (var item in _listenerDictionary) {
            for (var i = item.Value.Count - 1; i >= 0; i--)
                if (item.Value[i].Equals(null))
                    item.Value.RemoveAt(i);

            if (item.Value.Count > 0)
                newListenerDictionary.Add(item.Key, item.Value);
        }

        _listenerDictionary = newListenerDictionary;
    }
}