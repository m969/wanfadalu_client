using DG.Tweening;

namespace eMagicFire.HuanHuoUFram {
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
    using DG.Tweening;


    public class AvatarPanelView : AvatarPanelViewBase
    {
        [SerializeField]
        private Text _damageHintText;

        private int? _hp;

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

            _damageHintText.GetComponent<DOTweenAnimation>().onStepComplete.AddListener(() => { _damageHintText.gameObject.SetActive(false); });    //掉血动画结束后隐藏动画
        }

        public override void HPChanged(int arg1)
        {
            base.HPChanged(arg1);

            if (_hp.HasValue)
            {
                var changeValue = arg1 - _hp;
                if (changeValue > 0)
                {
                    _damageHintText.color = Color.green;
                }
                if (changeValue < 0)
                {
                    _damageHintText.color = Color.red;
                }
                _damageHintText.text = changeValue.ToString();
                _damageHintText.gameObject.SetActive(true);
            }
            _hp = arg1;
        }

        public override void ExecuteOnSkillStartCast(OnSkillStartCastCommand command)
        {
            base.ExecuteOnSkillStartCast(command);
            ((IdleState)Avatar.avatarState).CastSkillTransition();
            Avatar.avatarStateProperty.CastSkillState.OnEntering(Avatar.avatarState);
        }
    }
}
