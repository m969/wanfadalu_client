using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MagicFire.Common;
using MagicFire.Mmorpg;
using UnityEngine;
using UnityEngine.UI;

public class CombatEntityThreeDPanelView : ThreeDEntityPanelView
{
    private Image _hpImage;
    private Image _dpImage;
    private int _hp;
    private int _hpMax;
    private int _dp;
    private int _dpMax;

    public override void InitializeView(IModel model)
    {
        base.InitializeView(model);
        _hpImage = transform.Find("Ring/HP").GetComponent<Image>();

        Hp_Up(0);
        HpMax_Up(0);
        model.SubscribePropertyUpdate(CombatPropertys.Hp, Hp_Up);
        model.SubscribePropertyUpdate(CombatPropertys.HpMax, HpMax_Up);
    }

    public override void OnModelDestrooy(object[] objects)
    {
        if (Model != null)
        {
            Model.DesubscribePropertyUpdate(CombatPropertys.HpMax, HpMax_Up);
            Model.DesubscribePropertyUpdate(CombatPropertys.Hp, Hp_Up);
        }
        base.OnModelDestrooy(objects);
    }

    private void Hp_Up(object old)
    {
        var hp = (int)Model.getDefinedProperty(CombatPropertys.Hp);
        var hpMax = (int)Model.getDefinedProperty(CombatPropertys.HpMax);

        if (_hp == hp && _hpMax == hpMax)
            return;
        _hp = hp;
        _hpMax = hpMax;
        _hpImage.fillAmount = ((float)hp / hpMax) * 0.25f;
    }

    private void HpMax_Up(object old)
    {
        var hp = (int)Model.getDefinedProperty(CombatPropertys.Hp);
        var hpMax = (int)Model.getDefinedProperty(CombatPropertys.HpMax);

        if (Equals(_hp, hp) && Equals(_hpMax, hpMax))
            return;
        _hp = hp;
        _hpMax = hpMax;
        _hpImage.fillAmount = ((float)hp / hpMax) * 0.25f;
    }

}
