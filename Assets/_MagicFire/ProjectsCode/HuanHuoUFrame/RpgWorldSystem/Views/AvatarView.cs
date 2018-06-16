namespace MagicFire.HuanHuoUFrame {
    using DG.Tweening;
    using MagicFire.HuanHuoUFrame;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.Services;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    using Newtonsoft.Json.Linq;
    using PathologicalGames;


    public class AvatarView : AvatarViewBase {
        [SerializeField]
        [Range(0.1f, 1.0f)]
        private float _speed;
        [SerializeField]
        private bool _clientControl = false;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Animation _animation;
        [SerializeField]
        private CharacterController _characterController;
        [SerializeField]
        private Transform _weaponListNode;
        private Dictionary<ulong, Prop> _magicWeaponList;
        public JObject GongFaKeyOptions;


        public bool ClientControl
        {
            get
            {
                return _clientControl;
            }
        }

        public float Speed
        {
            get { return _speed; }
        }

        public Animator Animator
        {
            get
            {
                return _animator;
            }
        }

        public Animation Animation
        {
            get
            {
                return _animation;
            }
        }

        public CharacterController CharacterController
        {
            get
            {
                return _characterController;
            }
        }


        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as AvatarViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Avatar to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (this.Avatar.isPlayer())
            {
                this.tag = "Main Avatar";
                if (!_clientControl)
                {
                    Observable.EveryFixedUpdate().Subscribe(evt =>
                        {
                            transform.DOMove(ViewModelObject.position, 0.2f);
                            var dir = ViewModelObject.direction;
                            transform.eulerAngles = new Vector3(dir.x, dir.z, dir.y);
                        });
                }
            }
            //var v = new Vector3(ViewModelObject.position.x, 5, ViewModelObject.position.z);
            //transform.position = v;
            //ViewModelObject.position = v;
            //
            //var ray = new Ray();
            //ray.origin = ViewModelObject.position + Vector3.up * 10;
            //ray.direction = Vector3.down;
            //RaycastHit raycastHit;
            //Physics.Raycast(ray, out raycastHit, 20, 1 << LayerMask.NameToLayer("Terrian"));
            //transform.position = raycastHit.point;
            //Debug.Log("raycastHit.point " + raycastHit.point);
            //Debug.Log("ViewModelObject.position " + ViewModelObject.position);
            //Debug.Log("transform.position " + transform.position);
            Observable.EveryUpdate().Subscribe(evt =>{ transform.GetChild(0).localPosition = Vector3.zero; });
        }

        public override void TeleportExecuted(TeleportCommand command)
        {
            base.TeleportExecuted(command);
            transform.position = command.Position;
        }

        public override void canMoveChanged(int arg1)
        {
            //Debug.Log("AvatarView:canMoveChanged arg1=" + arg1);
            base.canMoveChanged(arg1);
            this._canMove = arg1;
        }

        public override void sectIDChanged(int arg1)
        {
            _sectID = arg1;
        }

        public override void OnIdleState()
        {
            //Debug.Log("AvatarView:OnIdleState");
            base.OnIdleState();
            Animator.Play("Idle");
            Animator.SetFloat("Speed", 0.0f);
        }

        public override void OnDeadState()
        {
            //Debug.Log("AvatarView:OnDeadState");
            Animator.SetTrigger("Death");
        }

        public override void OnWalkState()
        {
            base.OnWalkState();
            Animator.SetFloat("Speed", 1.0f);
        }

        public override void OnRunState()
        {
            base.OnRunState();
            Animator.SetFloat("Speed", 1.0f);
        }

        public override void OnHitState()
        {
            base.OnHitState();
        }

        public override void OnCastSkillState()
        {
            base.OnCastSkillState();
            //.OnCastSkill(Avatar.CurrentSkillName, Avatar.CurrentSkillArgs);
        }

        public override void OnDialogItemsReturnExecuted(OnDialogItemsReturnCommand command)
        {
            Debug.Log("AvatarView:OnDialogItemsReturnExecuted " + command);
            this.Publish(new ShowDialogPanelEvent() { DialogItemsObject = command.DialogItemsObject });
        }

        public override void OnPullStorePropListReturnExecuted(OnPullStorePropListReturnCommand command)
        {
            Debug.Log("AvatarView:OnPullStorePropListReturnExecuted " + command);
            this.Publish(new ShowStorePanelEvent() { StorePropListReturnCommand = command });
        }

        public override void OnJoinSectResultExecuted(OnJoinSectResultCommand command)
        {
            Debug.Log("AvatarView:OnJoinSectResultExecuted " + command);
        }

        public override void magicWeaponListChanged(object arg1)
        {
            //Debug.Log("MainAvatarInfoPanelView:magicWeaponListChanged ");
            //var tmpPropList = ((Dictionary<string, object>)arg1)["values"] as List<object>;
            //_magicWeaponList = new Dictionary<ulong, Prop>();
            //if (tmpPropList != null)
            //{
            //    foreach (var item in tmpPropList)
            //    {
            //        var propObject = (Dictionary<string, object>)item;
            //        var propData = JObject.Parse(propObject["propData"].ToString());
            //        var prop = new Prop();
            //        prop.propUUID = (ulong)propObject["propUUID"];
            //        prop.index = (int)propObject["index"];
            //        prop.propData = propData;
            //        _magicWeaponList.Add(prop.propUUID, prop);
            //        var spawnPool = PoolManager.Pools["MagicWeaponPool"];
            //        var weapon = spawnPool.Spawn(spawnPool.prefabs["weapon_" + propData["id"]]);
            //        weapon.SetParent(_weaponListNode.GetChild(prop.index));
            //        weapon.localScale = Vector3.one;
            //        weapon.localPosition = Vector3.zero;
            //    }
            //}
        }

        public override void gongFaKeyOptionsChanged(string arg1)
        {
            //Debug.Log("AvatarView:gongFaKeyOptionsChanged");
            if (arg1 != null)
                GongFaKeyOptions = JObject.Parse(arg1);
        }
    }
}
