namespace MagicFire.HuanHuoUFrame {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    
    
    public class ArenaSystemController : ArenaSystemControllerBase {
        
        public override void InitializeArenaSystem(ArenaSystemViewModel viewModel) {
            base.InitializeArenaSystem(viewModel);
            // This is called when a ArenaSystemViewModel is created
        }
        
        public override void RequestEnterArena(ArenaSystemViewModel viewModel, RequestEnterArenaCommand arg) {
            base.RequestEnterArena(viewModel, arg);
        }
    }
}
