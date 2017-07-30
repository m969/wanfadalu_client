/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *   类描述: 战斗对象的战斗信息面板
 * -------------------------- */
 using KBEngine;
using MagicFire.Common;
using UnityEngine.UI;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;
    using DG.Tweening;

    public class CombatEntityPanelView : EntityPanelView
    {
        private Slider _hpSlider;
        private Text _damageHint;
        private int _hp;
        private int _hpMax;

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);

            _hpSlider = GetComponentInChildren<Slider>();
            if (_hpSlider)
            {
                _damageHint = _hpSlider.transform.Find("DamageHint").GetComponent<Text>();
                _damageHint.gameObject.SetActive(false);
            }

            _damageHint.GetComponent<DOTweenAnimation>().onStepComplete.AddListener(HideDamageHint);    //掉血动画结束后隐藏动画

            //HpMax_Up(0);
            //Hp_Up(0);
            //model.SubscribePropertyUpdate(CombatPropertys.HpMax, HpMax_Up);
            //model.SubscribePropertyUpdate(CombatPropertys.Hp, Hp_Up);
        }

        public override void OnModelDestroy(object[] objects)
        {
            if (Model != null)
            {
                Model.DesubscribePropertyUpdate(CombatPropertys.HpMax, HpMax_Up);
                Model.DesubscribePropertyUpdate(CombatPropertys.Hp, Hp_Up);
            }
            base.OnModelDestroy(objects);
        }

        private void HpMax_Up(object old)
        {
            var hp = (int)Model.getDefinedProperty(CombatPropertys.Hp);
            var hpMax = (int)Model.getDefinedProperty(CombatPropertys.HpMax);

            if (Equals(_hp, hp) && Equals(_hpMax, hpMax))
                return;
            _hp = hp;
            _hpMax = hpMax;
            _hpSlider.value = (float)hp / hpMax;
        }

        private void Hp_Up(object old)
        {
            var hp = (int)Model.getDefinedProperty(CombatPropertys.Hp);
            var hpMax = (int)Model.getDefinedProperty(CombatPropertys.HpMax);

            if (_hp == hp && _hpMax == hpMax)
                return;
            _hp = hp;
            _hpMax = hpMax;
            _hpSlider.value = (float)hp / hpMax;
            _damageHint.text = "" + (hp - (int)old);
            _damageHint.gameObject.SetActive(true);
        }

        private void HideDamageHint()
        {
            _damageHint.gameObject.SetActive(false);
        }
    }
}