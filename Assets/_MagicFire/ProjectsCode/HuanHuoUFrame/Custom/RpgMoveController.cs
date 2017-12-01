namespace MagicFire.HuanHuoUFrame {
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

        [SerializeField]
        private bool _clientControl = false;
        private bool _canMove = true;
        private CharacterController _mainAvatarController;
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

            Observable.EveryUpdate()
                .Where(evt => { return Input.GetMouseButtonDown(1); })
                .Subscribe(evt =>
                {
                    OnRightMouseButtonDown();
                }).DisposeWith(this);

            Observable.EveryUpdate()
                .Where(evt => { return Input.GetMouseButtonDown(0); })
                .Subscribe(evt => 
                {
                    OnLeftMouseButtonDown();
                }).DisposeWith(this);

            if (_clientControl)
            {
                MainAvatarController.GetComponent<AvatarView>().Avatar.canMoveProperty.ObserveOnMainThread().Subscribe(canMove =>
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
                    MoveVector = Vector3.zero;
                });

                Observable.EveryUpdate()
                    .Subscribe(evt =>
                    {
                        KBEngine.Event.fireIn("updatePlayer", transform.position.x, transform.position.y, transform.position.z, transform.eulerAngles.y, transform.eulerAngles.z);
                    }).DisposeWith(this);

                Observable.EveryFixedUpdate()
                    .Subscribe(evt =>
                    {
                        MainAvatarController.Move(MoveVector);
                    }).DisposeWith(this);
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
                else
                {
                    if (!_clickPointObject.activeInHierarchy)
                    {
                        _clickPointObject.SetActive(true);
                    }
                    _clickPointObject.transform.position = hit.point;
                }

                if (_clientControl)
                {
                    if (_canMove)
                    {
                        KBEngine.Event.fireIn("RequestMove", new object[] { hit.point });
                        //MainAvatarController.transform.DOLookAt(new Vector3(hit.point.x, MainAvatarController.transform.position.y, hit.point.z), 0.0f);
                        MainAvatarController.transform.LookAt(new Vector3(hit.point.x, MainAvatarController.transform.position.y, hit.point.z));
                        MoveVector = MainAvatarController.transform.forward * 0.4f;
                    }
                }
                else
                {
                    KBEngine.Event.fireIn("RequestMove", new object[] { hit.point });
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
