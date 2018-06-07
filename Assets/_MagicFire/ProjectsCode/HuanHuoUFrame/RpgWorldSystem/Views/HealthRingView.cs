﻿namespace MagicFire.HuanHuoUFrame {
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


    public class HealthRingView : HealthRingViewBase {
        [SerializeField]
        private Image _hpImage;
        private int? _hp;
        private int? _hpMax;

        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as HealthEntityViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.HealthEntity to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

        public override void HP_MaxChanged(int arg1)
        {
            base.HP_MaxChanged(arg1);

            _hpMax = arg1;
            if (_hp.HasValue && _hpMax.HasValue)
                _hpImage.fillAmount = ((float)_hp.Value / _hpMax.Value) * 0.25f;
        }

        public override void HPChanged(int arg1)
        {
            base.HPChanged(arg1);

            _hp = arg1;
            if (_hp.HasValue && _hpMax.HasValue)
                _hpImage.fillAmount = ((float)_hp.Value / _hpMax.Value) * 0.25f;
        }
    }
}
