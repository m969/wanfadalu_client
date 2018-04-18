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
    using UniRx.Triggers;
    using uFrame.ECS.UnityUtilities;


    public partial class RpgInteractiveComponent
    {
        protected override void Start()
        {
            base.Start();

            this.OnMouseDownAsObservable().Subscribe(evt =>
            {
                Debug.Log("RpgInteractiveComponent:OnMouseDownAsObservable");
                //this.Publish<KbeRemoteCallEvent>(new KbeRemoteCallEvent()
                //{
                //    RemoteCallName = this.RemoteCallName,
                //    CallType = this.CallType,
                //    Params = new object[] { }
                //});
                //this.Publish<ResponseEvent>(new ResponseEvent()
                //{
                //    RpgInteractiveComponent = this
                //});
                //if (tag == "Npc")
                //{
                //    var npcView = GetComponent<NpcView>();
                //    if (npcView.Npc.npcType == 1)
                //        this.Publish(new ShowDialogPanelEvent());
                //    else
                //        this.Publish(new ShowAvatarBagEvent());
                //}
            });
        }
    }
}
