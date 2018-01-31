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
    
    
    public class YunLingZongSpaceLoader : YunLingZongSpaceLoaderBase {
        
        protected override IEnumerator LoadScene(YunLingZongSpace scene, Action<float, string> progressDelegate) {
            Debug.Log("YunLingZongSpaceLoader LoadScene " + progressDelegate);
            yield break;
        }
        
        protected override IEnumerator UnloadScene(YunLingZongSpace scene, Action<float, string> progressDelegate) {
            Debug.Log("YunLingZongSpaceLoader UnloadScene " + progressDelegate);
            yield break;
        }
    }
}
