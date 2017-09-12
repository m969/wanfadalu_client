namespace MagicFire.HuanHuoMVVM{
    using HuanHuoMVVM;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using UnityEngine;
    
    
    public class MuLingCunSpaceLoader : MuLingCunSpaceLoaderBase {
        
        protected override IEnumerator LoadScene(MuLingCunSpace scene, Action<float, string> progressDelegate) {
            yield break;
        }
        
        protected override IEnumerator UnloadScene(MuLingCunSpace scene, Action<float, string> progressDelegate) {
            yield break;
        }
    }
}
