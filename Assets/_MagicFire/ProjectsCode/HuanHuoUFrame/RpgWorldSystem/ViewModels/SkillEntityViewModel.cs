﻿namespace MagicFire.HuanHuoUFrame {
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
    
    
    public partial class SkillEntityViewModel : SkillEntityViewModelBase {
        public override void Execute(RequestCastSkillByNameCommand argument)
        {
            base.Execute(argument);
            cellCall("requestCastSkill", new object[] { argument.skillName, argument.argsString });
        }
    }
}
