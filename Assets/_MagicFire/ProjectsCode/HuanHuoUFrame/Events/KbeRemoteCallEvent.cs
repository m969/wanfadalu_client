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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.ECS;
    using UniRx;
    
    
    [uFrame.Attributes.EventId(2)]
    public partial class KbeRemoteCallEvent : object {
        
        [UnityEngine.SerializeField()]
        private String _RemoteCallName;
        
        [UnityEngine.SerializeField()]
        private object[] _Params;
        
        [UnityEngine.SerializeField()]
        private CallType _CallType;
        
        public String RemoteCallName {
            get {
                return _RemoteCallName;
            }
            set {
                _RemoteCallName = value;
            }
        }
        
        public object[] Params {
            get {
                return _Params;
            }
            set {
                _Params = value;
            }
        }
        
        public CallType CallType {
            get {
                return _CallType;
            }
            set {
                _CallType = value;
            }
        }
    }
}
