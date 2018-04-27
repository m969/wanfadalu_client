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
        
        private P<Int32> _lingshiAmountProperty;
        
        private P<Int32> _sectIDProperty;
        
        private P<String> _skillKeyOptionsProperty;
        
        private Signal<RequestTargetItemListCommand> _RequestTargetItemList;
        
        private Signal<OnDialogItemsReturnCommand> _OnDialogItemsReturn;
        
        private Signal<OnErrorCommand> _OnError;
        
        private Signal<SelectDialogItemCommand> _SelectDialogItem;
        
        private Signal<RequestForgeCommand> _RequestForge;
        
        private Signal<OnTargetItemListReturnCommand> _OnTargetItemListReturn;
        
        private Signal<TeleportCommand> _Teleport;
        
        private Signal<RequestDialogCommand> _RequestDialog;
        
        private Signal<onMainAvatarEnterSpaceCommand> _onMainAvatarEnterSpace;
        
        private Signal<onMainAvatarLeaveSpaceCommand> _onMainAvatarLeaveSpace;
        
        private Signal<OnJoinSectResultCommand> _OnJoinSectResult;
        
        private Signal<OnRequestForgeResultCommand> _OnRequestForgeResult;
        
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
        
        public virtual P<Int32> lingshiAmountProperty {
            get {
                return _lingshiAmountProperty;
            }
            set {
                _lingshiAmountProperty = value;
            }
        }
        
        public virtual P<Int32> sectIDProperty {
            get {
                return _sectIDProperty;
            }
            set {
                _sectIDProperty = value;
            }
        }
        
        public virtual P<String> skillKeyOptionsProperty {
            get {
                return _skillKeyOptionsProperty;
            }
            set {
                _skillKeyOptionsProperty = value;
            }
        }
        
        public virtual Int32 lingshiAmount {
            get {
                return lingshiAmountProperty.Value;
            }
            set {
                lingshiAmountProperty.Value = value;
            }
        }
        
        public virtual Int32 sectID {
            get {
                return sectIDProperty.Value;
            }
            set {
                sectIDProperty.Value = value;
            }
        }
        
        public virtual String skillKeyOptions {
            get {
                return skillKeyOptionsProperty.Value;
            }
            set {
                skillKeyOptionsProperty.Value = value;
            }
        }
        
        public virtual Signal<RequestTargetItemListCommand> RequestTargetItemList {
            get {
                return _RequestTargetItemList;
            }
            set {
                _RequestTargetItemList = value;
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
        
        public virtual Signal<OnErrorCommand> OnError {
            get {
                return _OnError;
            }
            set {
                _OnError = value;
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
        
        public virtual Signal<RequestForgeCommand> RequestForge {
            get {
                return _RequestForge;
            }
            set {
                _RequestForge = value;
            }
        }
        
        public virtual Signal<OnTargetItemListReturnCommand> OnTargetItemListReturn {
            get {
                return _OnTargetItemListReturn;
            }
            set {
                _OnTargetItemListReturn = value;
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
        
        public virtual Signal<OnJoinSectResultCommand> OnJoinSectResult {
            get {
                return _OnJoinSectResult;
            }
            set {
                _OnJoinSectResult = value;
            }
        }
        
        public virtual Signal<OnRequestForgeResultCommand> OnRequestForgeResult {
            get {
                return _OnRequestForgeResult;
            }
            set {
                _OnRequestForgeResult = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            this.RequestTargetItemList = new Signal<RequestTargetItemListCommand>(this);
            this.OnDialogItemsReturn = new Signal<OnDialogItemsReturnCommand>(this);
            this.OnError = new Signal<OnErrorCommand>(this);
            this.SelectDialogItem = new Signal<SelectDialogItemCommand>(this);
            this.RequestForge = new Signal<RequestForgeCommand>(this);
            this.OnTargetItemListReturn = new Signal<OnTargetItemListReturnCommand>(this);
            this.Teleport = new Signal<TeleportCommand>(this);
            this.RequestDialog = new Signal<RequestDialogCommand>(this);
            this.onMainAvatarEnterSpace = new Signal<onMainAvatarEnterSpaceCommand>(this);
            this.onMainAvatarLeaveSpace = new Signal<onMainAvatarLeaveSpaceCommand>(this);
            this.OnJoinSectResult = new Signal<OnJoinSectResultCommand>(this);
            this.OnRequestForgeResult = new Signal<OnRequestForgeResultCommand>(this);
            _lingshiAmountProperty = new P<Int32>(this, "lingshiAmount");
            _sectIDProperty = new P<Int32>(this, "sectID");
            _skillKeyOptionsProperty = new P<String>(this, "skillKeyOptions");
            _avatarStateProperty = new AvatarStateMachine(this, "avatarState");
        }
        
        public virtual void Execute(RequestTargetItemListCommand argument) {
            this.RequestTargetItemList.OnNext(argument);
        }
        
        public virtual void Execute(OnDialogItemsReturnCommand argument) {
            this.OnDialogItemsReturn.OnNext(argument);
        }
        
        public virtual void Execute(OnErrorCommand argument) {
            this.OnError.OnNext(argument);
        }
        
        public virtual void Execute(SelectDialogItemCommand argument) {
            this.SelectDialogItem.OnNext(argument);
        }
        
        public virtual void Execute(RequestForgeCommand argument) {
            this.RequestForge.OnNext(argument);
        }
        
        public virtual void Execute(OnTargetItemListReturnCommand argument) {
            this.OnTargetItemListReturn.OnNext(argument);
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
        
        public virtual void Execute(OnJoinSectResultCommand argument) {
            this.OnJoinSectResult.OnNext(argument);
        }
        
        public virtual void Execute(OnRequestForgeResultCommand argument) {
            this.OnRequestForgeResult.OnNext(argument);
        }
        
        public virtual void RequestTargetItemList_() {
            var cmd = new RequestTargetItemListCommand();
            this.RequestTargetItemList.OnNext(cmd);
        }
        
        public virtual void OnDialogItemsReturn_(object DialogItemsObject) {
            var cmd = new OnDialogItemsReturnCommand();
            cmd.DialogItemsObject = DialogItemsObject;
            this.OnDialogItemsReturn.OnNext(cmd);
        }
        
        public virtual void OnError_(Int32 ErrorCode) {
            var cmd = new OnErrorCommand();
            cmd.ErrorCode = ErrorCode;
            this.OnError.OnNext(cmd);
        }
        
        public virtual void SelectDialogItem_(Int32 DialogID) {
            var cmd = new SelectDialogItemCommand();
            cmd.DialogID = DialogID;
            this.SelectDialogItem.OnNext(cmd);
        }
        
        public virtual void RequestForge_() {
            var cmd = new RequestForgeCommand();
            this.RequestForge.OnNext(cmd);
        }
        
        public virtual void OnTargetItemListReturn_(String TargetItemListJson) {
            var cmd = new OnTargetItemListReturnCommand();
            cmd.TargetItemListJson = TargetItemListJson;
            this.OnTargetItemListReturn.OnNext(cmd);
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
        
        public virtual void OnJoinSectResult_(Int32 SectID, Int32 Result) {
            var cmd = new OnJoinSectResultCommand();
            cmd.SectID = SectID;
            cmd.Result = Result;
            this.OnJoinSectResult.OnNext(cmd);
        }
        
        public virtual void OnRequestForgeResult_(Int32 Result) {
            var cmd = new OnRequestForgeResultCommand();
            cmd.Result = Result;
            this.OnRequestForgeResult.OnNext(cmd);
        }
        
        public override void Read(uFrame.Kernel.Serialization.ISerializerStream stream) {
            base.Read(stream);
            this.lingshiAmount = stream.DeserializeInt("lingshiAmount");;
            this.sectID = stream.DeserializeInt("sectID");;
            this._avatarStateProperty.SetState(stream.DeserializeString("avatarState"));
            this.skillKeyOptions = stream.DeserializeString("skillKeyOptions");;
        }
        
        public override void Write(uFrame.Kernel.Serialization.ISerializerStream stream) {
            base.Write(stream);
            stream.SerializeInt("lingshiAmount", this.lingshiAmount);
            stream.SerializeInt("sectID", this.sectID);
            stream.SerializeString("avatarState", this.avatarState.Name);;
            stream.SerializeString("skillKeyOptions", this.skillKeyOptions);
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModels.ViewModelCommandInfo> list) {
            base.FillCommands(list);
            list.Add(new ViewModelCommandInfo("RequestTargetItemList", RequestTargetItemList) { ParameterType = typeof(RequestTargetItemListCommand) });
            list.Add(new ViewModelCommandInfo("OnDialogItemsReturn", OnDialogItemsReturn) { ParameterType = typeof(OnDialogItemsReturnCommand) });
            list.Add(new ViewModelCommandInfo("OnError", OnError) { ParameterType = typeof(OnErrorCommand) });
            list.Add(new ViewModelCommandInfo("SelectDialogItem", SelectDialogItem) { ParameterType = typeof(SelectDialogItemCommand) });
            list.Add(new ViewModelCommandInfo("RequestForge", RequestForge) { ParameterType = typeof(RequestForgeCommand) });
            list.Add(new ViewModelCommandInfo("OnTargetItemListReturn", OnTargetItemListReturn) { ParameterType = typeof(OnTargetItemListReturnCommand) });
            list.Add(new ViewModelCommandInfo("Teleport", Teleport) { ParameterType = typeof(TeleportCommand) });
            list.Add(new ViewModelCommandInfo("RequestDialog", RequestDialog) { ParameterType = typeof(RequestDialogCommand) });
            list.Add(new ViewModelCommandInfo("onMainAvatarEnterSpace", onMainAvatarEnterSpace) { ParameterType = typeof(onMainAvatarEnterSpaceCommand) });
            list.Add(new ViewModelCommandInfo("onMainAvatarLeaveSpace", onMainAvatarLeaveSpace) { ParameterType = typeof(onMainAvatarLeaveSpaceCommand) });
            list.Add(new ViewModelCommandInfo("OnJoinSectResult", OnJoinSectResult) { ParameterType = typeof(OnJoinSectResultCommand) });
            list.Add(new ViewModelCommandInfo("OnRequestForgeResult", OnRequestForgeResult) { ParameterType = typeof(OnRequestForgeResultCommand) });
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModels.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
            // PropertiesChildItem
            list.Add(new ViewModelPropertyInfo(_lingshiAmountProperty, false, false, false, false));
            // PropertiesChildItem
            list.Add(new ViewModelPropertyInfo(_sectIDProperty, false, false, false, false));
            // PropertiesChildItem
            list.Add(new ViewModelPropertyInfo(_avatarStateProperty, false, false, false, false));
            // PropertiesChildItem
            list.Add(new ViewModelPropertyInfo(_skillKeyOptionsProperty, false, false, false, false));
        }
    }
    
    public partial class AvatarViewModel {
        
        public AvatarViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
}
