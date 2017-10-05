namespace MagicFire.HuanHuoUFrame
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;
    using DG.Tweening;
    using MagicFire;
    using MagicFire.Common;
    using MagicFire.Common.Plugin;
    using MagicFire.SceneManagement;
    using Model = KBEngine.Model;
    using Object = UnityEngine.Object;

    public class AvatarStateController : MonoSingleton<AvatarStateController>
    {
        public const int RayCastHitDist = 400;
        private static CharacterController _characterController;
        private PlayerInputState _currentInputState;
        private PlayerInputState _playerInputState;
        private DeadInputState _deadInputState;

        public CharacterController CharacterController
        {
            get { return _characterController; }
        }

        private AvatarStateController()
        {

        }

        public void Init(AvatarView avatarView)
        {
            _characterController = GetComponent<CharacterController>();
            _playerInputState = new PcPlayerInputState(this);
            PlayerInputState.AvatarView = avatarView;
            _deadInputState = new DeadInputState(this);
            _currentInputState = _playerInputState;
        }

        private void Start()
        {

        }

        private void Update()
        {
            if (_currentInputState != null)
                _currentInputState.Run();
        }

        private void FixedUpdate()
        {
            if (_currentInputState != null)
                _currentInputState.FixedRun();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "TargetPoint")
            {
            }
        }

        public void OnDie()
        {
            _currentInputState = _deadInputState;
        }

        public void DoDialog(object[] args)
        {
            var dialogPanel = UiManager.Instance.TryGetOrCreatePanel("DialogPanel");
            if (dialogPanel == null)
            {
                return;
            }
            if (!dialogPanel.activeInHierarchy)
            {
                dialogPanel.SetActive(true);
            }
            //dialogPanel.GetComponent<DialogPanel>().ShowDialog(args[1] as string);
        }

        public void BuyResult(bool result)
        {
            var messageBox = SingletonGather.UiManager.TryGetOrCreatePanel("MessageBox");

            if (messageBox != null)
            {
                messageBox.transform.SetParent(UiManager.Instance.Canvas.transform);
                messageBox.transform.localPosition = new Vector3(0, 0, 0);
                messageBox.transform.Find("MessageText").GetComponent<Text>().text = result == true ? "购买成功" : "购买失败";
            }
        }

        public void DoStore(NpcView npc)
        {
            var storePanel = UiManager.Instance.TryGetOrCreatePanel("TheStorePanel");
            if (storePanel == null)
            {
                return;
            }
            //storePanel.GetComponent<StorePanel>().CurrentNpc = npc;
            if (!storePanel.activeInHierarchy)
            {
                storePanel.SetActive(true);
            }
        }
    }
}