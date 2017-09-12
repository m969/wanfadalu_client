namespace MagicFire.HuanHuoMVVM{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    
    
    public class UserLoginScreenController : UserLoginScreenControllerBase
    {
        
        public override void InitializeUserLoginScreen(UserLoginScreenViewModel viewModel) {
            base.InitializeUserLoginScreen(viewModel);
            // This is called when a LoginPanelViewModel is created
        }
        
        public override void UserLogin(UserLoginScreenViewModel viewModel, UserLoginCommand arg) {
            base.UserLogin(viewModel, arg);
        }

        public override void ShowRegistePanel(UserLoginScreenViewModel viewModel, ShowRegistePanelCommand arg)
        {
            arg.RegistePanel = viewModel.RegistePanel;
            UnityEngine.Debug.Log("LoginScreenController:ShowRegisterPanel");
            arg.RegistePanel.gameObject.SetActive(true);
            base.ShowRegistePanel(viewModel, arg);
        }

        public override void CloseRegistePanel(UserLoginScreenViewModel viewModel, CloseRegistePanelCommand arg)
        {
            arg.RegistePanel = viewModel.RegistePanel;
            UnityEngine.Debug.Log("LoginScreenController:CloseRegistePanel");
            arg.RegistePanel.gameObject.SetActive(false);
            base.CloseRegistePanel(viewModel, arg);
        }

    }
}
