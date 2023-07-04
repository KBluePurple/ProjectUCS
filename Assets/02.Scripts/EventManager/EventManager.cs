using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviourSingletonTemplate<EventManager>
{
    private Dictionary<EventType, List<IListener>> _listenerDictionary = new();

    /// <summary>
    ///     이벤트 추가
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public void AddListener(EventType eventType, IListener listener)
    {
        if (_listenerDictionary.TryGetValue(eventType, out var list))
        {
            list.Add(listener);
            return;
        }

        list = new List<IListener>
        {
            listener
        };

        _listenerDictionary.Add(eventType, list);
    }

    /// <summary>
    ///     이벤트 실행
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="sender"></param>
    /// <param name="param"></param>
    public void PostNotification(EventType eventType, Component sender, object param = null)
    {
        if (!_listenerDictionary.TryGetValue(eventType, out var list))
            return;

        for (var i = 0; i < list.Count; i++) list[i]?.OnEvent(eventType, sender, param);
    }

    /// <summary>
    ///     이벤트를 지울 때
    /// </summary>
    /// <param name="eventType"></param>
    public void RemoveEvent(EventType eventType)
    {
        _listenerDictionary.Remove(eventType);
    }

    /// <summary>
    ///     이벤트에서 특정 부분만 지울 때
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public void RemoveEvent(EventType eventType, IListener listener)
    {
        if (_listenerDictionary.TryGetValue(eventType, out var list)) list.Remove(listener);
    }

    /// <summary>
    ///     씬이 변경되면 실행
    ///     씬이 변경되었을 때 오브젝트 파괴 여부 확인용
    /// </summary>
    public void RemoveRedundancies()
    {
        var newListenerDictionary = new Dictionary<EventType, List<IListener>>();

        foreach (var item in _listenerDictionary)
        {
            for (var i = item.Value.Count - 1; i >= 0; i--)
                if (item.Value[i].Equals(null))
                    item.Value.RemoveAt(i);

            if (item.Value.Count > 0) newListenerDictionary.Add(item.Key, item.Value);
        }

        _listenerDictionary = newListenerDictionary;
    }
}