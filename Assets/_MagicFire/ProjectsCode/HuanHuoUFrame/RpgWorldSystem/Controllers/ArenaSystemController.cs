namespace MagicFire.HuanHuoUFrame {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UniRx;
    using uFrame.Kernel;
    using UnityEngine;
    
    
    public class ArenaSystemController : ArenaSystemControllerBase {
        
        public override void InitializeArenaSystem(ArenaSystemViewModel viewModel) {
            base.InitializeArenaSystem(viewModel);
            // This is called when a ArenaSystemViewModel is created
        }

        public override void RequestEnterArena(ArenaSystemViewModel viewModel, RequestEnterArenaCommand arg) {
            base.RequestEnterArena(viewModel, arg);
            Debug.Log("ArenaSystemController:RequestEnterArena");
            viewModel.cellCall("requestEnterArena", new object[] { arg.ArenaID });
        }

        public override void RequestExitArena(ArenaSystemViewModel viewModel, RequestExitArenaCommand arg)
        {
            base.RequestExitArena(viewModel, arg);
            Debug.Log("ArenaSystemController:RequestExitArena");
            viewModel.cellCall("requestExitArena");
        }

        public override void RequestRankingList(ArenaSystemViewModel viewModel, RequestRankingListCommand arg)
        {
            base.RequestRankingList(viewModel, arg);
            Debug.Log("ArenaSystemController:RequestRankingList");
            viewModel.baseCall("requestRankingList");
        }

        public override void RequestSelfRanking(ArenaSystemViewModel viewModel, RequestSelfRankingCommand arg)
        {
            base.RequestSelfRanking(viewModel, arg);
            Debug.Log("ArenaSystemController:RequestSelfRanking");
            viewModel.baseCall("requestSelfRanking");
        }
    }
}
