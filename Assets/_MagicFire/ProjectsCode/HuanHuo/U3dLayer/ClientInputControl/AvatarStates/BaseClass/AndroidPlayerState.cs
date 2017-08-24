using MagicFire.Common;
using MagicFire.Common.Plugin;

namespace MagicFire.Mmorpg.AvatarState
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AndroidPlayerState : PlayerState
    {
        private readonly AvatarStateController _avatarStateController;
        private Transform _avatarRotate;

        public AndroidPlayerState(AvatarStateController avatarStateController)
        {
            _avatarStateController = avatarStateController;
            Object.Instantiate(AssetTool.LoadAuxiliaryAssetByName("EasyTouchControlsCanvas"));
            Object.Instantiate(AssetTool.LoadAuxiliaryAssetByName("InputManager"));
        }

        public override void Run()
        {
            //左键点击npc请求交互
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Npc")
                    {
                        if (Mathf.Abs(Vector3.Distance(_avatarStateController.transform.position, hit.collider.transform.position)) < 5)
                        {
                            NpcView npcView = hit.collider.GetComponent<NpcView>();
                            if (npcView.EntityName == "新手引导" || npcView.EntityName == "守村人"
                                || npcView.EntityName == "上水村长" || npcView.EntityName == "工匠"
                                || npcView.EntityName == "店小二" || npcView.EntityName == "刘公子"
                                || npcView.EntityName == "神秘人" || npcView.EntityName == "陈丘村长"
                                || npcView.EntityName == "姑娘" || npcView.EntityName == "钱大娘"
                                || npcView.EntityName == "兽族族长" || npcView.EntityName == "看箱人"
                                || npcView.EntityName == "宝马盗贼")
                            {
                                KBEngine.Event.fireIn("RequestDialog", new object[] { SingletonGather.WorldMediator.CurrentSpaceId, npcView.EntityName });
                            }
                            if (npcView.EntityName == "商人" || npcView.EntityName == "上水商人"
                                 || npcView.EntityName == "钻石商人")
                            {
                                _avatarStateController.DoStore(npcView);
                            }
                        }
                    }
                }
            }
        }

        public override void FixedRun()
        {
            _avatarStateController.CharacterController.Move(MoveVector);
        }

        public void MoveMainAvatar(Vector2 vec)
        {
            if (_avatarRotate == null)
                _avatarRotate = new GameObject("AvatarRotate").transform;
            _avatarRotate.LookAt(new Vector3(_avatarRotate.position.x + vec.x, _avatarRotate.position.y, _avatarRotate.position.z + vec.y));
            _avatarStateController.transform.eulerAngles = new Vector3(0, _avatarRotate.eulerAngles.y, 0);

            SingletonGather.WorldMediator.MainAvatarView.DoMove(null);
            MoveVector = _avatarStateController.transform.forward * SingletonGather.WorldMediator.MainAvatarView.Speed;
        }
    }

}