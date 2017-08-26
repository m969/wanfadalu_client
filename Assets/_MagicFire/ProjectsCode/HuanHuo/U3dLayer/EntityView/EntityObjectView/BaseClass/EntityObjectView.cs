/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *    Date: 2017/02/20
 *    描述： 
 * -------------------------- */

using DG.Tweening;

namespace MagicFire.Mmorpg
{
    using System;
    using UnityEngine;
    using System.Collections;
    using Mmorpg.UI;
    using MagicFire.Common;
    using KBEngine;
    using Model = KBEngine.Model;

    public abstract class EntityObjectView : View
    {
        public string EntityName { get; set; }

        protected virtual void FixedUpdate()
        {
            if (Model == null)
                return;
            var v = ((KBEngine.Model)Model).position;
            transform.DOMove(new Vector3(v.x, v.y, v.z), 0.25f);
            transform.eulerAngles = new Vector3(((KBEngine.Model)Model).direction.x, ((KBEngine.Model)Model).direction.z, ((KBEngine.Model)Model).direction.y);
        }

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);
            EntityName_Up(0);
            model.SubscribePropertyUpdate(KBEngine.Avatar.EntityObject.entityName, EntityName_Up);
        }

        public override void OnModelDestroy(object[] objects)
        {
            if (Model != null)
                Model.DesubscribePropertyUpdate(KBEngine.Avatar.EntityObject.entityName, EntityName_Up);
            base.OnModelDestroy(objects);
        }

        public void EntityName_Up(object old)
        {
            if (EntityName == (string)Model.getDefinedProperty(KBEngine.Avatar.EntityObject.entityName))
                return;
            EntityName = (string)Model.getDefinedProperty(KBEngine.Avatar.EntityObject.entityName);
            gameObject.name = ((KBEngine.Model)Model).className + ":" + EntityName;
        }
    }
}
