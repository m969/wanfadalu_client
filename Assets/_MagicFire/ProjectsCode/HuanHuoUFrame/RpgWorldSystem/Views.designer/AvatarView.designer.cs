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
    
    
    public class AvatarViewBase : ArenaSystemView {
        
        [UnityEngine.SerializeField()]
        [uFrame.MVVM.Attributes.UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Int32 _sectID;
        
        [uFrame.MVVM.Attributes.UFToggleGroup("OnDialogItemsReturn")]
        [UnityEngine.HideInInspector()]
        public bool _BindOnDialogItemsReturn = true;
        
        [uFrame.MVVM.Attributes.UFToggleGroup("avatarState")]
        [UnityEngine.HideInInspector()]
        public bool _BindavatarState = true;
        
        [uFrame.MVVM.Attributes.UFGroup("avatarState")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_avatarStateonlyWhenChanged")]
        protected bool _avatarStateOnlyWhenChanged;
        
        [uFrame.MVVM.Attributes.UFToggleGroup("Teleport")]
        [UnityEngine.HideInInspector()]
        public bool _BindTeleport = true;
        
        [uFrame.MVVM.Attributes.UFToggleGroup("OnJoinSectResult")]
        [UnityEngine.HideInInspector()]
        public bool _BindOnJoinSectResult = true;
        
        [uFrame.MVVM.Attributes.UFToggleGroup("sectID")]
        [UnityEngine.HideInInspector()]
        public bool _BindsectID = true;
        
        [uFrame.MVVM.Attributes.UFGroup("sectID")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_sectIDonlyWhenChanged")]
        protected bool _sectIDOnlyWhenChanged;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(AvatarViewModel);
            }
        }
        
        public AvatarViewModel Avatar {
            get {
                return (AvatarViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as AvatarViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var avatarview = ((AvatarViewModel)model);
            avatarview.sectID = this._sectID;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Avatar to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindOnDialogItemsReturn) {
                this.BindCommandExecuted(this.Avatar.OnDialogItemsReturn, this.OnDialogItemsReturnExecuted);
            }
            if (_BindavatarState) {
                this.BindStateProperty(this.Avatar.avatarStateProperty, this.avatarStateChanged, _avatarStateOnlyWhenChanged);
            }
            if (_BindTeleport) {
                this.BindCommandExecuted(this.Avatar.Teleport, this.TeleportExecuted);
            }
            if (_BindOnJoinSectResult) {
                this.BindCommandExecuted(this.Avatar.OnJoinSectResult, this.OnJoinSectResultExecuted);
            }
            if (_BindsectID) {
                this.BindProperty(this.Avatar.sectIDProperty, this.sectIDChanged, _sectIDOnlyWhenChanged);
            }
        }
        
        public virtual void OnDialogItemsReturnExecuted(OnDialogItemsReturnCommand command) {
        }
        
        public virtual void avatarStateChanged(uFrame.MVVM.StateMachines.State arg1) {
            if (arg1 is CastSkillState) {
                this.OnCastSkillState();
            }
            if (arg1 is IdleState) {
                this.OnIdleState();
            }
            if (arg1 is DeadState) {
                this.OnDeadState();
            }
            if (arg1 is HitState) {
                this.OnHitState();
            }
            if (arg1 is RunState) {
                this.OnRunState();
            }
            if (arg1 is WalkState) {
                this.OnWalkState();
            }
        }
        
        public virtual void OnCastSkillState() {
        }
        
        public virtual void OnIdleState() {
        }
        
        public virtual void OnDeadState() {
        }
        
        public virtual void OnHitState() {
        }
        
        public virtual void OnRunState() {
        }
        
        public virtual void OnWalkState() {
        }
        
        public virtual void TeleportExecuted(TeleportCommand command) {
        }
        
        public virtual void OnJoinSectResultExecuted(OnJoinSectResultCommand command) {
        }
        
        public virtual void sectIDChanged(Int32 arg1) {
        }
        
        public virtual void ExecuteOnDialogItemsReturn(OnDialogItemsReturnCommand command) {
            command.Sender = Avatar;
            Avatar.OnDialogItemsReturn.OnNext(command);
        }
        
        public virtual void ExecuteSelectDialogItem(SelectDialogItemCommand command) {
            command.Sender = Avatar;
            Avatar.SelectDialogItem.OnNext(command);
        }
        
        public virtual void ExecuteTeleport(TeleportCommand command) {
            command.Sender = Avatar;
            Avatar.Teleport.OnNext(command);
        }
        
        public virtual void ExecuteRequestDialog(RequestDialogCommand command) {
            command.Sender = Avatar;
            Avatar.RequestDialog.OnNext(command);
        }
        
        public virtual void ExecuteonMainAvatarEnterSpace(onMainAvatarEnterSpaceCommand command) {
            command.Sender = Avatar;
            Avatar.onMainAvatarEnterSpace.OnNext(command);
        }
        
        public virtual void ExecuteonMainAvatarLeaveSpace(onMainAvatarLeaveSpaceCommand command) {
            command.Sender = Avatar;
            Avatar.onMainAvatarLeaveSpace.OnNext(command);
        }
        
        public virtual void ExecuteOnJoinSectResult(OnJoinSectResultCommand command) {
            command.Sender = Avatar;
            Avatar.OnJoinSectResult.OnNext(command);
        }
    }
}
