namespace MagicFire.Mmorpg.AvatarInputState
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
    using MagicFire.Mmorpg;
    using MagicFire.Mmorpg.Skill;
    using MagicFire.Mmorpg.UI;
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

        public AvatarView AvatarView
        {
            get { return SingletonGather.WorldMediator.MainAvatarView; }
        }

        private AvatarStateController()
        {

        }

        private void Start()
        {
            if (SingletonGather.WorldMediator.MainAvatarView == null)
                return;

            _characterController = GetComponent<CharacterController>();

            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    _playerInputState = new AndroidPlayerInputState(this);
                }
                else
                {
                    if (SingletonGather.XmlSceneManager.ControlMode == XmlSceneManager.ControlModeEnum.PcControl)
                        _playerInputState = new PcPlayerInputState(this);
                    else
                        _playerInputState = new AndroidPlayerInputState(this);
                }
            }

            _deadInputState = new DeadInputState(this);
            _currentInputState = _playerInputState;

            KBEngine.Avatar.MainAvatar.SubscribeMethodCall(KBEngine.Avatar.DialogSystem.DoDialog, DoDialog);
        }

        private void Update()
        {
            if (SingletonGather.WorldMediator.MainAvatarView == null)
                return;

            _currentInputState.Run();
        }

        private void FixedUpdate()
        {
            if (SingletonGather.WorldMediator.MainAvatarView == null)
                return;

            _currentInputState.FixedRun();

            if (transform.position.y < -4)
            {
                transform.position = new Vector3(transform.position.x, 2, transform.position.z);
            }
            if (transform.eulerAngles.x != 0 || transform.eulerAngles.z != 0)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            if (KBEngine.KBEngineApp.app == null)
            {
                return;
            }
            //KBEngine.Event.fireIn("updatePlayer", transform.position.x, transform.position.y, transform.position.z, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "TargetPoint")
            {
                //PlayerState.MoveVector = new Vector3(0, 0, 0);
                //KBEngine.Event.fireIn("StopMove");
                //AvatarView.OnStopMove(null);
            }
        }

        public void OnDie()
        {
            _currentInputState = _deadInputState;
        }

        public List<object> GetBagGoodsList()
        {
            return ((KBEngine.Avatar)AvatarView.Model).AvatarBag;
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
            dialogPanel.GetComponent<DialogPanel>().ShowDialog(args[1] as string);
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
            storePanel.GetComponent<StorePanel>().CurrentNpc = npc;
            if (!storePanel.activeInHierarchy)
            {
                storePanel.SetActive(true);
            }
        }

        public void StartMove()
        {
            KBEngine.Event.fireIn("RequestMove", new object[] { transform.forward * 2 });
        }

        public void MoveMainAvatar(Vector2 vec)
        {
            var androidPlayerState = _playerInputState as AndroidPlayerInputState;
            if (androidPlayerState != null) androidPlayerState.MoveMainAvatar(vec);
        }

        public void EndMove()
        {
            //PlayerState.MoveVector = Vector3.zero;
            //KBEngine.Event.fireIn("StopMove");
            //AvatarView.OnStopMove(null);
        }

        public void SkillQReady()
        {
            AvatarView.SkillManager.SkillReady(1);
        }

        public void OnSkillQReadying(Vector2 vec)
        {
            var skill = AvatarView.SkillManager.GetSkillRef(1) as SkillQ;
            if (skill != null) skill.SkillDirection = vec;
        }

        public void DoSkillQ()
        {
            AvatarView.SkillManager.DoSkill(1);
        }

        public void SkillWReady()
        {
            AvatarView.SkillManager.SkillReady(2);
        }

        public void OnSkillWReadying(Vector2 vec)
        {
            var skill = AvatarView.SkillManager.GetSkillRef(2) as SkillW;
            if (skill != null) skill.SkillTrajectoryPosition = vec;
        }

        public void DoSkillW()
        {
            AvatarView.SkillManager.DoSkill(2);
        }

        public void DoSkillE()
        {
            AvatarView.SkillManager.DoSkill(3);
        }

    }
}
//
//Vector3 a = _player.transform.eulerAngles;
//Vector3 b = hit.point;
//Vector3 c = Vector3.Cross(a, b);
//float angle = Vector3.Angle(a, b);

//// b 到 a 的夹角
//float sign = Mathf.Sign(Vector3.Dot(c.normalized, Vector3.Cross(a.normalized, b.normalized)));
//float signed_angle = angle * sign;

//Debug.Log("b -> a :" + signed_angle);

//// a 到 b 的夹角
//sign = Mathf.Sign(Vector3.Dot(c.normalized, Vector3.Cross(b.normalized, a.normalized)));
//signed_angle = angle * sign;

//Debug.Log("a -> b :" + signed_angle);
//