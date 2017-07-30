/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
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
            model.SubscribeMethodCall("OnDie", OnDie);
            model.SubscribeMethodCall("OnRespawn", OnRespawn);
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
            Model.DesubscribeMethodCall("OnDie", OnDie);
            Model.DesubscribeMethodCall("OnRespawn", OnRespawn);
            base.OnModelDestroy(objects);
        }
    }
}