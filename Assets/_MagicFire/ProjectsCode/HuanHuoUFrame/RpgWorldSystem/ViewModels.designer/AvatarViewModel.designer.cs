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
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    
    
    public partial class AvatarViewModelBase : PropSystemViewModel {
        
        private AvatarStateMachine _avatarStateProperty;
        
        private Signal<OnDialogItemsReturnCommand> _OnDialogItemsReturn;
        
        private Signal<SelectDialogItemCommand> _SelectDialogItem;
        
        private Signal<TeleportCommand> _Teleport;
        
        private Signal<RequestDialogCommand> _RequestDialog;
        
        private Signal<onMainAvatarEnterSpaceCommand> _onMainAvatarEnterSpace;
        
        private Signal<onMainAvatarLeaveSpaceCommand> _onMainAvatarLeaveSpace;
        
        public AvatarViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
        
        public virtual AvatarStateMachine avatarStateProperty {
            get {
                return _avatarStateProperty;
            }
            set {
                _avatarStateProperty = value;
            }
        }
        
        public virtual uFrame.MVVM.StateMachines.State avatarState {
            get {
                return avatarStateProperty.Value;
            }
            set {
                avatarStateProperty.Value = value;
            }
        }
        
        public virtual Signal<OnDialogItemsReturnCommand> OnDialogItemsReturn {
            get {
                return _OnDialogItemsReturn;
            }
            set {
                _OnDialogItemsReturn = value;
            }
        }
        
        public virtual Signal<SelectDialogItemCommand> SelectDialogItem {
            get {
                return _SelectDialogItem;
            }
            set {
                _SelectDialogItem = value;
            }
        }
        
        public virtual Signal<TeleportCommand> Teleport {
            get {
                return _Teleport;
            }
            set {
                _Teleport = value;
            }
        }
        
        public virtual Signal<RequestDialogCommand> RequestDialog {
            get {
                return _RequestDialog;
            }
            set {
                _RequestDialog = value;
            }
        }
        
        public virtual Signal<onMainAvatarEnterSpaceCommand> onMainAvatarEnterSpace {
            get {
                return _onMainAvatarEnterSpace;
            }
            set {
                _onMainAvatarEnterSpace = value;
            }
        }
        
        public virtual Signal<onMainAvatarLeaveSpaceCommand> onMainAvatarLeaveSpace {
            get {
                return _onMainAvatarLeaveSpace;
            }
            set {
                _onMainAvatarLeaveSpace = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            this.OnDialogItemsReturn = new Signal<OnDialogItemsReturnCommand>(this);
            this.SelectDialogItem = new Signal<SelectDialogItemCommand>(this);
            this.Teleport = new Signal<TeleportCommand>(this);
            this.RequestDialog = new Signal<RequestDialogCommand>(this);
            this.onMainAvatarEnterSpace = new Signal<onMainAvatarEnterSpaceCommand>(this);
            this.onMainAvatarLeaveSpace = new Signal<onMainAvatarLeaveSpaceCommand>(this);
            _avatarStateProperty = new AvatarStateMachine(this, "avatarState");
        }
        
        public virtual void Execute(OnDialogItemsReturnCommand argument) {
            this.OnDialogItemsReturn.OnNext(argument);
        }
        
        public virtual void Execute(SelectDialogItemCommand argument) {
            this.SelectDialogItem.OnNext(argument);
        }
        
        public virtual void Execute(TeleportCommand argument) {
            this.Teleport.OnNext(argument);
        }
        
        public virtual void Execute(RequestDialogCommand argument) {
            this.RequestDialog.OnNext(argument);
        }
        
        public virtual void Execute(onMainAvatarEnterSpaceCommand argument) {
            this.onMainAvatarEnterSpace.OnNext(argument);
        }
        
        public virtual void Execute(onMainAvatarLeaveSpaceCommand argument) {
            this.onMainAvatarLeaveSpace.OnNext(argument);
        }
        
        public virtual void OnDialogItemsReturn_(object DialogItemsObject) {
            var cmd = new OnDialogItemsReturnCommand();
            cmd.DialogItemsObject = DialogItemsObject;
            this.OnDialogItemsReturn.OnNext(cmd);
        }
        
        public virtual void SelectDialogItem_(Int32 DialogID) {
            var cmd = new SelectDialogItemCommand();
            cmd.DialogID = DialogID;
            this.SelectDialogItem.OnNext(cmd);
        }
        
        public virtual void Teleport_(Vector3 Position) {
            var cmd = new TeleportCommand();
            cmd.Position = Position;
            this.Teleport.OnNext(cmd);
        }
        
        public virtual void RequestDialog_(Int32 NpcID) {
            var cmd = new RequestDialogCommand();
            cmd.NpcID = NpcID;
            this.RequestDialog.OnNext(cmd);
        }
        
        public virtual void onMainAvatarEnterSpace_(Int32 SpaceId, String SpaceName) {
            var cmd = new onMainAvatarEnterSpaceCommand();
            cmd.SpaceId = SpaceId;
            cmd.SpaceName = SpaceName;
            this.onMainAvatarEnterSpace.OnNext(cmd);
        }
        
        public virtual void onMainAvatarLeaveSpace_() {
            var cmd = new onMainAvatarLeaveSpaceCommand();
            this.onMainAvatarLeaveSpace.OnNext(cmd);
        }
        
        public override void Read(uFrame.Kernel.Serialization.ISerializerStream stream) {
            base.Read(stream);
            this._avatarStateProperty.SetState(stream.DeserializeString("avatarState"));
        }
        
        public override void Write(uFrame.Kernel.Serialization.ISerializerStream stream) {
            base.Write(stream);
            stream.SerializeString("avatarState", this.avatarState.Name);;
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModels.ViewModelCommandInfo> list) {
            base.FillCommands(list);
            list.Add(new ViewModelCommandInfo("OnDialogItemsReturn", OnDialogItemsReturn) { ParameterType = typeof(OnDialogItemsReturnCommand) });
            list.Add(new ViewModelCommandInfo("SelectDialogItem", SelectDialogItem) { ParameterType = typeof(SelectDialogItemCommand) });
            list.Add(new ViewModelCommandInfo("Teleport", Teleport) { ParameterType = typeof(TeleportCommand) });
            list.Add(new ViewModelCommandInfo("RequestDialog", RequestDialog) { ParameterType = typeof(RequestDialogCommand) });
            list.Add(new ViewModelCommandInfo("onMainAvatarEnterSpace", onMainAvatarEnterSpace) { ParameterType = typeof(onMainAvatarEnterSpaceCommand) });
            list.Add(new ViewModelCommandInfo("onMainAvatarLeaveSpace", onMainAvatarLeaveSpace) { ParameterType = typeof(onMainAvatarLeaveSpaceCommand) });
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModels.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
            // PropertiesChildItem
            list.Add(new ViewModelPropertyInfo(_avatarStateProperty, false, false, false, false));
        }
    }
    
    public partial class AvatarViewModel {
        
        public AvatarViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
}
