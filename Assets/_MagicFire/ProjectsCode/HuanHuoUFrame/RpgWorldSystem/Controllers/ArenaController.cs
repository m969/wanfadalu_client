namespace MagicFire.HuanHuoUFrame {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UniRx;
    using UnityEngine;

    public class ArenaController : ArenaControllerBase {
        
        public override void InitializeArena(ArenaViewModel viewModel) {
            base.InitializeArena(viewModel);
            // This is called when a ArenaViewModel is created

            this.OnEvent<ExitArenaEvent>().Subscribe(evt =>
            {
                
            });
        }
    }
}
