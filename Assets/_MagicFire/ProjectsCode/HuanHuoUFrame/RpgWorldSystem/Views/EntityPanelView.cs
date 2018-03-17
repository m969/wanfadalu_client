using DG.Tweening;

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
    
    
    public class EntityPanelView : EntityPanelViewBase {
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as EntityCommonViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.EntityCommon to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            this.Bindings.Add(
                Observable.EveryFixedUpdate().Subscribe(evt =>
                {
                    var entityObj = ViewModelObject.renderObj as GameObject;
                    var v = new Vector3(entityObj.transform.position.x, entityObj.transform.position.y + 2.0f, entityObj.transform.position.z);
                    transform.DOLocalMove(v, 0.01f);
                    transform.eulerAngles = Camera.main.transform.eulerAngles;
                    //var v = Camera.main.WorldToScreenPoint(entityObj.transform.position);
                    //transform.DOMove(new Vector3(v.x, v.y, 0), 0.1f);
                })
            );
            transform.SetParent(WorldViewService.Canvas3D.transform);
            //var entity3DPanelPosition = new Vector3(ViewModelObject.position.x, ViewModelObject.position.z, -1);
            //transform.localPosition = entity3DPanelPosition;
            //transform.localEulerAngles = Vector3.zero;
            //var entityPanelPosition = Camera.main.WorldToScreenPoint(ViewModelObject.position);
            //entityPanelPosition = new Vector3(entityPanelPosition.x, entityPanelPosition.y, 0);
            //transform.localPosition = entityPanelPosition;
            //transform.localEulerAngles = Vector3.zero;
            //transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
