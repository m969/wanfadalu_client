namespace MagicFire.HuanHuoMVVM{
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
    
    
    public class LoginPanel : LoginPanelBase
    {
        public int CurrentServerNum
        {
            set
            {
                (ViewModelObject as UserLoginScreenViewModel).CurrentServerNum = value;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as LoginPanelViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            _RegistePanel.ViewModelObject = model;
            _RegistePanel.Bind();
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.LoginPanel to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }
    }
}
