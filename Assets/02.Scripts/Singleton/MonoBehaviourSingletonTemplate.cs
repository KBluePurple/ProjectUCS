using UnityEngine;

public class MonoBehaviourSingletonTemplate<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            _instance = (T)FindObjectOfType(typeof(T));

            if (_instance == null)
            {
                var obj = new GameObject(typeof(T).Name);
                _instance = obj.AddComponent<T>();
            }

            DontDestroyOnLoad(_instance.gameObject);

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            if (_instance != this) Destroy(gameObject);

            return;
        }

        _instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}