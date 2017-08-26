using MagicFire.Common.Plugin;
using MagicFire.Mmorpg;
using MagicFire.Mmorpg.AvatarInputState;
using PathologicalGames;

namespace MagicFire.Common
{
    using UnityEngine;
    using System.Collections;

    /// <summary>
    /// 单例聚合器
    /// </summary>
    public partial class SingletonGather
    {
        public static WorldMediator WorldMediator { get { return MagicFire.Mmorpg.WorldMediator.Instance; } }

        public static UiManager UiManager { get { return Common.UiManager.Instance; } }

        public static GameManager GameManager { get { return Common.GameManager.Instance; } }

        public static AvatarStateController AvatarStateController { get { return AvatarStateController.Instance; } }

        public static PlayerTarget PlayerTarget { get { return PlayerTarget.Instance; } }

        public static SpawnPool ViewObjectPool
        {
            get
            {
                if (PoolManager.Pools.ContainsKey("ViewObjectPool") == false)
                {
                    var asset = AssetTool.LoadAuxiliaryAssetByName("ViewObjectPool");
                    var pool = Object.Instantiate(asset, Vector3.zero, Quaternion.identity);
                    ((GameObject)pool).name = "ViewObjectPool";
                }
                return PoolManager.Pools["ViewObjectPool"];
            }
        }
    }

}