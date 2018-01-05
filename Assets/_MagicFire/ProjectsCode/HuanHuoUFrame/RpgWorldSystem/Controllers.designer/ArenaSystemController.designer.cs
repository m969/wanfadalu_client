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
    
    
    public class ArenaSystemControllerBase : CampEntityController {
        
        private uFrame.MVVM.ViewModels.IViewModelManager<ArenaSystemViewModel> _ArenaSystemViewModelManager;
        
        [uFrame.IOC.InjectAttribute("ArenaSystem")]
        public uFrame.MVVM.ViewModels.IViewModelManager<ArenaSystemViewModel> ArenaSystemViewModelManager {
            get {
                return _ArenaSystemViewModelManager;
            }
            set {
                _ArenaSystemViewModelManager = value;
            }
        }
        
        public IEnumerable<ArenaSystemViewModel> ArenaSystemViewModels {
            get {
                return ArenaSystemViewModelManager.ViewModels;
            }
        }
        
        public override void Setup() {
            base.Setup();
            // This is called when the controller is created
        }
        
        public override void Initialize(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.Initialize(viewModel);
            // This is called when a viewmodel is created
            this.InitializeArenaSystem(((ArenaSystemViewModel)(viewModel)));
        }
        
        public virtual ArenaSystemViewModel CreateArenaSystem() {
            return ((ArenaSystemViewModel)(this.Create(Guid.NewGuid().ToString())));
        }
        
        public override uFrame.MVVM.ViewModels.ViewModel CreateEmpty() {
            return new ArenaSystemViewModel(this.EventAggregator);
        }
        
        public virtual void InitializeArenaSystem(ArenaSystemViewModel viewModel) {
            // This is called when a ArenaSystemViewModel is created
            viewModel.RequestEnterArena.Action = this.RequestEnterArenaHandler;
            viewModel.RequestExitArena.Action = this.RequestExitArenaHandler;
            ArenaSystemViewModelManager.Add(viewModel);
        }
        
        public override void DisposingViewModel(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.DisposingViewModel(viewModel);
            ArenaSystemViewModelManager.Remove(viewModel);
        }
        
        public virtual void RequestEnterArenaHandler(RequestEnterArenaCommand command) {
            this.RequestEnterArena(command.Sender as ArenaSystemViewModel, command);
        }
        
        public virtual void RequestExitArenaHandler(RequestExitArenaCommand command) {
            this.RequestExitArena(command.Sender as ArenaSystemViewModel, command);
        }
        
        public virtual void RequestEnterArena(ArenaSystemViewModel viewModel, RequestEnterArenaCommand arg) {
        }
        
        public virtual void RequestExitArena(ArenaSystemViewModel viewModel, RequestExitArenaCommand arg) {
        }
    }
}
