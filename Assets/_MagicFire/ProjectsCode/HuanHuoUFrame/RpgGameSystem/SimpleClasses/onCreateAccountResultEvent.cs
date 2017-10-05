namespace MagicFire.HuanHuoUFrame {
    using MagicFire.HuanHuoUFrame;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Json;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    
    
    public partial class onCreateAccountResultEvent : onCreateAccountResultEventBase {
        public onCreateAccountResultEvent(int retCode, System.Byte[] datas)
        {
            this.RetCode = retCode;
            this.Datas = datas;
        }
    }
}
