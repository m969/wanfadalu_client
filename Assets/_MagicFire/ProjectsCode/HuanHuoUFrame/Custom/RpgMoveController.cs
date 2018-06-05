﻿namespace MagicFire.HuanHuoUFrame {
    using DG.Tweening;
    using PathologicalGames;
    using System;
    using uFrame.ECS.UnityUtilities;
    using uFrame.Kernel;
    using UniRx;
    using UniRx.Triggers;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public partial class RpgMoveController
    {
        public const int RayCastHitDist = 400;

        private bool _canMove = true;
        private CharacterController _mainAvatarController;
        private AvatarView _mainAvatarView;
        private GameObject _clickPointObject;
        private Vector3 _moveVector = new Vector3(0, 0, 0);


        public CharacterController MainAvatarController
        {
            get
            {
                return _mainAvatarController;
            }
            set
            {
                _mainAvatarController = value;
            }
        }

        public AvatarView MainAvatarView
        {
            get
            {
                return _mainAvatarView;
            }
            set
            {
                _mainAvatarView = value;
            }
        }

        public Vector3 MoveVector
        {
            get
            {
                return _moveVector;
            }
            set
            {
                _moveVector = value;
            }
        }


        protected override void Start()
        {
            base.Start();

            //Observable.EveryUpdate()
            //    .Where(evt => { return Input.GetMouseButtonDown(1); })
            //    .Subscribe(evt =>
            //    {
            //        OnRightMouseButtonDown();
            //    }).DisposeWith(this);

            this.OnEvent<StartMoveEvent>().Subscribe(StartMoveEvent);

            //Observable.EveryUpdate()
            //    .Where(evt => { return Input.GetMouseButtonDown(0); })
            //    .Subscribe(evt => 
            //    {
            //        OnLeftMouseButtonDown();
            //    }).DisposeWith(this);

            if (MainAvatarView.ClientControl)
            {
                MainAvatarView.Avatar.canMoveProperty.ObserveOnMainThread().Subscribe(canMove =>
                {
                    Debug.Log("RpgMoveController canMoveProperty");
                    if (canMove == 1)
                    {
                        _canMove = true;
                    }
                    else
                    {
                        _canMove = false;
                        MoveVector = Vector3.zero;
                    }
                }).DisposeWith(this);

                this.OnEvent<StopMoveEvent>().Subscribe(evt =>
                {
                    MainAvatarView.Avatar.Execute(new RequestStopMoveCommand());
                    MoveVector = Vector3.zero;
                });

                Observable.EveryUpdate()
                    .Subscribe(evt =>
                    {
                        var p = MainAvatarController.transform.position;
                        var e = MainAvatarController.transform.eulerAngles;
                        //Debug.Log("updatePlayer: " + e);
                        KBEngine.Event.fireIn("updatePlayer", p.x, p.y, p.z, e.y, e.z);
                    }).DisposeWith(this);

                Observable.EveryFixedUpdate()
                    .Subscribe(evt =>
                    {
                        if (MainAvatarController.transform.position.y < -4)
                            MainAvatarController.transform.position = new Vector3(MainAvatarController.transform.position.x, 22, MainAvatarController.transform.position.z);

                        if (MainAvatarController.transform.eulerAngles.x != 0 || MainAvatarController.transform.eulerAngles.z != 0)
                            MainAvatarController.transform.eulerAngles = new Vector3(0, MainAvatarController.transform.eulerAngles.y, 0);

                        if (MainAvatarController.isGrounded)
                            MoveVector = new Vector3(MoveVector.x, 0, MoveVector.z);
                        else
                            MoveVector = new Vector3(MoveVector.x, -4.0f, MoveVector.z);
                        MainAvatarController.Move(MoveVector);
                    }).DisposeWith(this);
            }
        }

        private void StartMoveEvent(StartMoveEvent evt)
        {
            if (_clickPointObject == null)
            {
                var viewPool = PoolManager.Pools["AuxiliaryPool"];
                _clickPointObject = viewPool.Spawn("RpgMovePointMarker").gameObject;
            }
            if (!_clickPointObject.activeInHierarchy)
            {
                _clickPointObject.SetActive(true);
            }
            _clickPointObject.transform.position = evt.Point;

            if (MainAvatarView.ClientControl)
            {
                if (_canMove)
                {
                    MainAvatarView.Avatar.Execute(new RequestMoveCommand() { Point = evt.Point });
                    //KBEngine.Event.fireIn("RequestMove", new object[] { hit.point });
                    //MainAvatarController.transform.DOLookAt(new Vector3(hit.point.x, MainAvatarController.transform.position.y, hit.point.z), 0.0f);
                    MainAvatarController.transform.LookAt(new Vector3(evt.Point.x, MainAvatarController.transform.position.y, evt.Point.z));
                    MoveVector = MainAvatarController.transform.forward * 0.2f * MainAvatarView.Speed;
                }
            }
            else
            {
                MainAvatarView.Avatar.Execute(new RequestMoveCommand() { Point = evt.Point });
            }
        }

        private void OnRightMouseButtonDown()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            var layerMask = 1 << LayerMask.NameToLayer("Terrian");
            if (Physics.Raycast(ray, out hit, RayCastHitDist, layerMask))
            {
                if (_clickPointObject == null)
                {
                    var viewPool = PoolManager.Pools["AuxiliaryPool"];
                    _clickPointObject = viewPool.Spawn("RpgMovePointMarker").gameObject;
                }
                if (!_clickPointObject.activeInHierarchy)
                {
                    _clickPointObject.SetActive(true);
                }
                _clickPointObject.transform.position = hit.point;

                if (MainAvatarView.ClientControl)
                {
                    if (_canMove)
                    {
                        MainAvatarView.Avatar.Execute(new RequestMoveCommand() { Point = hit.point });
                        //KBEngine.Event.fireIn("RequestMove", new object[] { hit.point });
                        //MainAvatarController.transform.DOLookAt(new Vector3(hit.point.x, MainAvatarController.transform.position.y, hit.point.z), 0.0f);
                        MainAvatarController.transform.LookAt(new Vector3(hit.point.x, MainAvatarController.transform.position.y, hit.point.z));
                        MoveVector = MainAvatarController.transform.forward * 0.2f * MainAvatarView.Speed;
                    }
                }
                else
                {
                    MainAvatarView.Avatar.Execute(new RequestMoveCommand() { Point = hit.point });
                }
            }
        }

        private void OnLeftMouseButtonDown()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                
                if (hit.collider.tag == "Npc")
                {
                    if (Mathf.Abs(Vector3.Distance(transform.position, hit.collider.transform.position)) < 5)
                    {

                    }
                }
                if (hit.collider.tag == "Arena")
                {
                    //Debug.Log("" + Mathf.Abs(Vector3.Distance(transform.position, hit.collider.transform.position)));
                    if (Mathf.Abs(Vector3.Distance(transform.position, hit.collider.transform.position)) < 5)
                    {
                        Debug.Log("hit.collider.tag == Arena");

                        //var avatarViewPool = PoolManager.Pools["AvatarViewPool"];
                        //avatarViewPool.Spawn(avatarViewPool.prefabs["ArenaApplyPanel"]);
                    }
                }
            }
        }
    }
}
