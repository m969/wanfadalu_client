using System;
using DG.Tweening;
using MagicFire.Common;
using VoiceChat.Base;
using VoiceChat.Behaviour;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;

    public class AvatarPanelView : CombatEntityPanelView
    {
        private VoiceChatPlayer _voiceChatPlayer;

        protected override void Start()
        {
            base.Start();
            gameObject.AddComponent<AudioSource>();
            _voiceChatPlayer = gameObject.AddComponent<VoiceChatPlayer>();
        }

        protected override void FixedUpdate()
        {
            if (Model == null)
                return;
            if (((KBEngine.Model)Model).isPlayer() == true)
            {
                var entityObj = ((KBEngine.Model) Model).renderObj as GameObject;
                if (entityObj != null)
                {
                    var v = Camera.main.WorldToScreenPoint(entityObj.transform.position);
                    transform.DOMove(new Vector3(v.x, v.y, 0), 0f);
                }
            }
            else
            {
                base.FixedUpdate();
            }
        }

        public override void InitializeView(IModel model)
        {
            base.InitializeView(model);
            Model.SubscribePropertyUpdate(KBEngine.Avatar.SuperPowerSystem.SP, Sp_Up);
            Model.SubscribeMethodCall(KBEngine.Avatar.ChatChannelSystem.ReciveVoiceSample, OnReciveVoiceSample);
        }

        private void Sp_Up(object old)
        {
            var val = (int)((KBEngine.Model) Model).getDefinedProperty(KBEngine.Avatar.SuperPowerSystem.SP);
            if (val != (int)old)
            {
                
            }
        }

        private void OnReciveVoiceSample(object[] args)
        {
            var packet = new VoiceChatPacket
            {
                Data = (byte[]) args[0],
                Length = (int) args[1],
                PacketId = (ulong) args[2]
            };

            Debug.Log("OnReciveVoiceSample");
            Debug.Log(packet.Data);
            Debug.Log(packet.Length);
            Debug.Log(packet.PacketId);

            _voiceChatPlayer.OnNewSample(packet);
        }
    }

}
