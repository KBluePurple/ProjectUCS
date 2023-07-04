using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSingletonTemplate<T> : MonoBehaviour where T : MonoBehaviour {
    public static T Instance {
        get {
            if (_instance == null) {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null) {
                    var obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }

                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    private static T _instance = null;

    private void Awake() {
        if (_instance != null) {
            if (_instance != this) {
                Destroy(gameObject);
            }

            return;
        }

        _instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}
