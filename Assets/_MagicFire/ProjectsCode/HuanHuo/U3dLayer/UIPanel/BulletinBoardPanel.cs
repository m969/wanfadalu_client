using System;
using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using MagicFire.Mmorpg.UI;
    using UnityEngine;
    using UnityEngine.UI;

    public class BulletinBoardPanel : Panel
    {
        [SerializeField]
        private Text _bulletinContentText;

        protected override void Start()
        {
            base.Start();
            transform.SetParent(SingletonGather.UiManager.CanvasLayerFront.transform);
            var rect = GetComponent<RectTransform>();

            rect.offsetMin = new Vector2(0, -50);
            rect.offsetMax = new Vector2(0, -50);
            rect.sizeDelta = new Vector2(0, 50);
            rect.anchorMin = new Vector2(0.0f, 1.0f);
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot= new Vector2(0.5f, 0.5f);
            transform.localScale = new Vector3(1, 1, 1);
        }

        //当主玩家激活
        public void OnMainAvatarActive(KBEngine.Model avatar)
        {
            Subscribe();
            gameObject.SetActive(false);
        }

        //订阅属性更新、方法调用
        private void Subscribe()
        {
            KBEngine.Avatar.MainAvatar.SubscribeMethodCall(KBEngine.Avatar.OnPublishBulletin, OnPublishBulletin);
            KBEngine.Avatar.MainAvatar.SubscribeMethodCall(KBEngine.Avatar.EntityObject.OnEntityDestroy, OnModelDestroy);
        }

        //取消订阅
        private void Desubscribe()
        {
            KBEngine.Avatar.MainAvatar.DesubscribeMethodCall(KBEngine.Avatar.OnPublishBulletin, OnPublishBulletin);
            KBEngine.Avatar.MainAvatar.DesubscribeMethodCall(KBEngine.Avatar.EntityObject.OnEntityDestroy, OnModelDestroy);
        }

        private void OnModelDestroy(object[] objects)
        {
            Desubscribe();
        }

        private void OnPublishBulletin(object[] args)
        {
            Debug.Log("OnPublishBulletin:" + args[0]);
            gameObject.SetActive(true);
            _bulletinContentText.text = "" + args[0];

            this.DelayExecute(() =>
            {
                gameObject.SetActive(false);
            }, 
            2);
        }

    }
}
