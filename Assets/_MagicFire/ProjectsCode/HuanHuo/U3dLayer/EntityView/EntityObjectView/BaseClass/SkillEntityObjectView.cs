/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Chengmin Yang
 *    Date: 2017/08/25
 *    描述： 
 * -------------------------- */

using System.Collections.Generic;
using MagicFire.Common.Plugin;
using MagicFire.SceneManagement;
using PathologicalGames;

namespace MagicFire.Mmorpg
{
    using UnityEngine;
    using System.Collections;
    using System;
    using System.Reflection;
    using MagicFire.Common;
    using Object = UnityEngine.Object;
    using Avatar = KBEngine.Avatar;

    public abstract class SkillEntityObjectView : CombatEntityObjectView
    {
        private GameObject _iceImprisonEffect;

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);
            Model.SubscribePropertyUpdate(Avatar.SkillSystem.isIceFreezing, IsIceFreezing_Up);
        }

        public override void OnModelDestroy(object[] objects)
        {
            Model.DesubscribePropertyUpdate(Avatar.SkillSystem.isIceFreezing, IsIceFreezing_Up);
            base.OnModelDestroy(objects);
        }

        private void IsIceFreezing_Up(object old)
        {
            var isIceFreezing = (int)Model.getDefinedProperty(Avatar.SkillSystem.isIceFreezing);
            if (isIceFreezing != 0)
            {
                if (_iceImprisonEffect == null)
                {
                    //_iceImprisonEffect = SingletonGather.ViewObjectPool.Spawn(SingletonGather.ViewObjectPool.prefabs["SkillE_IceImprisonEffect"]).gameObject;
                    if (!SingletonGather.ViewObjectPool.prefabs.ContainsKey("SkillE_IceImprisonEffect"))
                    {
                        XmlSceneManager.Message1 += "Not ContainsKey SkillE_IceImprisonEffect";
                        var pool = new PrefabPool();
                        pool.prefab = AssetTool.LoadTriggerAssetByName("SkillE_IceImprisonEffect") as Transform;
                        pool.preloadAmount = 3;
                        SingletonGather.ViewObjectPool.CreatePrefabPool(pool);
                    }
                    _iceImprisonEffect = SingletonGather.ViewObjectPool.Spawn("SkillE_IceImprisonEffect").gameObject;

                    if (_iceImprisonEffect != null)
                        _iceImprisonEffect.transform.SetParent(transform);
                }
                if (_iceImprisonEffect != null)
                {
                    _iceImprisonEffect.SetActive(true);
                    _iceImprisonEffect.transform.localPosition = new Vector3(0, 5, 1);
                }
            }
            else
            {
                if (_iceImprisonEffect != null)
                {
                    _iceImprisonEffect.SetActive(false);
                }
            }
        }
    }
}
