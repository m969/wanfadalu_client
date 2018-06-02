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


    public class MainAvatarInfoPanelView : MainAvatarInfoPanelViewBase {
        [SerializeField]
        private Transform _gongFaListParent;
        [SerializeField]
        private Transform _skillListParent;
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

        private Dictionary<ulong, Prop> _magicWeaponList;

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
            this.OnEvent<ExitArenaEvent>().Subscribe(evt => { this.Avatar.Execute(new RequestExitArenaCommand()); });
            this.OnEvent<OnSelectGongFaEvent>().Subscribe(OnSelectGongFa);
            var worldViewService = uFrameKernel.Instance.Services.Find(_x => { return _x.GetType().Equals(typeof(WorldViewService)); }) as WorldViewService;
            transform.SetParent(worldViewService.MasterCanvas.transform);
            transform.localScale = new Vector3(1, 1, 1);
            transform.localEulerAngles = Vector3.zero;
            var rect = GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, 45);
        }

        public override void HPChanged(int arg1)
        {
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
            _mspMax = arg1;
            if (_msp.HasValue && _mspMax.HasValue)
            {
                if (_mspSliderImage != null)
                    _mspSliderImage.fillAmount = (float)_msp / (float)_mspMax;
                if (_mspAmountText != null)
                    _mspAmountText.text = "" + _msp + "/" + _mspMax;
            }
        }

        private void OnSelectGongFa(OnSelectGongFaEvent evt)
        {
            foreach (Transform item in _skillListParent)
            {
                var skillImage = item.GetComponent<Image>();
                var tempType = skillImage.sprite;
                var srcName = "Square";
                skillImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                skillImage.color = Color.black;
            }
            var gongFaID = evt.GongFaID;
            Observable.Range(0, 8).Subscribe(index => {
                var skillID = gongFaID * 10 + index;
                JToken skillToken = null;
                if (WorldViewService.ConfigTableMap["skill_config_Table"].TryGetValue(skillID.ToString(), out skillToken))
                {
                    var skillItem = _skillListParent.Find(index.ToString());
                    var skillImage = skillItem.GetComponent<Image>();
                    var tempType = skillImage.sprite;
                    var srcName = "SkillImages/skill_" + skillID;
                    skillImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                    skillImage.color = Color.white;
                }
            });
        }

        public override void gongFaKeyOptionsChanged(string arg1)
        {
            foreach (Transform item in _gongFaListParent)
            {
                var skillImage = item.GetComponent<Image>();
                var tempType = skillImage.sprite;
                var srcName = "Square";
                skillImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                skillImage.color = Color.black;
            }
            var gongFaKeyOptions = JObject.Parse(arg1);
            foreach (var item in gongFaKeyOptions)
            {
                var keyCode = item.Key;
                var gongFaID = int.Parse(item.Value.ToString());
                if (gongFaID == 0)
                    continue;
                var skillItem = _gongFaListParent.Find(keyCode);
                var gongFaImage = skillItem.GetComponent<Image>();
                var tempType = gongFaImage.sprite;
                var srcName = "GongFaImages/gongfa_" + gongFaID;
                gongFaImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                gongFaImage.color = Color.white;
            }
        }

        public override void gongFaListChanged(object arg1)
        {
            Debug.Log("MainAvatarInfoPanelView:gongFaListChanged");
            var gongFaMap = this.Avatar.DecodeGongFaListObject(arg1);
            foreach (var item in gongFaMap)
            {
                //var child = _gongFaListParent.GetChild(item.Value.index);
                //var srcName = "GongFaImages/gongfa_" + item.Value.gongFaID;
                //var itemImage = child.GetComponent<Image>();
                //var tempType = itemImage.sprite;
                //itemImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                //itemImage.color = Color.white;

                //var gongFaItem = Instantiate(_gongFaItemPrefab);
                //gongFaItem.SetParent(_gongFaContentTransform);
                //var itemImage = gongFaItem.Find("GongFaImage").GetComponent<Image>();
                //var tempType = itemImage.sprite;
                //Debug.Log(item.Key);
                //var srcName = "GongFaImages/gongfa_" + item.Key;
                //itemImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                //var skillList = gongFaItem.Find("GongFaInfoPanel").Find("SkillList");
                //gongFaItem.Find("GongFaInfoPanel").Find("GongFaName").GetComponent<Text>().text = item.Key.ToString();
                //foreach (var skill in item.Value)
                //{
                //    var skillItem = Instantiate(_skillItemPrefab);
                //    skillItem.SetParent(skillList);
                //    var skillImage = skillItem.GetComponent<Image>();
                //    tempType = skillImage.sprite;
                //    srcName = "SkillImages/skill_" + (item.Key * 10 + skill.Key);
                //    skillImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                //}
            }
            //foreach (var item in _magicWeaponList)
            //{
            //    var child = _weaponListParent.transform.GetChild(item.Value.index);
            //    var srcName = "GongFaImages/gongfa_" + item.Value.propData["id"].ToString();
            //    //Debug.Log(srcName);
            //    var itemImage = child.GetComponent<Image>();
            //    var tempType = itemImage.sprite;
            //    itemImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
            //}
        }

        //public override void magicWeaponListChanged(object arg1)
        //{
        //    //Debug.Log("MainAvatarInfoPanelView:magicWeaponListChanged ");
        //    var tmpPropList = ((Dictionary<string, object>)arg1)["values"] as List<object>;
        //    _magicWeaponList = new Dictionary<ulong, Prop>();
        //    if (tmpPropList != null)
        //    {
        //        foreach (var item in tmpPropList)
        //        {
        //            var propObject = (Dictionary<string, object>)item;
        //            var propData = JObject.Parse(propObject["propData"] as string);
        //            var prop = new Prop();
        //            prop.propUUID = (ulong)propObject["propUUID"];
        //            prop.index = (int)propObject["index"];
        //            prop.propData = propData;
        //            _magicWeaponList.Add(prop.propUUID, prop);
        //        }
        //    }
        //    foreach (var item in _magicWeaponList)
        //    {
        //        var child = _weaponListParent.transform.GetChild(item.Value.index);
        //        var srcName = "PropImages/prop_" + item.Value.propData["id"].ToString();
        //        //Debug.Log(srcName);
        //        var itemImage = child.GetComponent<Image>();
        //        var tempType = itemImage.sprite;
        //        itemImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
        //    }
        //}
    }
}
