using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcesManager : SingletonTemplate<ResourcesManager> {
    private Dictionary<string, GameObject> _prefabDictionary = new Dictionary<string, GameObject>();

    public T Load<T>(string path) where T : Object => Resources.Load<T>(path);

    public GameObject Instantiate(string name, string path = "", Transform parent = null) {
        GameObject prefab = null;

        if (!_prefabDictionary.TryGetValue(name, out prefab)) {
            path = Path.Combine(path, name);
            prefab = Load<GameObject>($"Prefabs/{path}");

            if (prefab == null) {
                Debug.Log($"Failed to load prefab : {path}");
                return null;
            }
            _prefabDictionary.Add(name, prefab);
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go) {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
