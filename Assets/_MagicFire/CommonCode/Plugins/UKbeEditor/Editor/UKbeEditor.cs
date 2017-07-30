using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class UKbeEditor : EditorWindow
{

    private SerializedProperty _property;

    [MenuItem("Window/UKbeEditor/Open UKbeEditor Window", false, 1)]
    public static void Open()
    {
        GetWindow<UKbeEditor>();
    }

    [MenuItem("Assets/Create Entity Def Data")]
    public static void CreatePrefabTagMethod()
    {
        var path = EditorUtility.SaveFilePanelInProject("Save Resource", "EntityDefData", "asset", "", "Assets/Resources/EntityDefData");
        if (path.Length != 0)
        {
            var obj = ScriptableObject.CreateInstance<EntityDefData>();
            AssetDatabase.CreateAsset(obj, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    public void OnEnable()
    {
        Init();
    }

    public void OnDisable()
    {
    }

    public void OnGUI()
    {
        //if (GUILayout.Button("test", EditorStyles.miniButton, GUILayout.Width(80), GUILayout.Height(15)))
        //{
        //    Debug.Log("test");
        //}

        BeginWindows();

        GUI.Window(1, new Rect(10, 10, 100, 100), Func, "win", EditorStyles.helpBox);

        EndWindows();
    }

    private void Init()
    {
        minSize = new Vector2(600f, 300f);
    }

    private void Func(int id)
    {
        GUI.Toggle(new Rect(40, 0, 80, 20), true, "dd");
    }
}
