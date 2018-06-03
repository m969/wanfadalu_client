namespace MagicFire.HuanHuoUFrame {
    using PathologicalGames;
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
    
    
    public class MonsterView : MonsterViewBase {
        private Transform _monsterModel;

        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as MonsterViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Monster to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            this.OnMouseEvent(MouseEventType.OnMouseDown).Subscribe(evt =>
            {
                Debug.Log("MonsterView:OnMouseDown");
            }).DisposeWith(this);
            var viewPool = PoolManager.Pools["MonsterViewPool"];
            _monsterModel = viewPool.Spawn(viewPool.prefabs["monster_" + _typeID]);
            if (_monsterModel != null)
            {
                _monsterModel.SetParent(transform);
                _monsterModel.localPosition = Vector3.zero;
                ModelAnimator = _monsterModel.GetComponentInChildren<Animator>();
            }
        }

        public override void typeIDChanged(int arg1)
        {
            _typeID = arg1;
        }

        public override void StartMoveExecuted(StartMoveCommand command)
        {
            ModelAnimator.Play("idle");
        }

        public override void StopMoveExecuted(StopMoveCommand command)
        {
            ModelAnimator.Play("walk");
        }
    }
}
