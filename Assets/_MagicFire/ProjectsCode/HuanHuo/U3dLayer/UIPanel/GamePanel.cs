using System;
using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    public class GamePanel : Panel
    {
        [SerializeField]
        private Image _hpSliderImage;
        [SerializeField]
        private Image _mspSliderImage;
        [SerializeField]
        private Image _spSliderImage;
        [SerializeField]
        private Image _dsSliderImage;

        [SerializeField, Space(10)]
        private Text _hpAmountText;
        [SerializeField]
        private Text _mspAmountText;
        [SerializeField]
        private Text _spAmountText;
        [SerializeField]
        private Text _dsAmountText;

        [SerializeField, Space(10)]
        private Text _combatTimeBulletinText;

        private KBEngine.Model _avatar;

        protected override void Start()
        {
            base.Start();
            //StretchLayout();

            //ShowPanelByName("BagPanel");
            //ShowPanelByName("TheStorePanel");
            //ShowPanelByName("TaskInfoListPanel");
            //ShowPanelByName("FriendsListPanel");
            ShowPanelByName("CharacterInfoPanel");
            this.DelayExecuteRepeating(CombatTimeBulletin, 0, 1);
        }

        private void CombatTimeBulletin()
        {
            var currentTime = DateTime.Now;
            var taregtTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 21, 0, 0);
            var span = taregtTime.Subtract(currentTime);
            if (span.Ticks > 0)
            {
                _combatTimeBulletinText.text = "距离资源争夺战开始还有" + span.Hours + "小时" + span.Minutes + "分" + span.Seconds + "秒";
            }
            else
            {
                taregtTime = taregtTime.AddHours(24);
                span = taregtTime.Subtract(currentTime);
                _combatTimeBulletinText.text = "距离资源争夺战开始还有" + span.Hours + "小时" + span.Minutes + "分" + span.Seconds + "秒";
            }
        }

        //当主玩家激活
        public void OnMainAvatarActive(KBEngine.Model avatar)
        {
            if (avatar != null)
            {
                _avatar = avatar;
                Subscribe();
            }
        }

        //订阅属性更新、方法调用
        private void Subscribe()
        {
            if (_avatar == null)
                return;

            Hp_Up(null);
            HpMax_Up(null);

            Msp_Up(null);
            MspMax_Up(null);

            Sp_Up(null);
            SpMax_Up(null);

            _avatar.SubscribePropertyUpdate(KBEngine.Avatar.HealthSystem.HP, Hp_Up);
            _avatar.SubscribePropertyUpdate(KBEngine.Avatar.HealthSystem.HP_Max, HpMax_Up);

            _avatar.SubscribePropertyUpdate(KBEngine.Avatar.SuperPowerSystem.MSP, Msp_Up);
            _avatar.SubscribePropertyUpdate(KBEngine.Avatar.SuperPowerSystem.MSP_Max, MspMax_Up);

            _avatar.SubscribePropertyUpdate(KBEngine.Avatar.SuperPowerSystem.SP, Sp_Up);
            _avatar.SubscribePropertyUpdate(KBEngine.Avatar.SuperPowerSystem.SP_Max, SpMax_Up);

            _avatar.SubscribeMethodCall(KBEngine.Avatar.EntityObject.OnEntityDestroy, OnModelDestroy);
        }

        //取消订阅
        private void Desubscribe()
        {
            if (_avatar == null)
                return;
            _avatar.DesubscribePropertyUpdate(KBEngine.Avatar.HealthSystem.HP, Hp_Up);
            _avatar.DesubscribePropertyUpdate(KBEngine.Avatar.HealthSystem.HP_Max, HpMax_Up);

            _avatar.DesubscribePropertyUpdate(KBEngine.Avatar.SuperPowerSystem.MSP, Msp_Up);
            _avatar.DesubscribePropertyUpdate(KBEngine.Avatar.SuperPowerSystem.MSP_Max, MspMax_Up);

            _avatar.DesubscribePropertyUpdate(KBEngine.Avatar.SuperPowerSystem.SP, Sp_Up);
            _avatar.DesubscribePropertyUpdate(KBEngine.Avatar.SuperPowerSystem.SP_Max, SpMax_Up);

            _avatar.DesubscribeMethodCall(KBEngine.Avatar.EntityObject.OnEntityDestroy, OnModelDestroy);
        }

        private void OnModelDestroy(object[] objects)
        {
            Desubscribe();
        }

        //生命值UI更新
        private void Hp_Up(object old)
        {
            if (_avatar != null)
            {
                var hp = (int)_avatar.getDefinedProperty(CombatPropertys.Hp);
                var maxHp = (int)_avatar.getDefinedProperty(CombatPropertys.HpMax);
                if (_hpSliderImage != null)
                    _hpSliderImage.fillAmount = (float)hp / maxHp;
                if (_hpAmountText != null)
                    _hpAmountText.text = "" + hp + "/" + maxHp;
            }
        }

        //生命值上限UI更新
        private void HpMax_Up(object old)
        {
            if (_avatar != null)
            {
                var hp = (int)_avatar.getDefinedProperty(CombatPropertys.Hp);
                var maxHp = (int)_avatar.getDefinedProperty(CombatPropertys.HpMax);
                if (_hpSliderImage != null)
                    _hpSliderImage.fillAmount = (float)hp / maxHp;
                if (_hpAmountText != null)
                    _hpAmountText.text = "" + hp + "/" + maxHp;
            }
        }

        //最大一次灵力释放量UI更新
        private void Msp_Up(object old)
        {
            if (_avatar != null)
            {
                var mp = (int)_avatar.getDefinedProperty(AvatarPropertys.Msp);
                var maxMp = (int)_avatar.getDefinedProperty(AvatarPropertys.MspMax);
                if (_mspSliderImage != null)
                    _mspSliderImage.fillAmount = (float)mp / maxMp;
                if (_mspAmountText != null)
                    _mspAmountText.text = "" + mp + "/" + maxMp;
            }
        }

        //最大一次灵力释放量上限UI更新
        private void MspMax_Up(object old)
        {
            if (_avatar != null)
            {
                var mp = (int)_avatar.getDefinedProperty(AvatarPropertys.Msp);
                var maxMp = (int)_avatar.getDefinedProperty(AvatarPropertys.MspMax);
                if (_mspSliderImage != null)
                    _mspSliderImage.fillAmount = (float)mp / maxMp;
                if (_mspAmountText != null)
                    _mspAmountText.text = "" + mp + "/" + maxMp;
            }
        }

        //灵力总量UI更新
        private void Sp_Up(object old)
        {
            if (_avatar != null)
            {
                var sp = (int)_avatar.getDefinedProperty(AvatarPropertys.Sp);
                var maxSp = (int)_avatar.getDefinedProperty(AvatarPropertys.SpMax);
                if (_spSliderImage != null)
                    _spSliderImage.fillAmount = (float)sp / maxSp;
                if (_spAmountText != null)
                    _spAmountText.text = "" + sp + "/" + maxSp;
            }
        }

        //灵力总量上限UI更新
        private void SpMax_Up(object old)
        {
            if (_avatar != null)
            {
                var sp = (int)_avatar.getDefinedProperty(AvatarPropertys.Sp);
                var maxSp = (int)_avatar.getDefinedProperty(AvatarPropertys.SpMax);
                if (_spSliderImage != null)
                    _spSliderImage.fillAmount = (float)sp / maxSp;
                if (_spAmountText != null)
                    _spAmountText.text = "" + sp + "/" + maxSp;
            }
        }

        public void ExitGame()
        {
            if (KBEngine.KBEngineApp.app != null)
            {
                KBEngine.KBEngineApp.app._closeNetwork(KBEngine.KBEngineApp.app.networkInterface());
                KBEngine.KBEngineApp.app.destroy();
            }
            UnityEngine.Application.Quit();
        }

        public void ShowPanelByName(string panelName)
        {
            var panel = UiManager.Instance.TryGetOrCreatePanel(panelName);
            ActiveOrHide(panel);
        }

        private void ActiveOrHide(GameObject panel)
        {
            if (panel == null)
                return;
            if (panel.activeInHierarchy)
                panel.SetActive(false);
            else
                panel.SetActive(true);
        }
    }

}