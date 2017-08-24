using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MagicFire.Common;
using MagicFire.Mmorpg;
using UnityEngine;
using UnityEngine.UI;

public class ThreeDEntityPanelView : View
{
    protected virtual void FixedUpdate()
    {
        if (Model == null)
            return;
        var entityObj = ((KBEngine.Model)Model).renderObj as GameObject;
        if (entityObj == null)
            return;
        var v = new Vector3(entityObj.transform.position.x, entityObj.transform.position.z, -1);
        transform.DOLocalMove(v, 0f);
    }

    public override void InitializeView(IModel model)
    {
        base.InitializeView(model);
        transform.SetParent(SingletonGather.UiManager.Canvas3D.transform);

        var entity3DPanelPosition = new Vector3(((KBEngine.Model)model).position.x, ((KBEngine.Model)model).position.z, -1);
        transform.localPosition = entity3DPanelPosition;
        transform.localEulerAngles = Vector3.zero;
    }
}
