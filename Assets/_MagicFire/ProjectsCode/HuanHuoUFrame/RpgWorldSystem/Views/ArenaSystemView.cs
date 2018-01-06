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
    
    
    public class ArenaSystemView : ArenaSystemViewBase {
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as ArenaSystemViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.ArenaSystem to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

        public override void OnEnterArenaExecuted(OnEnterArenaCommand command)
        {
            base.OnEnterArenaExecuted(command);
            Debug.Log("ArenaView:OnEnterArenaExecuted");
            this.transform.position = command.CenterPosition;
        }

        public override void OnExitArenaExecuted(OnExitArenaCommand command)
        {
            base.OnExitArenaExecuted(command);
            Debug.Log("ArenaView:OnExitArenaExecuted");
            this.transform.position = command.OutPosition;
        }
    }
}
