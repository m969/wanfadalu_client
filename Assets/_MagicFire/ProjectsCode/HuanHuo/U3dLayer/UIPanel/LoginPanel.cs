using MagicFire.Common;
using PathologicalGames;
using UnityEngine.UI;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using KBEngine;
    using System;
    using System.Collections;
    using MagicFire;

    public class LoginPanel : MagicFire.MonoSingleton<LoginPanel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [SerializeField]
        private Dropdown _dropdown;

        public Dropdown Dropdown
        {
            get { return _dropdown; }
        }

        private void Start()
        {
            SingletonGather.WorldMediator.InitializeGameWorld();
            SingletonGather.ViewObjectPool.ToString();
            KBEngine.Event.registerOut("onLoginFailed", this, "OnLoginFailed");
            KBEngine.Event.registerOut("onCreateAccountResult", this, "OnCreateAccountResult");
        }

        public void Login()
        {
            switch (_dropdown.value)
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
            KBEngine.Event.fireIn("login", new object[] { Username, Password, datas });
        }

        public void OnLoginFailed(int failedcode)
        {
            var message = "";
            switch (failedcode)
            {
                case 0:
                    message = "成功。";
                    break;
                case 1:
                    message = ("服务器没有准备好。");
                    break;
                case 2:
                    message = ("服务器负载过重。");
                    break;
                case 3:
                    message = ("非法登录。");
                    break;
                case 4:
                    message = ("用户名或者密码不正确。");
                    break;
                case 5:
                    message = ("用户名不正确。");
                    break;
                case 6:
                    message = ("密码不正确。");
                    break;
                case 7:
                    message = ("创建账号失败（已经存在一个相同的账号）。");
                    break;
                case 8:
                    message = ("操作过于繁忙(例如：在服务器前一次请求未执行完毕的情况下连续N次创建账号)。");
                    break;
                case 9:
                    message = ("当前账号在另一处登录了。");
                    break;
                case 10:
                    message = ("账号已登陆。");
                    break;
                case 11:
                    message = ("与客户端关联的proxy在服务器上已经销毁。");
                    break;
                case 12:
                    message = ("EntityDefs不匹配。");
                    break;
                case 13:
                    message = ("服务器正在关闭中。");
                    break;
                case 14:
                    message = ("Email地址错误。");
                    break;
                case 15:
                    message = ("账号被冻结。");
                    break;
                case 16:
                    message = ("账号已过期。");
                    break;
                case 17:
                    message = ("账号未激活。");
                    break;
                case 18:
                    message = ("与服务端的版本不匹配。");
                    break;
                case 19:
                    message = ("操作失败。");
                    break;
                case 20:
                    message = ("服务器正在启动中。");
                    break;
                case 21:
                    message = ("未开放账号注册功能。");
                    break;
                case 22:
                    message = ("不能使用email地址。");
                    break;
                case 23:
                    message = ("找不到此账号。");
                    break;
                case 24:
                    message = ("数据库错误(请检查dbmgr日志和DB)。");
                    break;
                case 25:
                    message = ("用户自定义错误码1。");
                    break;

                case 35:
                    message = ("需要检查密码。");
                    break;
            }

            if (message != "")
                SingletonGather.UiManager.MessageBox(message);
        }

        public void OnCreateAccountResult(int retcode, byte[] datas)
        {
            OnLoginFailed(retcode);
        }

        public void ShowRegisterPanel()
        {
            SingletonGather.UiManager.TryGetOrCreatePanel("RegisterPanel").SetActive(true);
        }

        public void QuiklyLogin01()
        {
            
            switch (_dropdown.value)
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
            KBEngine.Event.fireIn("login", new object[] { "test01", "test01", datas });
        }

        public void QuiklyLogin02()
        {
            switch (_dropdown.value)
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
            KBEngine.Event.fireIn("login", new object[] { "test02", "test02", datas });
        }

        public void QuiklyLogin03()
        {
            switch (_dropdown.value)
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
            KBEngine.Event.fireIn("login", new object[] { "test03", "test03", datas });
        }
    }
}