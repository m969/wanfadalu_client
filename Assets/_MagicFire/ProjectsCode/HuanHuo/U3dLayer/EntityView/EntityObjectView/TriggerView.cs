/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *   类描述: 
 * -------------------------- */
using KBEngine;
using MagicFire.Common;

namespace MagicFire.Mmorpg
{
    using UnityEngine;
    using System.Collections;
    using System;
    using MagicFire.Common.Plugin;

    public class TriggerView : EntityObjectView
    {
        [SerializeField/*, Range(0, 4)*/]
        private int _triggerSize = 1;

        [SerializeField]
        private bool _adaptTriggerSize;

        //private GameObject _myTriggerObject;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(_triggerSize * 2, _triggerSize * 2, _triggerSize * 2));
        }

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);

            TriggerSize_Up(0);

            model.SubscribePropertyUpdate(KBEngine.Trigger.triggerSize, TriggerSize_Up);
        }

        private void InvokeMethod()
        {
            //_myTriggerObject.SetActive(true);
        }

        private void TriggerSize_Up(object old)
        {
            _triggerSize = (int)((KBEngine.Model)Model).getDefinedProperty(KBEngine.Trigger.triggerSize);
            if (_adaptTriggerSize)
            {
                transform.localScale = new Vector3(_triggerSize * 2, _triggerSize * 2, _triggerSize * 2);
            }
        }
    }
}
