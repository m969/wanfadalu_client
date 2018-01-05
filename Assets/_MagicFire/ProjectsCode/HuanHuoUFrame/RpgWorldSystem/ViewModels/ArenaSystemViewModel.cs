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
            Debug.Log("ArenaSystemViewModel:Execute RequestEnterArenaCommand");
            this.cellCall("requestEnterArena", new object[] { argument.ArenaID });
        }

        public override void Execute(RequestExitArenaCommand argument)
        {
            base.Execute(argument);
            Debug.Log("ArenaSystemViewModel:Execute RequestExitArenaCommand");
            this.cellCall("requestExitArena");
        }
    }
}
