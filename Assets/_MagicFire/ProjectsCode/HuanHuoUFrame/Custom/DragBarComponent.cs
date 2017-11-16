using System;
using uFrame.ECS.UnityUtilities;
using uFrame.Kernel;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MagicFire.HuanHuoUFrame {
    public partial class DragBarComponent
    {
        private Vector3 _lastPosition;
        private void OnEnable()
        {
            this.OnEvent<BeginDragDispatcher>().Where(evt => evt.Entity == this.Entity).Subscribe(evt =>
            {
                Vector3 currentPosition;

                RectTransformUtility.ScreenPointToWorldPointInRectangle(MoveObject.GetComponent<RectTransform>(),
                    evt.PointerEventData.position, evt.PointerEventData.pressEventCamera, out currentPosition);

                var v = GameObject.Find("MasterCanvas").GetComponent<RectTransform>().rect.size / 2;
                _lastPosition = currentPosition - new Vector3(v.x, v.y) - MoveObject.transform.localPosition;
            }).DisposeWith(this);

            this.OnEvent<DragDispatcher>().Where(evt => evt.Entity == this.Entity).Subscribe(evt =>
            {
                Vector3 currentPosition;

                RectTransformUtility.ScreenPointToWorldPointInRectangle(MoveObject.GetComponent<RectTransform>(),
                    evt.PointerEventData.position, evt.PointerEventData.pressEventCamera, out currentPosition);

                var point = MoveObject.transform.localPosition;
                var v = GameObject.Find("MasterCanvas").GetComponent<RectTransform>().rect.size / 2;
                MoveObject.transform.localPosition =  currentPosition - new Vector3(v.x, v.y) - _lastPosition;

            }).DisposeWith(this);
        }

    }
}
