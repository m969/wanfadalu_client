namespace MagicFire.HuanHuoUFrame {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.ECS;
    using uFrame.ECS.Components;
    using uFrame.Json;
    using UniRx;
    using UnityEngine;


    public partial class RightKeyMenuComponent : uFrame.ECS.Components.EcsComponent
    {
        public int ZhenFa;

        public void DoZhenFa()
        {
            gameObject.SetActive(false);
        }
    }
}
