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
    
    
    public partial class ArenaSystemViewModel : ArenaSystemViewModelBase {
        public override void Execute(RequestEnterArenaCommand argument)
        {
            base.Execute(argument);
            //this.cellCall("requestEnterArena", new object[] { argument.ArenaID });
        }
    }
}
