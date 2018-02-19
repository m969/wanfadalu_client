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
                //Debug.Log(item.Key + ":" + item.Value);
                var gongFaItem = Instantiate(_gongFaItemPrefab);
                gongFaItem.SetParent(_gongFaContentTransform);
                gongFaItem.Find("Text").GetComponent<Text>().text = item.Key.ToString();
            }
        }
    }
}
