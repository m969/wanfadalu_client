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
    
    
    [uFrame.Attributes.EventId(3)]
    public partial class ResponseEvent : object {
        
        [UnityEngine.SerializeField()]
        private RpgInteractiveComponent _RpgInteractiveComponent;
        
        public RpgInteractiveComponent RpgInteractiveComponent {
            get {
                return _RpgInteractiveComponent;
            }
            set {
                _RpgInteractiveComponent = value;
            }
        }
    }
}