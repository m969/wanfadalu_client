namespace MagicFire.Common.Plugin
{
    using UnityEngine;
    using UnityEditor;
    using System.Collections;
    using MagicFire;

    public class CreatePrefabTag
    {
        [MenuItem("Assets/Create Prefab Tag")]
        public static void CreatePrefabTagMethod()
        {
            var path = EditorUtility.SaveFilePanelInProject("Save Resource", "prefabTag", "asset", "", "Assets/_Prefabs/_PrefabTags");
            if (path.Length != 0)
            {
                var obj = ScriptableObject.CreateInstance<PrefabTagScriptableObject>();
                AssetDatabase.CreateAsset(obj, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
