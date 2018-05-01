using DG.Tweening;

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
    
    
    public class EntityModelView : EntityModelViewBase {
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as SuperPowerEntityViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.SuperPowerEntity to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (!this.EntityCommon.isPlayer())
            {
                this.Bindings.Add(
                    Observable.EveryFixedUpdate().Subscribe(evt =>
                    {
                        transform.DOMove(ViewModelObject.position, 0.2f);
                        var dir = ViewModelObject.direction;
                        transform.eulerAngles = new Vector3(dir.x, dir.z, dir.y);
                        if (this.EntityCommon.className == "Avatar")
                            Debug.Log("EntityModelView: " + transform.eulerAngles);
                    })
                );
            }
            ViewModelObject.renderObj = gameObject;
            //transform.position = ViewModelObject.position;
            //var dirt = ViewModelObject.direction;
            //transform.eulerAngles = new Vector3(dirt.x, dirt.z, dirt.y);
        }
    }
}
