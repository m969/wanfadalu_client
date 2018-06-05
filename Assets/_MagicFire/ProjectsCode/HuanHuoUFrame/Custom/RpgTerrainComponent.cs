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
    using UnityEngine.EventSystems;


    public partial class RpgTerrainComponent : uFrame.ECS.Components.EcsComponent, IPointerDownHandler
    {
        protected override void Start()
        {
            base.Start();
            //this.OnMouseEvent(MouseEventType.OnMouseDown).Subscribe(evt =>
            //{
            //    Debug.Log("NpcView:OnMouseDown");
            //    KBEngine.KBEngineApp.app.player().cellCall("requestDialog", Npc.id);
            //}).DisposeWith(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("RpgTerrainComponent:OnPointerDown " + eventData.button);
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                var layerMask = 1 << LayerMask.NameToLayer("Terrian");
                if (Physics.Raycast(ray, out hit, RpgMoveController.RayCastHitDist, layerMask))
                {
                    this.Publish(new StartMoveEvent() { Point = hit.point });
                }
            }
        }
    }
}
