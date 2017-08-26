/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Ch3ngmin Yang
 *    Date: 2017/02/20
 *    描述： 
 * -------------------------- */

using MagicFire.Common.Plugin;

namespace MagicFire.Mmorpg
{
    using UnityEngine;
    using System.Collections;
    using System;
    using System.Reflection;
    using MagicFire.Common;
    using Object = UnityEngine.Object;

    public abstract class CombatEntityObjectView : EntityObjectView
    {
        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);
            model.SubscribeMethodCall(KBEngine.Avatar.HealthSystem.OnDie, OnDie);
            model.SubscribeMethodCall(KBEngine.Avatar.HealthSystem.OnRespawn, OnRespawn);
        }

        protected virtual void OnDie(object[] args)
        {
            Instantiate(
                AssetTool.LoadEffectAssetByName("DieEffect"),
                transform.position, 
                transform.rotation);
        }

        protected virtual void OnRespawn(object[] args)
        {
            
        }

        public override void OnModelDestroy(object[] objects)
        {
            Model.DesubscribeMethodCall(KBEngine.Avatar.HealthSystem.OnDie, OnDie);
            Model.DesubscribeMethodCall(KBEngine.Avatar.HealthSystem.OnRespawn, OnRespawn);
            base.OnModelDestroy(objects);
        }
    }
}