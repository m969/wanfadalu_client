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


    public int Up_HP
    {
        set
        {
            Debug.Log("Up_HP " + value);
            _hp = value;

            _hp = (int)Model.getDefinedProperty(KBEngine.Avatar.HealthSystem.HP);
            _hpMax = (int)Model.getDefinedProperty(KBEngine.Avatar.HealthSystem.HP_Max);

            _hpImage.fillAmount = ((float)_hp / _hpMax) * 0.25f;
        }
    }

    public int Up_HP_Max
    {
        set
        {
            Debug.Log("Up_HP_Max " + value);
            _hp = value;

            _hp = (int)Model.getDefinedProperty(KBEngine.Avatar.HealthSystem.HP);
            _hpMax = (int)Model.getDefinedProperty(KBEngine.Avatar.HealthSystem.HP_Max);

            _hpImage.fillAmount = ((float)_hp / _hpMax) * 0.25f;
        }
    }


    public override void InitializeView(IModel model)
    {
        base.InitializeView(model);
        _hpImage = transform.Find("Ring/HPOutline/HP").GetComponent<Image>();

        Hp_Up(0);
        HpMax_Up(0);
        model.SubscribePropertyUpdate(KBEngine.Avatar.HealthSystem.HP, Hp_Up);
        model.SubscribePropertyUpdate(KBEngine.Avatar.HealthSystem.HP_Max, HpMax_Up);
    }

    public override void OnModelDestroy(object[] objects)
    {
        if (Model != null)
        {
            Model.DesubscribePropertyUpdate(KBEngine.Avatar.HealthSystem.HP_Max, HpMax_Up);
            Model.DesubscribePropertyUpdate(KBEngine.Avatar.HealthSystem.HP, Hp_Up);
        }
        base.OnModelDestroy(objects);
    }

    private void Hp_Up(object old)
    {
        var hp = (int)Model.getDefinedProperty(KBEngine.Avatar.HealthSystem.HP);
        var hpMax = (int)Model.getDefinedProperty(KBEngine.Avatar.HealthSystem.HP_Max);

        if (_hp == hp && _hpMax == hpMax)
            return;
        _hp = hp;
        _hpMax = hpMax;
        _hpImage.fillAmount = ((float)hp / hpMax) * 0.25f;
    }

    private void HpMax_Up(object old)
    {
        var hp = (int)Model.getDefinedProperty(KBEngine.Avatar.HealthSystem.HP);
        var hpMax = (int)Model.getDefinedProperty(KBEngine.Avatar.HealthSystem.HP_Max);

        if (Equals(_hp, hp) && Equals(_hpMax, hpMax))
            return;
        _hp = hp;
        _hpMax = hpMax;
        _hpImage.fillAmount = ((float)hp / hpMax) * 0.25f;
    }
}
