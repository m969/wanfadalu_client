namespace MagicFire.Mmorpg.AvatarInputState
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using DG.Tweening;
    using MagicFire.Common;
    using MagicFire.Mmorpg;
    using UnityEngine;

    public class PcPlayerInputState : PlayerInputState
    {
        private readonly AvatarStateController _avatarStateController;

        public PcPlayerInputState(AvatarStateController avatarStateController)
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
                //ȡ������Ԥ��
                AvatarView.SkillManager.CancelReady();
                //�Ҽ����������ƶ�
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
                    //_avatarStateController.transform.LookAt(new Vector3(hit.point.x, _avatarStateController.transform.position.y, hit.point.z));

                    //KBEngine.Event.fireIn("RequestMove", new object[] { hit.point });
                    KBEngine.Avatar.MainAvatar.RequestMove(hit.point);
                    //AvatarView.DoMove(null);
                    //MoveVector = new Vector3(0, 0, 0);
                    //_avatarStateController.transform.DOLookAt(new Vector3(hit.point.x, _avatarStateController.transform.position.y, hit.point.z), 0.25f);
                    //MoveVector = _avatarStateController.transform.forward * AvatarView.Speed;
                }
            }
            //��������npc���󽻻�
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
                            if (npcView.EntityName == "��������" || npcView.EntityName == "�ش���"
                                || npcView.EntityName == "��ˮ�峤" || npcView.EntityName == "����"
                                || npcView.EntityName == "��С��" || npcView.EntityName == "������"
                                || npcView.EntityName == "������" || npcView.EntityName == "�����峤"
                                || npcView.EntityName == "����" || npcView.EntityName == "Ǯ����"
                                || npcView.EntityName == "�����峤" || npcView.EntityName == "������"
                                || npcView.EntityName == "��������")
                            {
                                KBEngine.Event.fireIn("RequestDialog", new object[] { SingletonGather.WorldMediator.CurrentSpaceId, npcView.EntityName });
                            }
                            if (npcView.EntityName == "����" || npcView.EntityName == "��ˮ����"
                                 || npcView.EntityName == "��ʯ����")
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
