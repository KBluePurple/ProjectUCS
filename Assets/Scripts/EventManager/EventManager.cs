using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventManager : MonoBehaviourSingletonTemplate<EventManager> {
    private Dictionary<EVENT_TYPE, List<IListener>> _listenerDictionary = new Dictionary<EVENT_TYPE, List<IListener>>();

    /// <summary>
    /// 이벤트 추가
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public void AddListener(EVENT_TYPE eventType, IListener listener) {
        List<IListener> list = null;

        if (_listenerDictionary.TryGetValue(eventType, out list)) {
            list.Add(listener);
            return;
        }

        list = new List<IListener> {
            listener
        };

        _listenerDictionary.Add(eventType, list);
    }

    /// <summary>
    /// 이벤트 실행
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="sender"></param>
    /// <param name="param"></param>
    public void PostNotification(EVENT_TYPE eventType, Component sender, object param = null) {
        List<IListener> list = null;

        if (!_listenerDictionary.TryGetValue(eventType, out list))
            return;

        for (int i = 0; i < list.Count; i++) {
            list[i]?.OnEvent(eventType, sender, param);
        }
    }

    /// <summary>
    /// 이벤트를 지울 때
    /// </summary>
    /// <param name="eventType"></param>
    public void RemoveEvent(EVENT_TYPE eventType) => _listenerDictionary.Remove(eventType);

    /// <summary>
    /// 이벤트에서 특정 부분만 지울 때
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public void RemoveEvent(EVENT_TYPE eventType, IListener listener) {
        List<IListener> list = null;

        if (_listenerDictionary.TryGetValue(eventType, out list)) {
            list.Remove(listener);
            return;
        }
    }

    /// <summary>
    /// 씬이 변경되면 실행
    /// 씬이 변경되었을 때 오브젝트 파괴 여부 확인용
    /// </summary>
    public void RemoveRedundancies() {
        Dictionary<EVENT_TYPE, List<IListener>> newListenerDictionary = new Dictionary<EVENT_TYPE, List<IListener>>();

        foreach (KeyValuePair<EVENT_TYPE, List<IListener>> item in _listenerDictionary) {
            for (int i = item.Value.Count - 1; i >= 0; i--) {
                if (item.Value[i].Equals(null))
                    item.Value.RemoveAt(i);
            }

            if (item.Value.Count > 0) {
                newListenerDictionary.Add(item.Key, item.Value);
            }
        }

        _listenerDictionary = newListenerDictionary;
    }
}
