using MagicFire.Common;
using MagicFire.Common.Plugin;

namespace MagicFire.Mmorpg.AvatarInputState
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AndroidPlayerInputState : PlayerInputState
    {
        private readonly AvatarStateController _avatarStateController;
        private Transform _avatarRotate;

        public AndroidPlayerInputState(AvatarStateController avatarStateController)
        {
            _avatarStateController = avatarStateController;
            Object.Instantiate(AssetTool.LoadAuxiliaryAssetByName("EasyTouchControlsCanvas"));
            Object.Instantiate(AssetTool.LoadAuxiliaryAssetByName("InputManager"));
        }

        public override void Run()
        {
            //��������npc���󽻻�
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