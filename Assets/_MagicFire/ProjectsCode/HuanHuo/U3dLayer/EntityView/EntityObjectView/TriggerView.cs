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

            //if (model != null)
            //{
            //    var entityName 
            //        = transform.Find("Name").GetComponent<TextMesh>().text
            //        = (string)model.getDefinedProperty("entityName");
            //    transform.Find("Name").GetComponent<TextMesh>().text = "";

            //    //if (entityName == "GateWayTrigger")
            //    //{
            //    //    //transform.Find("Name").GetComponent<TextMesh>().text
            //    //    //    = (string)model1.getDefinedProperty("name");

            //    //    var trigger =
            //    //        Instantiate(
            //    //            AssetTool.LoadAsset_Database_Or_Bundle(
            //    //                AssetTool.Assets__Prefabs_ + "Trigger/GateWayTrigger/GateWayTrigger.prefab",
            //    //                "Prefabs",
            //    //                "trigger_bundle",
            //    //                "GateWayTrigger")) as GameObject;
            //    //    if (trigger != null)
            //    //    {
            //    //        trigger.transform.SetParent(transform);
            //    //        trigger.transform.localPosition = new Vector3(0, 0, 0);
            //    //    }
            //    //}
            //    //else
            //    //{

            //    //}
            //    var trigger =
            //            Instantiate(
            //                AssetTool.LoadAsset_Database_Or_Bundle(
            //                    AssetTool.Assets__Prefabs_ + "Trigger/" + entityName + ".prefab",
            //                    "Prefabs",
            //                    "trigger_bundle",
            //                    entityName)) as GameObject;
            //    if (trigger != null)
            //    {
            //        //trigger.SetActive(false);
            //        trigger.transform.SetParent(transform);
            //        trigger.transform.localPosition = new Vector3(0, 0, 0);
            //        trigger.transform.eulerAngles = Vector3.zero;
            //        _myTriggerObject = trigger;
            //        //Invoke("InvokeMethod", 0.1f);
            //    }
            //    else
            //    {
            //        Debug.LogError(entityName + ".prefab is null");
            //    }
            //}

            TriggerSize_Up(0);

            model.SubscribePropertyUpdate(TriggerPeopertys.TriggerSize, TriggerSize_Up);
        }

        private void InvokeMethod()
        {
            //_myTriggerObject.SetActive(true);
        }

        private void TriggerSize_Up(object old)
        {
            _triggerSize = (int)((KBEngine.Model)Model).getDefinedProperty(TriggerPeopertys.TriggerSize);
            if (_adaptTriggerSize)
            {
                transform.localScale = new Vector3(_triggerSize * 2, _triggerSize * 2, _triggerSize * 2);
            }
        }
    }
}
