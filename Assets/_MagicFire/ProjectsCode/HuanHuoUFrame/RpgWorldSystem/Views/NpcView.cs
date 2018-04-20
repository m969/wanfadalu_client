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
    using PathologicalGames;
    
    
    public class NpcView : NpcViewBase {
        private Transform _npcModel;
        
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
            }).DisposeWith(this);
            var viewPool = PoolManager.Pools["NpcViewPool"];
            if (_npcType == 2)
                _npcModel = viewPool.Spawn(viewPool.prefabs["npc_arena"]);
            else if (_npcType == 3)
                _npcModel = viewPool.Spawn(viewPool.prefabs["npc_sect_" + _sectID]);
            else
                _npcModel = viewPool.Spawn(viewPool.prefabs["npc_" + _npcID]);
            if (_npcModel != null)
            {
                _npcModel.SetParent(transform);
                _npcModel.localPosition = Vector3.zero;
            }
            if (_npcType == 3)
            {
                var mainAvatar = KBEngine.KBEngineApp.app.player() as AvatarViewModel;
                if (_sectID == mainAvatar.sectID)
                    _npcModel.GetComponent<BoxCollider>().enabled = false;
                else
                {
                    mainAvatar.sectIDProperty.Subscribe(x =>
                    {
                        if (x == _sectID)
                        {
                            _npcModel.GetComponent<BoxCollider>().enabled = false;
                        }
                    });
                }
            }
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

        public override void sectIDChanged(int arg1)
        {
            _sectID = arg1;
        }

        public override void OnDestroyExecuted(OnDestroyCommand command)
        {
            Debug.Log("NpcView:OnDestroyExecuted");
            if (_npcModel.gameObject.activeSelf)
            {
                _npcModel.SetParent(ParentSpawnPool.transform);
                ParentSpawnPool.Despawn(_npcModel);
            }
            Npc.renderObj = null;
            //base.OnDestroyExecuted(command);
            DestroyImmediate(gameObject);
        }

        public override void OnLeaveWorldExecuted(OnLeaveWorldCommand command)
        {
            Debug.Log("NpcView:OnLeaveWorldExecuted");
            if (_npcModel.gameObject.activeSelf)
            {
                _npcModel.SetParent(ParentSpawnPool.transform);
                ParentSpawnPool.Despawn(_npcModel);
            }
            Npc.renderObj = null;
            //base.OnLeaveWorldExecuted(command);
            DestroyImmediate(gameObject);
        }
    }
}
