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
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    
    
    public partial class EntityCommonViewModel : EntityCommonViewModelBase {
        public override void __init__()
        {
            base.__init__();
            Debug.Log(this.GetType().Name);
        }

        public override void onDestroy()
        {
            if (isPlayer())
                KBEngine.Event.deregisterIn(this);
        }

        public override void onEnterWorld()
        {
            base.onEnterWorld();
            if (isPlayer())
                Aggregator.Publish(new OnMainAvatarEnterWorldEvent() { Entity = this });
        }
    }
}
