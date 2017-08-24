namespace MagicFire.Mmorpg.AvatarState
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using DG.Tweening;
    using MagicFire.Common;
    using MagicFire.Mmorpg;
    using UnityEngine;

    public class PcPlayerState : PlayerState
    {
        private readonly AvatarStateController _avatarStateController;

        public PcPlayerState(AvatarStateController avatarStateController)
        {
            _avatarStateController = avatarStateController;
        }

        public override void Run()
        {
            //if (!_avatarStateController.CharacterController.isGrounded)
            //{
            //    if (Math.Abs(MoveVector.y - (-2)) > 0)
            //    {
            //        MoveVector = new Vector3(MoveVector.x, -2, MoveVector.z);
            //    }
            //}
            if (Input.GetMouseButtonDown(1))
            {
                //取消技能预备
                AvatarView.SkillManager.CancelReady();
                //右键点击地面移动
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                var layerMask = 1 << LayerMask.NameToLayer("Terrian");
                if (Physics.Raycast(ray, out hit, AvatarStateController.RayCastHitDist, layerMask))
                {
                    if (ClickPointObject == null)
                    {
                        ClickPointObject = UnityEngine.Object.Instantiate(ClickPointAuxiliaryPrefab, hit.point, Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        if (!ClickPointObject.activeInHierarchy)
                        {
                            ClickPointObject.SetActive(true);
                        }
                        ClickPointObject.transform.position = hit.point;
                    }
                    _avatarStateController.transform.LookAt(new Vector3(hit.point.x, _avatarStateController.transform.position.y, hit.point.z));

                    //KBEngine.Event.fireIn("RequestMove", new object[] { hit.point });
                    KBEngine.Avatar.MainAvatar.RequestMove(hit.point);
                    //AvatarView.DoMove(null);
                    //MoveVector = new Vector3(0, 0, 0);
                    _avatarStateController.transform.DOLookAt(new Vector3(hit.point.x, _avatarStateController.transform.position.y, hit.point.z), 0.25f);
                    //MoveVector = _avatarStateController.transform.forward * AvatarView.Speed;
                }
            }
            //左键点击npc请求交互
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

            if (Input.GetKeyDown(KeyCode.Q))
                AvatarView.SkillManager.SkillReady(1);
            if (Input.GetKeyDown(KeyCode.W))
                AvatarView.SkillManager.SkillReady(2);
            if (Input.GetKeyDown(KeyCode.E))
                AvatarView.SkillManager.SkillReady(3);
        }

        public override void FixedRun()
        {
            //_avatarStateController.CharacterController.Move(MoveVector);
        }
    }

}
