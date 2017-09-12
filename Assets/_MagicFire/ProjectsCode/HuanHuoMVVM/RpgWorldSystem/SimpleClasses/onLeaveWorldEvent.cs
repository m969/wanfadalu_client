namespace MagicFire.HuanHuoMVVM {
    using MagicFire.HuanHuoMVVM;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Json;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    
    
    public partial class onLeaveWorldEvent : onLeaveWorldEventBase {
        public onLeaveWorldEvent(KBEngine.Entity entity)
        {
            Entity = entity;
        }
    }
}
