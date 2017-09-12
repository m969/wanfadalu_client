namespace MagicFire.HuanHuoMVVM{
    using MagicFire.HuanHuoMVVM;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.Services;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Text.RegularExpressions;
    
    
    public class RegistePanel : RegistePanelBase {

        private string _confirmPassword;
        private string _password;

        [SerializeField]
        private Text _usernameFeedbackText;
        [SerializeField]
        private Text _passwordFeedbackText;
        [SerializeField]
        private Text _password2FeedbackText;

        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as LoginScreenViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.LoginScreen to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

        public override void UsernameChanged(string arg1)
        {
            base.UsernameChanged(arg1);

            if (arg1.Length < 6)
            {
                _usernameFeedbackText.color = Color.red;
                _usernameFeedbackText.text = "账号长度小于6";
            }
            else
            {
                if (IsUsername(arg1))
                {
                    _usernameFeedbackText.color = Color.green;
                    _usernameFeedbackText.text = "账号合法";
                }
                else
                {
                    _usernameFeedbackText.color = Color.red;
                    _usernameFeedbackText.text = "账号非法";
                }
            }
        }

        public override void PasswordChanged(string arg1)
        {
            base.PasswordChanged(arg1);

            _password = arg1;

            if (_confirmPassword != null)
            {
                if (!_confirmPassword.Equals(_password))
                {
                    _password2FeedbackText.color = Color.red;
                    _password2FeedbackText.text = "两次输入的密码不相同";
                }
                else
                {
                    _password2FeedbackText.color = Color.green;
                    _password2FeedbackText.text = "两次输入的密码相同";
                }
            }

            if (_password.Length < 6)
            {
                _passwordFeedbackText.color = Color.red;
                _passwordFeedbackText.text = "密码长度小于6";
            }
            else
            {
                if (IsPassword(_password))
                {
                    _passwordFeedbackText.color = Color.green;
                    _passwordFeedbackText.text = "密码合法";
                }
                else
                {
                    _passwordFeedbackText.color = Color.red;
                    _passwordFeedbackText.text = "密码非法";
                }
            }
        }

        public override void SecondPasswordChanged(string arg1)
        {
            base.SecondPasswordChanged(arg1);

            _confirmPassword = arg1;

            if (!_confirmPassword.Equals(_password))
            {
                _password2FeedbackText.color = Color.red;
                _password2FeedbackText.text = "两次输入的密码不相同";
            }
            else
            {
                _password2FeedbackText.color = Color.green;
                _password2FeedbackText.text = "两次输入的密码相同";
            }
        }

        private bool IsUsername(string str)
        {
            //是否是用户名:\w表示数字，英文大小写字母，下划线的组合，即匹配包括下划线的单词字符

            //^[\\w]+$ ^[^\u4e00-\u9fa5]{0,}$

            var rgx = new Regex("^[^\u4e00-\u9fa5]{0,}$");
            var result = rgx.IsMatch(str);

            if (result == false)
                return false;

            rgx = new Regex("^[\\w%!]+$");
            return rgx.IsMatch(str);
        }

        private bool IsPassword(string pwd)
        {
            //\W表示非单词字符，密码的字符一般根据实际需要选择范围

            //^[A-Za-z0-9]+$　　//匹配由数字和26个英文字母组成的字符串
            //^\w+$　　//匹配由数字、26个英文字母或者下划线组成的字符串

            //^[\d]+$ 纯数字密码
            //^[\da-z]+$ 数字+小写字母
            //^[\w%!]+$ 单词字符+某些特殊字符
            //^[\\w\\W]+$ 密码范围广

            var rgx = new Regex("^[^\u4e00-\u9fa5]{0,}$");
            var result = rgx.IsMatch(pwd);

            if (result == false)
                return false;

            rgx = new Regex("^[\\w%!]+$");//单词字符+某些特殊字符
            return rgx.IsMatch(pwd);
        }

    }
}
