using System.Text.RegularExpressions;
using MagicFire.Common;

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
    
    
    public partial class UserLoginScreenViewModel : UserLoginScreenViewModelBase
    {
        public override int CurrentServerNum
        {
            get
            {
                return base.CurrentServerNum;
            }

            set
            {
                base.CurrentServerNum = value;

                CurrentServerIp = "127.0.0.1";
                switch (CurrentServerNum)
                {
                    case 0:
                        CurrentServerIp = "127.0.0.1";
                        break;
                    case 1:
                        CurrentServerIp = "47.94.18.88";
                        break;
                    case 2:
                        CurrentServerIp = "127.0.0.1";
                        break;
                }
                KBEngine.KBEngineApp.app.getInitArgs().ip = CurrentServerIp;
            }
        }
    }
}
