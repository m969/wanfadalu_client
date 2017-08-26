/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *    描述： 此类用来封装unity的两种资源加载方式，Editor模式下的AssetDatabase和
 *          产品模式（就是编译发布后的）下的AssetBundle，（Resources的资源加载模式不适用于Mmorpg游戏）
 *          
 * 
    IOS:
    Application.dataPath :                      Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data
    Application.streamingAssetsPath :   Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data/Raw
    Application.persistentDataPath :      Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Documents
    Application.temporaryCachePath :   Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Library/Caches

    Android:
    Application.dataPath :                         /data/app/xxx.xxx.xxx.apk
    Application.streamingAssetsPath :      jar:file:///data/app/xxx.xxx.xxx.apk/!/assets
    Application.persistentDataPath :         /data/data/xxx.xxx.xxx/files
    Application.temporaryCachePath :      /data/data/xxx.xxx.xxx/cache

    Windows:
    Application.dataPath :                         /Assets
    Application.streamingAssetsPath :      /Assets/StreamingAssets
    Application.persistentDataPath :         C:/Users/xxxx/AppData/LocalLow/CompanyName/ProductName
    Application.temporaryCachePath :      C:/Users/xxxx/AppData/Local/Temp/CompanyName/ProductName

    Mac:
    Application.dataPath :                         /Assets
    Application.streamingAssetsPath :      /Assets/StreamingAssets
    Application.persistentDataPath :         /Users/xxxx/Library/Caches/CompanyName/Product Name
    Application.temporaryCachePath :     /var/folders/57/6b4_9w8113x2fsmzx_yhrhvh0000gn/T/CompanyName/Product Name


    Windows Web Player:
    Application.dataPath :             file:///D:/MyGame/WebPlayer (即导包后保存的文件夹，html文件所在文件夹)
    Application.streamingAssetsPath :
    Application.persistentDataPath :
    Application.temporaryCachePath :
 * -------------------------- */

using System;
using System.Collections.Generic;
using MagicFire.Mmorpg.Huanhuo;
using MagicFire.SceneManagement;

namespace MagicFire.Common.Plugin
{
    using UnityEngine;
    using System.Collections;
    using System.IO;

    public class AssetTool : MonoSingleton<AssetTool>
    {
        public const string PrefabsFolder = 
            "Assets/_Prefabs";

        public const string ScenesFolder =
            "Assets/_Scenes";

        public const string AuxiliaryPrefabsFolder = 
            "Assets/_Prefabs/AuxiliaryPrefabs";

        public const string AvatarFolder =
            "Assets/_Prefabs/Avatar";

        public const string EffectFolder = 
            "Assets/_Prefabs/Effect";

        public const string MonsterFolder = 
            "Assets/_Prefabs/Monster";

        public const string NpcFolder = 
            "Assets/_Prefabs/Npc";

        public const string TriggerFolder = 
            "Assets/_Prefabs/Trigger";

        public const string UiPanelFolder =
            "Assets/_Prefabs/UiPanel";

        public const string UiPanelPanelsFolder = 
            "Assets/_Prefabs/UiPanel/Panels";

        public const string UiPanelGoodsItemsFolder =
            "Assets/_Prefabs/UiPanel/GoodsItems";



        public static Object LoadScenesAssetBySceneNameAndAssetName(string sceneName, string assetName)
        {
            return LoadAsset_Database_Or_Bundle(ScenesFolder + "/" + sceneName + "/" + assetName + ".prefab", "Prefabs", sceneName.ToLower(), assetName);
        }

        public static Object LoadAuxiliaryAssetByName(string assetName)
        {
            return LoadAsset_Database_Or_Bundle(AuxiliaryPrefabsFolder + "/" + assetName + ".prefab", "Prefabs", "auxiliaryprefabs_bundle", assetName);
        }

        public static Object LoadAvatarAssetByName(string assetName)
        {
            return LoadAsset_Database_Or_Bundle(AvatarFolder + "/" + assetName + ".prefab", "Prefabs", "avatar_bundle", assetName);
        }

