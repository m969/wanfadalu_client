namespace MagicFire.HuanHuoUFrame{
    using HuanHuoUFrame;
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
            //Debug.Log("MuLingCunSpace LoadScene " + progressDelegate);
            yield break;
        }
        
        protected override IEnumerator UnloadScene(MuLingCunSpace scene, Action<float, string> progressDelegate) {
            //Debug.Log("MuLingCunSpace UnloadScene " + progressDelegate);
            yield break;
        }
    }
}
