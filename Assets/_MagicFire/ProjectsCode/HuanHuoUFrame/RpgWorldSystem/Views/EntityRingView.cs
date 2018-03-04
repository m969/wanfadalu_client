using DG.Tweening;
using uFrame.IOC;

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
    
    
    public class EntityRingView : EntityRingViewBase
    {
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

            this.Bindings.Add(
                Observable.EveryFixedUpdate().Subscribe(evt =>
                {
                    var entityObj = ViewModelObject.renderObj as GameObject;
                    if (entityObj == null)
                        return;
                    var v = new Vector3(entityObj.transform.position.x, entityObj.transform.position.z, -0.1f);
                    transform.DOLocalMove(v, 0.01f);
                })
            );

            if (WorldViewService)
                transform.SetParent(WorldViewService.Canvas3D.transform);
            else
                Debug.LogError("WorldViewService is null");

            var entity3DPanelPosition = new Vector3(ViewModelObject.position.x, ViewModelObject.position.z, -1);
            transform.localPosition = entity3DPanelPosition;
            transform.localEulerAngles = Vector3.zero;
        }
    }
}
