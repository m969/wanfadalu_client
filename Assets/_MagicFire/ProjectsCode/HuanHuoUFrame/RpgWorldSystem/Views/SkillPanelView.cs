using DG.Tweening;
using UnityEngine.UI;

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
    
    
    public class SkillPanelView : SkillPanelViewBase {

        [SerializeField]
        private Image _skillSingingBar;
        private FloatReactiveProperty _value = new FloatReactiveProperty(1);

        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as SkillEntityViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.SkillEntity to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.

            _skillSingingBar.gameObject.SetActive(false);
            _value.Subscribe(x =>
            {
                _skillSingingBar.fillAmount = x;
            });
        }

        public override void OnSkillStartSingExecuted(OnSkillStartSingCommand command)
        {
            base.OnSkillStartSingExecuted(command);
            if (SkillEntity.isPlayer())
            {
                _value.Value = 1;
                _skillSingingBar.gameObject.SetActive(true);
                DOTween.To(() => _value.Value, x => _value.Value = x, 0f, command.singTime).OnComplete(() => { _skillSingingBar.gameObject.SetActive(false); });
            }
        }

    }
}