        public static Object LoadEffectAssetByName(string assetName)
        {
            return LoadAsset_Database_Or_Bundle(EffectFolder + "/" + assetName + ".prefab", "Prefabs", "effect_bundle", assetName);
        }

        public static Object LoadMonsterAssetByName(string assetName)
        {
            return LoadAsset_Database_Or_Bundle(MonsterFolder + "/" + assetName + ".prefab", "Prefabs", "monster_bundle", assetName);
        }

        public static Object LoadNpcAssetByName(string assetName)
        {
            return LoadAsset_Database_Or_Bundle(NpcFolder + "/" + assetName + ".prefab", "Prefabs", "npc_bundle", assetName);
        }

        public static Object LoadTriggerAssetByName(string assetName)
        {
            var assetNameDatabase = assetName;
            assetNameDatabase = assetNameDatabase.Replace("_", "/");
            return LoadAsset_Database_Or_Bundle(TriggerFolder + "/" + assetNameDatabase + ".prefab", "Prefabs", "trigger_bundle", assetName);
        }

        public static Object LoadUiPanelPanelsAssetByName(string assetName)
        {
            return LoadAsset_Database_Or_Bundle(UiPanelPanelsFolder + "/" + assetName + ".prefab", "Prefabs", "uipanel_bundle", assetName);
        }

        public static Object LoadUiPanelGoodsItemsAssetByName(string assetName)
        {
            return LoadAsset_Database_Or_Bundle(UiPanelGoodsItemsFolder + "/" + assetName + ".prefab", "Prefabs", "uipanel_bundle", assetName);
        }



        public static Object LoadAsset_Database_Or_Bundle(string assetDatabasePath, string bundlePath = "", string bundleName = "", string assetName = "")
        {
            Object asset = null;
            AssetBundle bundle = null;

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (XmlSceneManager.Instance.LoadMode == XmlSceneManager.LoadModeEnum.Database)
                {
                    asset = LoadAssetDatabaseOrTag(assetDatabasePath);
                    if (asset == null)
                        Debug.LogError(assetDatabasePath + " is null!");
                    return asset;
                }
            }
            bundle = BundleTool.Instance.TryGetBundle(bundlePath, bundleName);
            if (bundle)
            {
                asset = bundle.LoadAsset(assetName);
                if (asset == null)
                {
                    Debug.LogError(assetName + " is null");
                }
            }
            else
            {
                Debug.LogError(bundleName + " is null");
            }
            return asset;
        }

        public static Object LoadAssetByTag(string assetTag)
        {
            Object asset = null;
            PrefabTagScriptableObject tag = null;

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (XmlSceneManager.Instance.LoadMode == XmlSceneManager.LoadModeEnum.Database)
                {
                    asset = LoadAssetDatabaseOrTag("", assetTag);
                    return asset;
                }
            }

            var tagBundle = BundleTool.Instance.TryGetBundle("Prefabs", "prefab_tags_bundle");
            if (tagBundle)
                tag = tagBundle.LoadAsset(assetTag) as PrefabTagScriptableObject;
            if (tag == null)
                XmlSceneManager.Message1 += "tag is null!\n";
            if (tag != null)
            {
                var bundle = BundleTool.Instance.TryGetBundle(tag.BundlePath, tag.BundleName);
                if (bundle)
                    asset = bundle.LoadAsset(tag.AssetName);
            }
            return asset;
        }

        private static Object LoadAssetDatabaseOrTag(string assetDatabasePath, string assetTag = "")
        {
            Object asset = null;



            //if (assetTag != "")
            //{
            //    var tag = UnityEditor.AssetDatabase.LoadAssetAtPath<PrefabTagScriptableObject>("Assets/_Prefabs/_PrefabTags/" + assetTag + ".asset");
            //    assetDatabasePath = tag.DatabasePath;
            //}
            //asset = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(assetDatabasePath);



            return asset;
        }
    }
}
