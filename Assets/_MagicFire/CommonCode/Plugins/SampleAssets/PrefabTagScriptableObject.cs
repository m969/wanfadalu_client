using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTagScriptableObject : ScriptableObject
{
    public const string PrefabsFolder = "Assets/_Prefabs";
    public const string PanelsFolder = "Assets/_Prefabs/UIPanel/Panels";
    public const string AvatarFolder = "Assets/_Prefabs/Avatar";
    public const string MonsterFolder = "Assets/_Prefabs/Monster";
    public const string NpcFolder = "Assets/_Prefabs/Npc";
    public const string TriggerFolder = "Assets/_Prefabs/Trigger";


    [System.Serializable]
    public enum FolderTypeEnum
    {
        None,
        Prefabs,
        Panels,
        Avatar,
        Monster,
        Npc,
        Trigger
    }


    [SerializeField, Space(20)]
    private FolderTypeEnum _folderType;
    [SerializeField]
    private string _databasePath;

    [SerializeField, Space(20)]
    private string _bundlePath = "Prefabs";
    [SerializeField]
    private string _bundleName;
    [SerializeField]
    private string _assetName;

    public FolderTypeEnum FolderType
    {
        get
        {
            return _folderType;
        }
    }

    public string DatabasePath
    {
        get
        {
            switch (_folderType)
            {
                case FolderTypeEnum.None:
                    break;
                case FolderTypeEnum.Prefabs:
                    return PrefabsFolder + "/" + _databasePath;
                case FolderTypeEnum.Panels:
                    return PanelsFolder + "/" + _databasePath;
                case FolderTypeEnum.Avatar:
                    return AvatarFolder + "/" + _databasePath;
                case FolderTypeEnum.Monster:
                    return MonsterFolder + "/" + _databasePath;
                case FolderTypeEnum.Npc:
                    return NpcFolder + "/" + _databasePath;
                case FolderTypeEnum.Trigger:
                    return TriggerFolder + "/" + _databasePath;
            }
            return _databasePath;
        }
    }

    public string BundlePath
    {
        get
        {
            return _bundlePath;
        }
    }

    public string BundleName
    {
        get
        {
            return _bundleName;
        }
    }

    public string AssetName
    {
        get
        {
            return _assetName;
        }
    }
}
