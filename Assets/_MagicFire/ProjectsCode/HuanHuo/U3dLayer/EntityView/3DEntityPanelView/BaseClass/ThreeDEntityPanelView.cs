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
        var entityObj = ((KBEngine.Model)Model).renderObj as GameObject;
        if (entityObj == null)
            return;
        var v = new Vector3(entityObj.transform.position.x, entityObj.transform.position.z, -1);
        transform.DOLocalMove(v, 0f);
    }
}
