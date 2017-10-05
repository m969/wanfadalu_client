using MagicFire.Common.Plugin;
using MagicFire.SceneManagement;
using PathologicalGames;

namespace MagicFire.HuanHuoUFrame {
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
    
    
    public class SkillEntityView : SkillEntityViewBase {

        private GameObject _iceImprisonEffect;

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
        }

        public override void isIceFreezingChanged(Int32 arg1)
        {
            var isIceFreezing = arg1;
            if (isIceFreezing != 0)
            {
                if (_iceImprisonEffect == null)
                {
                    _iceImprisonEffect = PoolManager.Pools["SkillTrajectoryPool"].Spawn("SkillE_IceImprisonEffect").gameObject;

                    if (_iceImprisonEffect != null)
                        _iceImprisonEffect.transform.SetParent(transform);
                }
                if (_iceImprisonEffect != null)
                {
                    _iceImprisonEffect.SetActive(true);
                    _iceImprisonEffect.transform.localPosition = new Vector3(0, 5, 1);
                }
            }
            else
            {
                if (_iceImprisonEffect != null)
                {
                    _iceImprisonEffect.SetActive(false);
                }
            }
        }

    }
}
