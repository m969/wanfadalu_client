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
    
    
    public partial class MotionSystemViewModelBase : SuperPowerEntityViewModel {
        
        private P<Int32> _canMoveProperty;
        
        private Signal<RequestStopMoveCommand> _RequestStopMove;
        
        private Signal<OnStopMoveCommand> _OnStopMove;
        
        private Signal<DoMoveCommand> _DoMove;
        
        private Signal<RequestMoveCommand> _RequestMove;
        
        public MotionSystemViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
        
        public virtual P<Int32> canMoveProperty {
            get {
                return _canMoveProperty;
            }
            set {
                _canMoveProperty = value;
            }
        }
        
        public virtual Int32 canMove {
            get {
                return canMoveProperty.Value;
            }
            set {
                canMoveProperty.Value = value;
            }
        }
        
        public virtual Signal<RequestStopMoveCommand> RequestStopMove {
            get {
                return _RequestStopMove;
            }
            set {
                _RequestStopMove = value;
            }
        }
        
        public virtual Signal<OnStopMoveCommand> OnStopMove {
            get {
                return _OnStopMove;
            }
            set {
                _OnStopMove = value;
            }
        }
        
        public virtual Signal<DoMoveCommand> DoMove {
            get {
                return _DoMove;
            }
            set {
                _DoMove = value;
            }
        }
        
        public virtual Signal<RequestMoveCommand> RequestMove {
            get {
                return _RequestMove;
            }
            set {
                _RequestMove = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            this.RequestStopMove = new Signal<RequestStopMoveCommand>(this);
            this.OnStopMove = new Signal<OnStopMoveCommand>(this);
            this.DoMove = new Signal<DoMoveCommand>(this);
            this.RequestMove = new Signal<RequestMoveCommand>(this);
            _canMoveProperty = new P<Int32>(this, "canMove");
        }
        
        public virtual void Execute(RequestStopMoveCommand argument) {
            this.RequestStopMove.OnNext(argument);
        }
        
        public virtual void Execute(OnStopMoveCommand argument) {
            this.OnStopMove.OnNext(argument);
        }
        
        public virtual void Execute(DoMoveCommand argument) {
            this.DoMove.OnNext(argument);
        }
        
        public virtual void Execute(RequestMoveCommand argument) {
            this.RequestMove.OnNext(argument);
        }
        
        public virtual void RequestStopMove_() {
            var cmd = new RequestStopMoveCommand();
            this.RequestStopMove.OnNext(cmd);
        }
        
        public virtual void OnStopMove_() {
            var cmd = new OnStopMoveCommand();
            this.OnStopMove.OnNext(cmd);
        }
        
        public virtual void DoMove_(Vector3 Point) {
            var cmd = new DoMoveCommand();
            cmd.Point = Point;
            this.DoMove.OnNext(cmd);
        }
        
        public virtual void RequestMove_(Vector3 Point) {
            var cmd = new RequestMoveCommand();
            cmd.Point = Point;
            this.RequestMove.OnNext(cmd);
        }
        
        public override void Read(uFrame.Kernel.Serialization.ISerializerStream stream) {
            base.Read(stream);
            this.canMove = stream.DeserializeInt("canMove");;
        }
        
        public override void Write(uFrame.Kernel.Serialization.ISerializerStream stream) {
            base.Write(stream);
            stream.SerializeInt("canMove", this.canMove);
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModels.ViewModelCommandInfo> list) {
            base.FillCommands(list);
            list.Add(new ViewModelCommandInfo("RequestStopMove", RequestStopMove) { ParameterType = typeof(RequestStopMoveCommand) });
            list.Add(new ViewModelCommandInfo("OnStopMove", OnStopMove) { ParameterType = typeof(OnStopMoveCommand) });
            list.Add(new ViewModelCommandInfo("DoMove", DoMove) { ParameterType = typeof(DoMoveCommand) });
            list.Add(new ViewModelCommandInfo("RequestMove", RequestMove) { ParameterType = typeof(RequestMoveCommand) });
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModels.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
            // PropertiesChildItem
            list.Add(new ViewModelPropertyInfo(_canMoveProperty, false, false, false, false));
        }
    }
    
    public partial class MotionSystemViewModel {
        
        public MotionSystemViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
}
