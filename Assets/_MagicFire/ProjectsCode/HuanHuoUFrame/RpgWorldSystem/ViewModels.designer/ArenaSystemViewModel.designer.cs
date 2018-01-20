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
    
    
    public partial class ArenaSystemViewModelBase : CampEntityViewModel {
        
        private Signal<RequestEnterArenaCommand> _RequestEnterArena;
        
        private Signal<OnExitArenaCommand> _OnExitArena;
        
        private Signal<RequestExitArenaCommand> _RequestExitArena;
        
        private Signal<OnEnterArenaCommand> _OnEnterArena;
        
        private Signal<OnMatchEndCommand> _OnMatchEnd;
        
        public ArenaSystemViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
        
        public virtual Signal<RequestEnterArenaCommand> RequestEnterArena {
            get {
                return _RequestEnterArena;
            }
            set {
                _RequestEnterArena = value;
            }
        }
        
        public virtual Signal<OnExitArenaCommand> OnExitArena {
            get {
                return _OnExitArena;
            }
            set {
                _OnExitArena = value;
            }
        }
        
        public virtual Signal<RequestExitArenaCommand> RequestExitArena {
            get {
                return _RequestExitArena;
            }
            set {
                _RequestExitArena = value;
            }
        }
        
        public virtual Signal<OnEnterArenaCommand> OnEnterArena {
            get {
                return _OnEnterArena;
            }
            set {
                _OnEnterArena = value;
            }
        }
        
        public virtual Signal<OnMatchEndCommand> OnMatchEnd {
            get {
                return _OnMatchEnd;
            }
            set {
                _OnMatchEnd = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            this.RequestEnterArena = new Signal<RequestEnterArenaCommand>(this);
            this.OnExitArena = new Signal<OnExitArenaCommand>(this);
            this.RequestExitArena = new Signal<RequestExitArenaCommand>(this);
            this.OnEnterArena = new Signal<OnEnterArenaCommand>(this);
            this.OnMatchEnd = new Signal<OnMatchEndCommand>(this);
        }
        
        public virtual void Execute(RequestEnterArenaCommand argument) {
            this.RequestEnterArena.OnNext(argument);
        }
        
        public virtual void Execute(OnExitArenaCommand argument) {
            this.OnExitArena.OnNext(argument);
        }
        
        public virtual void Execute(RequestExitArenaCommand argument) {
            this.RequestExitArena.OnNext(argument);
        }
        
        public virtual void Execute(OnEnterArenaCommand argument) {
            this.OnEnterArena.OnNext(argument);
        }
        
        public virtual void Execute(OnMatchEndCommand argument) {
            this.OnMatchEnd.OnNext(argument);
        }
        
        public virtual void RequestEnterArena_(Int32 ArenaID) {
            var cmd = new RequestEnterArenaCommand();
            cmd.ArenaID = ArenaID;
            this.RequestEnterArena.OnNext(cmd);
        }
        
        public virtual void OnExitArena_(Vector3 OutPosition) {
            var cmd = new OnExitArenaCommand();
            cmd.OutPosition = OutPosition;
            this.OnExitArena.OnNext(cmd);
        }
        
        public virtual void RequestExitArena_() {
            var cmd = new RequestExitArenaCommand();
            this.RequestExitArena.OnNext(cmd);
        }
        
        public virtual void OnEnterArena_(Vector3 CenterPosition) {
            var cmd = new OnEnterArenaCommand();
            cmd.CenterPosition = CenterPosition;
            this.OnEnterArena.OnNext(cmd);
        }
        
        public virtual void OnMatchEnd_(Boolean IsWin) {
            var cmd = new OnMatchEndCommand();
            cmd.IsWin = IsWin;
            this.OnMatchEnd.OnNext(cmd);
        }
        
        public override void Read(uFrame.Kernel.Serialization.ISerializerStream stream) {
            base.Read(stream);
        }
        
        public override void Write(uFrame.Kernel.Serialization.ISerializerStream stream) {
            base.Write(stream);
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModels.ViewModelCommandInfo> list) {
            base.FillCommands(list);
            list.Add(new ViewModelCommandInfo("RequestEnterArena", RequestEnterArena) { ParameterType = typeof(RequestEnterArenaCommand) });
            list.Add(new ViewModelCommandInfo("OnExitArena", OnExitArena) { ParameterType = typeof(OnExitArenaCommand) });
            list.Add(new ViewModelCommandInfo("RequestExitArena", RequestExitArena) { ParameterType = typeof(RequestExitArenaCommand) });
            list.Add(new ViewModelCommandInfo("OnEnterArena", OnEnterArena) { ParameterType = typeof(OnEnterArenaCommand) });
            list.Add(new ViewModelCommandInfo("OnMatchEnd", OnMatchEnd) { ParameterType = typeof(OnMatchEndCommand) });
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModels.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
        }
    }
    
    public partial class ArenaSystemViewModel {
        
        public ArenaSystemViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
}