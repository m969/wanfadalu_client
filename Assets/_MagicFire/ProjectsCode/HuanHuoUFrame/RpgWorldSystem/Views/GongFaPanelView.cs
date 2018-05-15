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
    using GongFaName = System.String;
    using SkillName = System.String;

    public class GongFaPanelView : GongFaPanelViewBase {
        [SerializeField]
        private Transform _gongFaContentTransform;
        [SerializeField]
        private Transform _gongFaItemPrefab;
        [SerializeField]
        private Transform _skillItemPrefab;


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
        }

        public override void gongFaListChanged(object arg1)
        {
            var gongFaMap = this.Avatar.DecodeGongFaListObject(arg1);
            foreach (var item in gongFaMap)
            {
                var gongFaID = item.Value.gongFaID;
                var gongFaItem = Instantiate(_gongFaItemPrefab);
                gongFaItem.SetParent(_gongFaContentTransform);
                var itemImage = gongFaItem.Find("GongFaImage").GetComponent<Image>();
                var tempType = itemImage.sprite;
                var srcName = "GongFaImages/gongfa_" + gongFaID;
                itemImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                var skillList = gongFaItem.Find("GongFaInfoPanel").Find("SkillList");
                gongFaItem.Find("GongFaInfoPanel").Find("GongFaName").GetComponent<Text>().text = WorldViewService.ConfigTableMap["gongFa_config_Table"][gongFaID.ToString()]["name"].ToString();
                foreach (var skill in item.Value.skillList)
                {
                    var skillID = gongFaID * 10 + skill.Key;
                    var skillItem = Instantiate(_skillItemPrefab);
                    skillItem.SetParent(skillList);
                    var skillImage = skillItem.GetComponent<Image>();
                    tempType = skillImage.sprite;
                    srcName = "SkillImages/skill_" + skillID;
                    skillImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                    skillItem.Find("Text").GetComponent<Text>().text = WorldViewService.ConfigTableMap["skill_config_Table"][skillID.ToString()]["name"].ToString();
                }
            }
        }
    }
}
