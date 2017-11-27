namespace MagicFire.HuanHuoUFrame {
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
        private GameObject ClickPointObject;

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

            Observable.EveryUpdate()
                .Subscribe(evt =>
                {

                }).DisposeWith(this);
        }

        private void OnRightMouseButtonDown()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            var layerMask = 1 << LayerMask.NameToLayer("Terrian");
            if (Physics.Raycast(ray, out hit, RayCastHitDist, layerMask))
            {
                if (ClickPointObject == null)
                {
                    var viewPool = PoolManager.Pools["AuxiliaryPool"];
                    ClickPointObject = viewPool.Spawn("RpgMovePointMarker").gameObject;
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
