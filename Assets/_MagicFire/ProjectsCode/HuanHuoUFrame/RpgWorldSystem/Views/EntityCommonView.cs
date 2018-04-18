using PathologicalGames;
using uFrame.IOC;
using uFrame.MVVM.Views;

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
    using DG.Tweening;
    
    
    public class EntityCommonView : EntityCommonViewBase {
        [Inject("WorldViewService")]
        public WorldViewService WorldViewService;

        public SpawnPool ParentSpawnPool { get; set; }

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
        }

        public override void OnDestroyExecuted(OnDestroyCommand command)
        {
            //Debug.Log("EntityCommonView:OnDestroyExecuted");
            ParentSpawnPool.DespawnEntityCommonView(this);
            ViewModelObject = null;
            //if (gameObject.activeSelf)
            //{
            //    ParentSpawnPool.Despawn(this.transform);
            //}
            //if (gameObject.activeSelf)
            //{
            //    Debug.Log(name + " SetActive(false)");
            //    gameObject.SetActive(false);
            //}
        }

        public override void OnLeaveWorldExecuted(OnLeaveWorldCommand command)
        {
            //Debug.Log("EntityCommonView:OnLeaveWorldExecuted");
            //ParentSpawnPool.DespawnEntityCommonView(this);
            //if (gameObject.activeSelf)
            //{
            //    ParentSpawnPool.Despawn(this.transform);
            //}
            //if (gameObject.activeSelf)
            //{
            //    Debug.Log(name + " SetActive(false)");
            //    gameObject.SetActive(false);
            //}
        }
    }
}
