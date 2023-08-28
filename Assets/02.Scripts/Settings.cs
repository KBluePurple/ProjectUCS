using System;
using UnityEngine;
using UnityEngine.InputSystem;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable MemberHidesStaticFromOuterClass
public static class Settings
{
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        Load();
    }

    public static void Apply()
    {
        OnApply?.Invoke();
    }

    public static void Save()
    {
        object settingObj = new();

        var type = typeof(Settings);
        foreach (var category in type.GetNestedTypes())
        {
            var categoryObj = new object();
            foreach (var field in category.GetFields())
            {
                var value = field.GetValue(null);
                categoryObj.GetType().GetField(field.Name)?.SetValue(categoryObj, value);
            }

            settingObj.GetType().GetField(category.Name)?.SetValue(settingObj, categoryObj);
        }

        var json = JsonUtility.ToJson(settingObj);
        PlayerPrefs.SetString("Settings", json);
    }

    private static void Load()
    {
        var json = PlayerPrefs.GetString("Settings");
        if (string.IsNullOrEmpty(json)) return;

        var settingObj = JsonUtility.FromJson(json, typeof(object));

        var type = typeof(Settings);
        foreach (var category in type.GetNestedTypes())
        {
            var categoryObj = settingObj.GetType().GetField(category.Name)?.GetValue(settingObj);
            foreach (var field in category.GetFields())
            {
                var value = categoryObj?.GetType().GetField(field.Name)?.GetValue(categoryObj);
                field.SetValue(null, value);
            }
        }
    }

    public static event Action OnApply;

    public static class KeyBindings
    {
        public static Key MoveUp { get; set; } = Key.UpArrow;
        public static Key MoveDown { get; set; } = Key.DownArrow;
        public static Key MoveLeft { get; set; } = Key.LeftArrow;
        public static Key MoveRight { get; set; } = Key.RightArrow;
        
        public static Key Attack { get; set; } = Key.X;
        public static Key Jump { get; set; } = Key.C;
        
        public static Key Skill1 { get; set; } = Key.A;
        public static Key Skill2 { get; set; } = Key.S;
        public static Key Skill3 { get; set; } = Key.D;
        
        public static void Apply()
        {
            OnApply?.Invoke();
        }
        
        public static event Action OnApply;
    }
}