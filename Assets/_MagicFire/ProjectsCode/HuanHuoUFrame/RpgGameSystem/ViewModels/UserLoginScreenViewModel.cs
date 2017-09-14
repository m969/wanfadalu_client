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

        public void Login(string username, string password)
        {
            byte[] datas;
            datas = new byte[1];
            KBEngine.Event.fireIn("login", new object[] { username, password, datas });
        }

        public override void Execute(UserLoginCommand argument)
        {
            base.Execute(argument);
            Login(Username, Password);
        }

        public override void Execute(ShowRegistePanelCommand argument)
        {
            //argument.RegistePanel = RegistePanel;
            base.Execute(argument);
            //RegistePanel.gameObject.SetActive(true);
        }

        public override void Execute(CloseRegistePanelCommand argument)
        {
            //argument.RegistePanel = RegistePanel;
            base.Execute(argument);
            //RegistePanel.gameObject.SetActive(false);
        }

        public override void Execute(Test01LoginCommand argument)
        {
            Debug.Log("Execute Test01LoginCommand");
            base.Execute(argument);
            Login("test01", "test01");
            Debug.Log("Execute Test01LoginCommand_");
        }

        public override void Execute(Test02LoginCommand argument)
        {
            base.Execute(argument);
            Login("test02", "test02");
        }

        public override void Execute(Test03LoginCommand argument)
        {
            base.Execute(argument);
            Login("test03", "test03");
        }

        public override void Execute(RegisteUserCommand argument)
        {
            base.Execute(argument);
            byte[] datas;
            datas = new byte[1];
            KBEngine.Event.fireIn("createAccount", new object[] { Username, Password, datas });
        }
    }
}
