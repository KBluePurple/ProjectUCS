using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventManager : MonoBehaviourSingletonTemplate<EventManager> {
    private Dictionary<EVENT_TYPE, List<IListener>> _listenerDictionary = new Dictionary<EVENT_TYPE, List<IListener>>();

    /// <summary>
    /// �̺�Ʈ �߰�
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
    /// �̺�Ʈ ����
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
    /// �̺�Ʈ�� ���� ��
    /// </summary>
    /// <param name="eventType"></param>
    public void RemoveEvent(EVENT_TYPE eventType) => _listenerDictionary.Remove(eventType);

    /// <summary>
    /// �̺�Ʈ���� Ư�� �κи� ���� ��
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
    /// ���� ����Ǹ� ����
    /// ���� ����Ǿ��� �� ������Ʈ �ı� ���� Ȯ�ο�
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
