namespace MagicFire.HuanHuoUFrame {
    using MagicFire.HuanHuoUFrame;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.Services;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    using UnityEngine.UI;
    using Newtonsoft.Json.Linq;
    using PathologicalGames;


    public struct Prop
    {
        public string propUUID;
        public JObject propData;
    }


    public class MainAvatarInfoPanelView : MainAvatarInfoPanelViewBase {
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

        private int? _hp;
        private int? _hpMax;
        private int? _dp;
        private int? _dpMax;
        private int? _sp;
        private int? _spMax;
        private int? _msp;
        private int? _mspMax;

        private Dictionary<string, Prop> _propList;
        private Dictionary<int, string> _magicWeaponList;

        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as AvatarViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Avatar to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.

            this.OnEvent<ExitArenaEvent>()
                .Subscribe(evt =>
                {
                    this.Avatar.Execute(new RequestExitArenaCommand());
                });

            transform.SetParent(WorldViewService.MasterCanvas.transform);
            transform.localScale = new Vector3(1, 1, 1);
            transform.localEulerAngles = Vector3.zero;
            var rect = GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, 50);
        }

        public override void HPChanged(int arg1)
        {
            base.HPChanged(arg1);

            _hp = arg1;
            if (_hp.HasValue && _hpMax.HasValue)
            {
                if (_hpSliderImage != null)
                    _hpSliderImage.fillAmount = (float)_hp.Value / _hpMax.Value;
                if (_hpAmountText != null)
                    _hpAmountText.text = "" + _hp.Value + "/" + _hpMax.Value;
            }
        }

        public override void HP_MaxChanged(int arg1)
        {
            base.HP_MaxChanged(arg1);

            _hpMax = arg1;
            if (_hp.HasValue && _hpMax.HasValue)
            {
                if (_hpSliderImage != null)
                    _hpSliderImage.fillAmount = (float)_hp.Value / _hpMax.Value;
                if (_hpAmountText != null)
                    _hpAmountText.text = "" + _hp.Value + "/" + _hpMax.Value;
            }
        }

        public override void SPChanged(int arg1)
        {
            base.SPChanged(arg1);

            _sp = arg1;
            if (_sp.HasValue && _spMax.HasValue)
            {
                if (_spSliderImage != null)
                    _spSliderImage.fillAmount = (float)_sp / (float)_spMax;
                if (_spAmountText != null)
                    _spAmountText.text = "" + _sp + "/" + _spMax;
            }
        }

        public override void SP_MaxChanged(int arg1)
        {
            base.SP_MaxChanged(arg1);
            
            _spMax = arg1;
            if (_sp.HasValue && _spMax.HasValue)
            {
                if (_spSliderImage != null)
                    _spSliderImage.fillAmount = (float)_sp / (float)_spMax;
                if (_spAmountText != null)
                    _spAmountText.text = "" + _sp + "/" + _spMax;
            }
        }

        public override void MSPChanged(int arg1)
        {
            base.MSPChanged(arg1);

            _msp = arg1;
            if (_msp.HasValue && _mspMax.HasValue)
            {
                if (_mspSliderImage != null)
                    _mspSliderImage.fillAmount = (float)_msp / (float)_mspMax;
                if (_mspAmountText != null)
                    _mspAmountText.text = "" + _msp + "/" + _mspMax;
            }
        }

        public override void MSP_MaxChanged(int arg1)
        {
            base.MSP_MaxChanged(arg1);

            _mspMax = arg1;
            if (_msp.HasValue && _mspMax.HasValue)
            {
                if (_mspSliderImage != null)
                    _mspSliderImage.fillAmount = (float)_msp / (float)_mspMax;
                if (_mspAmountText != null)
                    _mspAmountText.text = "" + _msp + "/" + _mspMax;
            }
        }

        public override void propListChanged(object arg1)
        {
            base.propListChanged(arg1);
            Debug.Log("MainAvatarInfoPanelView:propListChanged ");
            var tmpPropList = ((Dictionary<string, object>)arg1)["values"] as List<object>;
            if (_propList == null)
                _propList = new Dictionary<string, Prop>();
            if (tmpPropList != null)
            {
                foreach (var item in tmpPropList)
                {
                    var propObject = (Dictionary<string, object>)item;
                    var propData = JObject.Parse(propObject["propData"] as string);
                    var prop = new Prop();
                    prop.propUUID = propObject["propUUID"] as string;
                    prop.propData = propData;
                    _propList.Add(prop.propUUID, prop);
                }
            }
        }

        public override void magicWeaponListChanged(object arg1)
        {
            base.magicWeaponListChanged(arg1);
            Debug.Log("MainAvatarInfoPanelView:magicWeaponListChanged ");
            var tmpList = ((Dictionary<string, object>)arg1)["values"] as List<object>;
            if (_magicWeaponList == null)
                _magicWeaponList = new Dictionary<int, string>();
            if (tmpList != null)
            {
                foreach (var item in tmpList)
                {
                    var value = (Dictionary<string, object>)item;
                    _magicWeaponList.Add((int)value["index"], value["propUUID"] as string);
                    Prop prop;
                    if (_propList.TryGetValue(value["propUUID"] as string, out prop))
                    {
                        var index = (int)value["index"];
                        var propID = (int)prop.propData["id"];
                        Debug.Log("index = " + index);
                        Debug.Log("propID = " + propID);
                    }
                }
            }
        }
    }
}
