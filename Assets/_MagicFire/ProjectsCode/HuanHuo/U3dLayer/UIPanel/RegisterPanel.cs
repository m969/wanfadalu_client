using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using KBEngine;
using MagicFire.Common;
using MagicFire.Mmorpg.UI;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : Panel
{
    private string _username;
    private string _password;
    private string _confirmPassword;

    public string Username
    {
        get
        {
            return _username;
        }
        set
        {
            _username = value;
            if (_username.Length < 6)
            {
                _usernameFeedbackText.color = Color.red;
                _usernameFeedbackText.text = "账号长度小于6";
            }
            else
            {
                if (IsUsername(_username))
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
    }

    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            _password = value;

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
    }

    public string ConfirmPassword
    {
        get
        {
            return _confirmPassword;
        }
        set
        {
            _confirmPassword = value;
            
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
    }

    [SerializeField]
    private Text _usernameFeedbackText;
    [SerializeField]
    private Text _passwordFeedbackText;
    [SerializeField]
    private Text _password2FeedbackText;

    public void Register()
    {
        switch (LoginPanel.Instance.Dropdown.value)
        {
            case 0:
                ClientApp.Instance.ip = "127.0.0.1";
                break;
            case 1:
                KBEngineApp.app.getInitArgs().ip = "47.94.18.88";
                Debug.Log("ip 47.94.18.88");
                break;
            case 2:
                ClientApp.Instance.ip = "127.0.0.1";
                break;
        }
        byte[] datas;
        datas = new byte[1];
        KBEngine.Event.fireIn("createAccount", new object[] { Username, Password, datas });
    }

    //是否是用户名:\w表示数字，英文大小写字母，下划线的组合，即匹配包括下划线的单词字符
    public bool IsUsername(string str)
    {
        //^[\\w]+$ ^[^\u4e00-\u9fa5]{0,}$

        var rgx = new Regex("^[^\u4e00-\u9fa5]{0,}$");
        var result = rgx.IsMatch(str);

        if (result == false)
            return false;

        rgx = new Regex("^[\\w%!]+$");
        return rgx.IsMatch(str);
    }

    //\W表示非单词字符，密码的字符一般根据实际需要选择范围

    //^[A-Za-z0-9]+$　　//匹配由数字和26个英文字母组成的字符串
    //^\w+$　　//匹配由数字、26个英文字母或者下划线组成的字符串

    //^[\d]+$ 纯数字密码
    //^[\da-z]+$ 数字+小写字母
    //^[\w%!]+$ 单词字符+某些特殊字符
    //^[\\w\\W]+$ 密码范围广
    public bool IsPassword(string pwd)
    {

        var rgx = new Regex("^[^\u4e00-\u9fa5]{0,}$");
        var result = rgx.IsMatch(pwd);

        if (result == false)
            return false;

        rgx = new Regex("^[\\w%!]+$");//单词字符+某些特殊字符
        return rgx.IsMatch(pwd);
    }

    public void ShowLoginPanel()
    {
        SingletonGather.UiManager.TryGetOrCreatePanel("LoginPanel");
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
