using uFrame.IOC;

namespace MagicFire.HuanHuoUFrame{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    
    
    public class UserLoginScreenController : UserLoginScreenControllerBase
    {
        [Inject]
        public RpgGameSystemLoader RpgGameSystemLoader;

        public override void InitializeUserLoginScreen(UserLoginScreenViewModel viewModel) {
            base.InitializeUserLoginScreen(viewModel);
            // This is called when a LoginPanelViewModel is created
        }
        
        public override void UserLogin(UserLoginScreenViewModel viewModel, UserLoginCommand arg) {
            base.UserLogin(viewModel, arg);
            KBEngine.Event.fireIn("login", new object[] { viewModel.Username, viewModel.Password, new byte[1] });
        }

        public override void RegisteUser(UserLoginScreenViewModel viewModel, RegisteUserCommand arg)
        {
            base.RegisteUser(viewModel, arg);
            KBEngine.Event.fireIn("createAccount", new object[] { viewModel.Username, viewModel.Password, new byte[1] });
        }

        public override void ShowRegistePanel(UserLoginScreenViewModel viewModel, ShowRegistePanelCommand arg)
        {
            arg.RegistePanel = viewModel.RegistePanel;
            arg.RegistePanel.gameObject.SetActive(true);
            base.ShowRegistePanel(viewModel, arg);
        }

        public override void CloseRegistePanel(UserLoginScreenViewModel viewModel, CloseRegistePanelCommand arg)
        {
            arg.RegistePanel = viewModel.RegistePanel;
            arg.RegistePanel.gameObject.SetActive(false);
            base.CloseRegistePanel(viewModel, arg);
        }

        public override void Test01Login(UserLoginScreenViewModel viewModel, Test01LoginCommand arg)
        {
            base.Test01Login(viewModel, arg);
            KBEngine.Event.fireIn("login", new object[] { "test01", "test01", new byte[1] });
        }

        public override void Test02Login(UserLoginScreenViewModel viewModel, Test02LoginCommand arg)
        {
            base.Test02Login(viewModel, arg);
            KBEngine.Event.fireIn("login", new object[] { "test02", "test02", new byte[1] });
        }

        public override void Test03Login(UserLoginScreenViewModel viewModel, Test03LoginCommand arg)
        {
            base.Test03Login(viewModel, arg);
            KBEngine.Event.fireIn("login", new object[] { "test03", "test03", new byte[1] });
        }
    }
}
