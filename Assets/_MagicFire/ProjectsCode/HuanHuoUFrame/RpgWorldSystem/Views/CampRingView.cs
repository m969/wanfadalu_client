using UnityEngine.UI;

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
    
    
    public class CampRingView : CampRingViewBase
    {
        [SerializeField]
        private Image _campImage;

        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as CampEntityViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.CampEntity to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

        public override void campNameChanged(String arg1)
        {
            if (EntityCommon.id == AvatarViewModel.MainAvatar.id)
            {
                _campImage.color = new Color(0.0f, 32.0f / 255.0f, 0.0f);
                return;
            }
            if (arg1 == AvatarViewModel.MainAvatar.campName)
            {
                _campImage.color = new Color(0, 0, 32.0f / 255.0f);
            }
            else
            {
                _campImage.color = new Color(32.0f / 255.0f, 0, 0);
            }
        }
    }
}
