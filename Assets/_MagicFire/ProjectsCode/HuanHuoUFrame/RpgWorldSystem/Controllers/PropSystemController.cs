namespace MagicFire.HuanHuoUFrame {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    
    
    public class PropSystemController : PropSystemControllerBase {
        
        public override void InitializePropSystem(PropSystemViewModel viewModel) {
            base.InitializePropSystem(viewModel);
            // This is called when a PropSystemViewModel is created
        }

        public override void RequestPullStorePropList(PropSystemViewModel viewModel, RequestPullStorePropListCommand arg)
        {
            Debug.Log("PropSystemController:RequestPullStorePropList");
            viewModel.cellCall("requestPullStorePropList", arg.StoreNpcID);
        }
    }
}
