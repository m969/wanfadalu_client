namespace MagicFire.HuanHuoUFrame {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.ECS;
    using uFrame.ECS.Components;
    using uFrame.Json;
    using UniRx;
    using UnityEngine;
    using DG.Tweening;

    public partial class RpgLookAtPlayerComponent
    {
        public Transform Target;

        [SerializeField]
        private GameObject _camera;
        private Vector2 _startPoint;
        private float _startAngle;
        private bool _hasDown;


        protected override void Start()
        {
            base.Start();
            _hasDown = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _hasDown = true;
                _startPoint = Input.mousePosition;
                _startAngle = transform.localEulerAngles.y;
            }
            if (Input.GetMouseButtonUp(2))
            {
                _hasDown = false;
            }
            if (_hasDown)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _startAngle + (Input.mousePosition.x - _startPoint.x) * 0.5f, 0);
            }
            var scrollValue = Input.GetAxis("Mouse ScrollWheel");
            var p = _camera.transform.localPosition;
            _camera.transform.localPosition = new Vector3(p.x, p.y + scrollValue * -10, p.z);
        }

        private void FixedUpdate()
        {
            transform.DOMove(Target.position, 0.8f);
        }
    }
}
