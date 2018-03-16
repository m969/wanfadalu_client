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
    
    
    public class AvatarControllerBase : PropSystemController {
        
        private uFrame.MVVM.ViewModels.IViewModelManager<AvatarViewModel> _AvatarViewModelManager;
        
        [uFrame.IOC.InjectAttribute("Avatar")]
        public uFrame.MVVM.ViewModels.IViewModelManager<AvatarViewModel> AvatarViewModelManager {
            get {
                return _AvatarViewModelManager;
            }
            set {
                _AvatarViewModelManager = value;
            }
        }
        
        public IEnumerable<AvatarViewModel> AvatarViewModels {
            get {
                return AvatarViewModelManager.ViewModels;
            }
        }
        
        public override void Setup() {
            base.Setup();
            // This is called when the controller is created
        }
        
        public override void Initialize(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.Initialize(viewModel);
            // This is called when a viewmodel is created
            this.InitializeAvatar(((AvatarViewModel)(viewModel)));
        }
        
        public virtual AvatarViewModel CreateAvatar() {
            return ((AvatarViewModel)(this.Create(Guid.NewGuid().ToString())));
        }
        
        public override uFrame.MVVM.ViewModels.ViewModel CreateEmpty() {
            return new AvatarViewModel(this.EventAggregator);
        }
        
        public virtual void InitializeAvatar(AvatarViewModel viewModel) {
            // This is called when a AvatarViewModel is created
            viewModel.OnDialogItemsReturn.Action = this.OnDialogItemsReturnHandler;
            viewModel.OnError.Action = this.OnErrorHandler;
            viewModel.SelectDialogItem.Action = this.SelectDialogItemHandler;
            viewModel.OnTargetItemListReturn.Action = this.OnTargetItemListReturnHandler;
            viewModel.Teleport.Action = this.TeleportHandler;
            viewModel.RequestDialog.Action = this.RequestDialogHandler;
            viewModel.onMainAvatarEnterSpace.Action = this.onMainAvatarEnterSpaceHandler;
            viewModel.onMainAvatarLeaveSpace.Action = this.onMainAvatarLeaveSpaceHandler;
            viewModel.OnJoinSectResult.Action = this.OnJoinSectResultHandler;
            viewModel.OnRequestForgeResult.Action = this.OnRequestForgeResultHandler;
            AvatarViewModelManager.Add(viewModel);
        }
        
        public override void DisposingViewModel(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.DisposingViewModel(viewModel);
            AvatarViewModelManager.Remove(viewModel);
        }
        
        public virtual void OnDialogItemsReturnHandler(OnDialogItemsReturnCommand command) {
            this.OnDialogItemsReturn(command.Sender as AvatarViewModel, command);
        }
        
        public virtual void OnErrorHandler(OnErrorCommand command) {
            this.OnError(command.Sender as AvatarViewModel, command);
        }
        
        public virtual void SelectDialogItemHandler(SelectDialogItemCommand command) {
            this.SelectDialogItem(command.Sender as AvatarViewModel, command);
        }
        
        public virtual void OnTargetItemListReturnHandler(OnTargetItemListReturnCommand command) {
            this.OnTargetItemListReturn(command.Sender as AvatarViewModel, command);
        }
        
        public virtual void TeleportHandler(TeleportCommand command) {
            this.Teleport(command.Sender as AvatarViewModel, command);
        }
        
        public virtual void RequestDialogHandler(RequestDialogCommand command) {
            this.RequestDialog(command.Sender as AvatarViewModel, command);
        }
        
        public virtual void onMainAvatarEnterSpaceHandler(onMainAvatarEnterSpaceCommand command) {
            this.onMainAvatarEnterSpace(command.Sender as AvatarViewModel, command);
        }
        
        public virtual void onMainAvatarLeaveSpaceHandler(onMainAvatarLeaveSpaceCommand command) {
            this.onMainAvatarLeaveSpace(command.Sender as AvatarViewModel, command);
        }
        
        public virtual void OnJoinSectResultHandler(OnJoinSectResultCommand command) {
            this.OnJoinSectResult(command.Sender as AvatarViewModel, command);
        }
        
        public virtual void OnRequestForgeResultHandler(OnRequestForgeResultCommand command) {
            this.OnRequestForgeResult(command.Sender as AvatarViewModel, command);
        }
        
        public virtual void OnDialogItemsReturn(AvatarViewModel viewModel, OnDialogItemsReturnCommand arg) {
        }
        
        public virtual void OnError(AvatarViewModel viewModel, OnErrorCommand arg) {
        }
        
        public virtual void SelectDialogItem(AvatarViewModel viewModel, SelectDialogItemCommand arg) {
        }
        
        public virtual void OnTargetItemListReturn(AvatarViewModel viewModel, OnTargetItemListReturnCommand arg) {
        }
        
        public virtual void Teleport(AvatarViewModel viewModel, TeleportCommand arg) {
        }
        
        public virtual void RequestDialog(AvatarViewModel viewModel, RequestDialogCommand arg) {
        }
        
        public virtual void onMainAvatarEnterSpace(AvatarViewModel viewModel, onMainAvatarEnterSpaceCommand arg) {
        }
        
        public virtual void onMainAvatarLeaveSpace(AvatarViewModel viewModel, onMainAvatarLeaveSpaceCommand arg) {
        }
        
        public virtual void OnJoinSectResult(AvatarViewModel viewModel, OnJoinSectResultCommand arg) {
        }
        
        public virtual void OnRequestForgeResult(AvatarViewModel viewModel, OnRequestForgeResultCommand arg) {
        }
    }
}
