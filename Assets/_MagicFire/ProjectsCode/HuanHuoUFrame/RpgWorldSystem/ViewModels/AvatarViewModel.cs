namespace MagicFire.HuanHuoUFrame{
    using HuanHuoUFrame;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    
    
    public partial class AvatarViewModel : AvatarViewModelBase
    {

        public string CurrentSkillName;
        public string CurrentSkillArgs;

        public static AvatarViewModel MainAvatar
        {
            get { return KBEngine.KBEngineApp.app.player() as AvatarViewModel; }
        }


        public override void Bind()
        {
            base.Bind();
            //this.avatarStateProperty.CastSkillState.OnCompleted();
        }

        public override void __init__()
        {
            base.__init__();
            if (isPlayer())
            {
                //this.Aggregator.GetEvent<KbeRemoteCallEvent>().Subscribe(evt =>
                //{
                //    Debug.Log("AvatarViewModel:GetEvent<KbeRemoteCallEvent>()");
                //    Debug.Log(evt.RemoteCallName);
                //    if (evt.CallType == CallType.Base)
                //        baseCall(evt.RemoteCallName, evt.Params);
                //    else
                //        cellCall(evt.RemoteCallName, evt.Params);
                //});
                KBEngine.Event.registerIn("updatePlayer", this, "updatePlayer");
                //KBEngine.Event.registerIn("RequestMove", this, "RequestMove");
                //KBEngine.Event.registerIn("StopMove", this, "StopMove");
                KBEngine.Event.registerIn("RequestDialog", this, "RequestDialog");
                KBEngine.Event.registerIn("RequestBuyGoods", this, "RequestBuyGoods");
                //KBEngine.Event.registerIn("RequestCastSkillByName", this, "RequestCastSkillByName");
                KBEngine.Event.registerIn("OnLeaveSpaceClientInputInValid", this, "OnLeaveSpaceClientInputInValid");
                KBEngine.Event.registerIn("SendChatMessage", this, "SendChatMessage");
                KBEngine.Event.registerIn("SendVoiceSample", this, "SendVoiceSample");
                KBEngine.Event.registerIn("FindFriends", this, "FindFriends");
                KBEngine.Event.registerIn("AddFriends", this, "AddFriends");
                KBEngine.Event.registerIn("DeleteFriends", this, "DeleteFriends");
            }
        }

        #region 暴露给服务端调用的方法代码块

        public override void onMainAvatarEnterSpace_(int SpaceId, string SpaceName)
        {
            base.onMainAvatarEnterSpace_(SpaceId, SpaceName);
            this.Aggregator.Publish(new OnMainAvatarEnterSpaceEvent(SpaceId, SpaceName));
        }

        public override void onMainAvatarLeaveSpace_()
        {
            base.onMainAvatarLeaveSpace_();
            this.Aggregator.Publish(new OnMainAvatarLeaveSpaceEvent());
        }

        public override void DoMove_(Vector3 Point)
        {
            base.DoMove_(Point);
            this.avatarStateProperty.Run.OnNext(true);
        }

        public override void OnStopMove_()
        {
            base.OnStopMove_();
            this.avatarStateProperty.Stand.OnNext(true);
        }

        public override void OnSkillStartSing_(float singTime)
        {
            base.OnSkillStartSing_(singTime);
            this.avatarStateProperty.Stand.OnNext(true);
        }

        public override void OnSkillStartCast_(string skillName, string argsString, float castTime)
        {
            base.OnSkillStartCast_(skillName, argsString, castTime);
            CurrentSkillName = skillName;
            CurrentSkillArgs = argsString;
            this.avatarStateProperty.Transition("CastSkill");
        }

        public override void OnSkillEndCast_(string argsString, string skillName)
        {
            base.OnSkillEndCast_(argsString, skillName);
            this.avatarStateProperty.Stand.OnNext(true);
        }

        public void DoDialog(System.String npcName, System.String dialog)
        {
            KBEngine.Event.fireOut("DoDialog", new object[] { this, npcName, dialog });
        }

        public void BuyResult(int result)
        {
            KBEngine.Event.fireOut("BuyResult", new object[] { this, System.Convert.ToBoolean(result) });
        }

        public void DoStore(Dictionary<string, object> storeGoodsIdListObject)
        {
            List<System.Int32> storeGoodsIdList = (List<System.Int32>)storeGoodsIdListObject["values"];
            foreach (var item in storeGoodsIdList)
            {
                Debug.Log(item);
            }
            KBEngine.Event.fireOut("DoStore", new object[] { this });
        }

        #endregion

        #region 处理u3d表现层的抛入事件
        // ReSharper disable once InconsistentNaming
        public virtual void updatePlayer(float x, float y, float z, float dir_y, float dir_z)
        {
            position.x = x;
            position.y = y;
            position.z = z;

            direction.z = dir_y;
            direction.y = dir_z;
        }

        public override void Execute(RequestEnterArenaCommand argument)
        {
            base.Execute(argument);
            //this.cellCall("requestEnterArena", new object[] { argument.ArenaID });
        }

        public void OnLeaveSpaceClientInputInValid()
        {
            cellCall("onLeaveSpaceClientInputInValid");
        }

        public override void Execute(RequestMoveCommand argument)
        {
            base.Execute(argument);
            cellCall("requestMove", new object[] { argument.Point });
        }

        public override void Execute(RequestStopMoveCommand argument)
        {
            base.Execute(argument);
            cellCall("requestStopMove");
        }

        //public void RequestMove(Vector3 point)
        //{
        //    cellCall("requestMove", new object[] { point });
        //}

        //public void StopMove()
        //{
        //    cellCall("stopMove");
        //}

        public void RequestDialog(uint spaceId, string npcName)
        {
            cellCall("requestDialog", new object[] { spaceId, npcName });
        }

        public void RequestBuyGoods(uint spaceId, string npcName, int goodsId)
        {
            cellCall("requestBuyGoods", new object[] { spaceId, npcName, goodsId });
        }

        //public void RequestCastSkillByName(string skillName, string argsString)
        //{
        //    cellCall("requestCastSkill", new object[] { skillName, argsString });
        //}

        public void SendChatMessage(string message)
        {
            cellCall("sendChatMessage", new object[] { getDefinedProperty("entityName"), message });
        }

        public void SendVoiceSample(byte[] data, int length, ulong packetId)
        {
            Debug.Log("SendVoiceSample");
            Debug.Log("length " + length);
            cellCall("sendVoiceSample", data, length, packetId);
        }

        public void FindFriends()
        {
            cellCall("findFriends");
        }

        public void AddFriends(string goldxFriendsName)
        {
            cellCall("addFriends", new object[] { goldxFriendsName });
        }

        public void DeleteFriends(string goldxFriendsName)
        {
            cellCall("deleteFriends", new object[] { goldxFriendsName });
        }

        public void ShowAllFriends()
        {
            cellCall("showAllFriends");
        }

        #endregion
    }
}
