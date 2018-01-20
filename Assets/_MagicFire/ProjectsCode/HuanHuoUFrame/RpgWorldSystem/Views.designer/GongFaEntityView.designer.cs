// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

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
    
    
    public class GongFaEntityViewBase : MotionSystemView {
        
        [UnityEngine.SerializeField()]
        [uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public object _gongFaList;
        
        [uFrame.MVVM.Attributes.UFToggleGroup("gongFaList")]
        [UnityEngine.HideInInspector()]
        public bool _BindgongFaList = true;
        
        [uFrame.MVVM.Attributes.UFGroup("gongFaList")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_gongFaListonlyWhenChanged")]
        protected bool _gongFaListOnlyWhenChanged;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(GongFaEntityViewModel);
            }
        }
        
        public GongFaEntityViewModel GongFaEntity {
            get {
                return (GongFaEntityViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as GongFaEntityViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var gongfaentityview = ((GongFaEntityViewModel)model);
            gongfaentityview.gongFaList = this._gongFaList;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.GongFaEntity to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindgongFaList) {
                this.BindProperty(this.GongFaEntity.gongFaListProperty, this.gongFaListChanged, _gongFaListOnlyWhenChanged);
            }
        }
        
        public virtual void gongFaListChanged(object arg1) {
        }
        
        public virtual void ExecutelearnGongFa(learnGongFaCommand command) {
            command.Sender = GongFaEntity;
            GongFaEntity.learnGongFa.OnNext(command);
        }
    }
}