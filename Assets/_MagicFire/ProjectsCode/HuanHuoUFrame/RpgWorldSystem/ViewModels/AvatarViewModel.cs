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
                //KBEngine.Event.registerIn("RequestDialog", this, "RequestDialog");
                //KBEngine.Event.registerIn("RequestBuyGoods", this, "RequestBuyGoods");
                //KBEngine.Event.registerIn("RequestCastSkillByName", this, "RequestCastSkillByName");
                KBEngine.Event.registerIn("OnLeaveSpaceClientInputInValid", this, "OnLeaveSpaceClientInputInValid");
                //KBEngine.Event.registerIn("SendChatMessage", this, "SendChatMessage");
                //KBEngine.Event.registerIn("SendVoiceSample", this, "SendVoiceSample");
                //KBEngine.Event.registerIn("FindFriends", this, "FindFriends");
                //KBEngine.Event.registerIn("AddFriends", this, "AddFriends");
                //KBEngine.Event.registerIn("DeleteFriends", this, "DeleteFriends");
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

        public override void OnDead_()
        {
            base.OnDead_();
            this.avatarStateProperty.Dead.OnNext(true);
        }

        public override void OnRespawn_(Vector3 RespawnPosition)
        {
            base.OnRespawn_(RespawnPosition);
            this.avatarStateProperty.Stand.OnNext(true);
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

        public override void OnSkillStartCast_(int skillID, string argsString, float castTime)
        {
            base.OnSkillStartCast_(skillID, argsString, castTime);
            this.avatarStateProperty.Transition("CastSkill");
        }

        public override void OnSkillEndCast_(int skillID, string argsString)
        {
            base.OnSkillEndCast_(skillID, argsString);
            this.avatarStateProperty.Stand.OnNext(true);
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

        public void OnLeaveSpaceClientInputInValid()
        {
            cellCall("onLeaveSpaceClientInputInValid");
        }
        #endregion
    }
}
