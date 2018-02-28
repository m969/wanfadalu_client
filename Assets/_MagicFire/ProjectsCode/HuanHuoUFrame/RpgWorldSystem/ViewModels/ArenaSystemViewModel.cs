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

        public override void __init__()
        {
            base.__init__();
            if (this.isPlayer())
            {
                this.Aggregator.GetEvent<ResponseEvent>().Where(evt => { return evt.RpgInteractiveComponent.RemoteCallName == "requestEnterArena"; })
                    .Subscribe(evt =>
                    {
                        var arenaID = ((ArenaView)evt.RpgInteractiveComponent.EntityView).Arena.arenaID;
                        var command = new RequestEnterArenaCommand() { ArenaID = arenaID };
                        this.Execute(command);
                    });
            }
        }
    }
}
