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
    
    
    public class NpcView : NpcViewBase {
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as NpcViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Npc to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            this.OnMouseEvent(MouseEventType.OnMouseDown).Subscribe(evt =>
            {
                Debug.Log("NpcView:OnMouseDown");
                KBEngine.KBEngineApp.app.player().cellCall("requestDialog", Npc.id);
            });
        }

        public override void entityNameChanged(string arg1)
        {
            _entityName = arg1;
        }

        public override void npcIDChanged(int arg1)
        {
            _npcID = arg1;
        }

        public override void npcTypeChanged(int arg1)
        {
            _npcType = arg1;
        }

        public override void arenaIDChanged(int arg1)
        {
            _arenaID = arg1;
        }

        public override void OnDestroyExecuted(OnDestroyCommand command)
        {
        }

        public override void OnLeaveWorldExecuted(OnLeaveWorldCommand command)
        {
        }
    }
}
