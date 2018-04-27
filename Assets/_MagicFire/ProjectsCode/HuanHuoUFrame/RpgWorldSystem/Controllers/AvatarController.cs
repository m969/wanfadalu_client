namespace MagicFire.HuanHuoUFrame{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Newtonsoft.Json.Linq;
    
    
    public class AvatarController : AvatarControllerBase {
        
        public override void InitializeAvatar(AvatarViewModel viewModel) {
            base.InitializeAvatar(viewModel);
            // This is called when a AvatarViewModel is created
            Debug.Log("AvatarController:InitializeAvatar");
        }

        public override void RequestMove(MotionSystemViewModel viewModel, RequestMoveCommand arg)
        {
            //Debug.Log("AvatarController:RequestMove");
            viewModel.cellCall("requestMove", arg.Point);
        }

        public override void RequestStopMove(MotionSystemViewModel viewModel, RequestStopMoveCommand arg)
        {
            //Debug.Log("AvatarController:RequestStopMove");
            viewModel.cellCall("requestStopMove");
        }

        public override void RequestDialog(AvatarViewModel viewModel, RequestDialogCommand arg)
        {
            Debug.Log("AvatarController:RequestDialog");
            viewModel.cellCall("requestDialog", arg.NpcID);
        }

        public override void SelectDialogItem(AvatarViewModel viewModel, SelectDialogItemCommand arg)
        {
            Debug.Log("AvatarController:SelectDialogItem");
            viewModel.cellCall("selectDialogItem", arg.DialogID);
        }

        public override void RequestTargetItemList(AvatarViewModel viewModel, RequestTargetItemListCommand arg)
        {
            Debug.Log("AvatarController:RequestTargetItemList");
            //viewModel.cellCall("requestTargetItemList");
        }

        public override void RequestForge(AvatarViewModel viewModel, RequestForgeCommand arg)
        {
            Debug.Log("AvatarController:RequestForge");
            //viewModel.cellCall("requestForge");
        }
    }
}
