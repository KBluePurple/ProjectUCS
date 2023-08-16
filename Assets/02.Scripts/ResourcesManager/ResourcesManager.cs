using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcesManager : SingletonTemplate<ResourcesManager> {
    private readonly Dictionary<string, GameObject> _prefabDictionary = new();

    public T Load<T>(string path) where T : Object {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string name, string path = "", Transform parent = null) {
        if (_prefabDictionary.TryGetValue(name, out var prefab))
            return Object.Instantiate(prefab, parent);

        path = Path.Combine(path, name);
        prefab = Load<GameObject>($"Prefabs/{path}");

        if (prefab == null) {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        _prefabDictionary.Add(name, prefab);

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go) {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}