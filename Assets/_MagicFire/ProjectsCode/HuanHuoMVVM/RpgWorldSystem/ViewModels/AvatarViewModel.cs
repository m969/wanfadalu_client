namespace MagicFire.HuanHuoMVVM{
    using HuanHuoMVVM;
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
    
    
    public partial class AvatarViewModel : AvatarViewModelBase {
        //private List<object> _avatarBag = new List<object>();

        //public List<object> AvatarBag
        //{
        //    get
        //    {
        //        if (_avatarBag.Count == 0)
        //        {
        //            object avatarBagObject = getDefinedProperty("avatarBag");
        //            _avatarBag = ((Dictionary<string, object>)avatarBagObject)["values"] as List<object>;
        //            return _avatarBag;
        //        }
        //        return _avatarBag;
        //    }
        //}

        public override void __init__()
        {
            base.__init__();
            if (isPlayer())
            {
                KBEngine.Event.registerIn("updatePlayer", this, "updatePlayer");

                KBEngine.Event.registerIn("RequestMove", this, "RequestMove");
                KBEngine.Event.registerIn("StopMove", this, "StopMove");

                KBEngine.Event.registerIn("RequestDialog", this, "RequestDialog");

                KBEngine.Event.registerIn("RequestBuyGoods", this, "RequestBuyGoods");

                KBEngine.Event.registerIn("OnLeaveSpaceClientInputInValid", this, "OnLeaveSpaceClientInputInValid");

                KBEngine.Event.registerIn("SendChatMessage", this, "SendChatMessage");
                KBEngine.Event.registerIn("SendVoiceSample", this, "SendVoiceSample");

                KBEngine.Event.registerIn("FindFriends", this, "FindFriends");
                KBEngine.Event.registerIn("AddFriends", this, "AddFriends");
                KBEngine.Event.registerIn("DeleteFriends", this, "DeleteFriends");
            }
        }

        //public void set_avatarBag(object old)
        //{
        //    object avatarBagObject = getDefinedProperty("avatarBag");
        //    _avatarBag = ((Dictionary<string, object>)avatarBagObject)["values"] as List<object>;
        //}

        #region 暴露给服务端调用的方法代码块
        public void OnMainAvatarEnterSpace(int spaceId, string spaceName)
        {
            KBEngine.Event.fireOut("OnMainAvatarEnterSpace", spaceId, spaceName);
        }

        public void OnMainAvatarLeaveSpace()
        {
            KBEngine.Event.fireOut("OnMainAvatarLeaveSpace");
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

        public void OnLeaveSpaceClientInputInValid()
        {
            cellCall("onLeaveSpaceClientInputInValid");
        }

        public void RequestMove(Vector3 point)
        {
            cellCall("requestMove", new object[] { point });
        }

        public void StopMove()
        {
            cellCall("stopMove");
        }

        public void RequestDialog(uint spaceId, string npcName)
        {
            cellCall("requestDialog", new object[] { spaceId, npcName });
        }

        public void RequestBuyGoods(uint spaceId, string npcName, int goodsId)
        {
            cellCall("requestBuyGoods", new object[] { spaceId, npcName, goodsId });
        }

        public void RequestCastSkillByName(string skillName, string argsString)
        {
            cellCall("requestCastSkill", new object[] { skillName, argsString });
        }

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
