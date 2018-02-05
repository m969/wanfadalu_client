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
                Debug.Log("ArenaSystemViewModel:Subscribe ResponseEvent");
                this.Aggregator.GetEvent<ResponseEvent>().Where(evt => { return evt.RpgInteractiveComponent.RemoteCallName == "requestEnterArena"; })
                    .Subscribe(evt =>
                    {
                        Debug.Log("ArenaSystemViewModel:OnEvent<ResponseEvent>()");
                        var arenaID = ((ArenaView)evt.RpgInteractiveComponent.EntityView).Arena.arenaID;
                        var command = new RequestEnterArenaCommand() { ArenaID = arenaID };
                        this.Execute(command);
                    });
            }
        }

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

        public override void Execute(RequestRankingListCommand argument)
        {
            base.Execute(argument);
            Debug.Log("ArenaSystemViewModel:Execute RequestRankingListCommand");
            this.cellCall("requestRankingList");
        }

        public override void Execute(RequestSelfRankingCommand argument)
        {
            base.Execute(argument);
            Debug.Log("ArenaSystemViewModel:Execute RequestSelfRankingCommand");
            this.cellCall("requestSelfRanking");
        }
    }
}
