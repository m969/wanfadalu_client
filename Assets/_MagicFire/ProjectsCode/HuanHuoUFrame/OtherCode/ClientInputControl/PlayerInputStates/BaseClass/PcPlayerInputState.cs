using PathologicalGames;

namespace MagicFire.HuanHuoUFrame
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
            if (Input.GetMouseButtonDown(1))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                var layerMask = 1 << LayerMask.NameToLayer("Terrian");
                if (Physics.Raycast(ray, out hit, AvatarStateController.RayCastHitDist, layerMask))
                {
                    if (ClickPointObject == null)
                    {
                        var viewPool = PoolManager.Pools["AuxiliaryPool"];
                        ClickPointObject = viewPool.Spawn("ClickPointAuxiliary").gameObject;
                        //ClickPointObject = UnityEngine.Object.Instantiate(ClickPointAuxiliaryPrefab, hit.point, Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        if (!ClickPointObject.activeInHierarchy)
                        {
                            ClickPointObject.SetActive(true);
                        }
                        ClickPointObject.transform.position = hit.point;
                    }
                    KBEngine.Event.fireIn("RequestMove", new object[] { hit.point });
                    AvatarView.SkillManager.CancelReady();
                }
            }
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
            if (Input.GetKeyDown(KeyCode.R))
                AvatarView.SkillManager.GetSkillRef(4).Conjure();
        }

        public override void FixedRun()
        {
            //_avatarStateController.CharacterController.Move(MoveVector);
        }
    }

}
