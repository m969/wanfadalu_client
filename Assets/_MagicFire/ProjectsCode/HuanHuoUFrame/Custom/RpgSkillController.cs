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

    public partial class RpgSkillController
    {
        public AvatarView Avatar { get; set; }
        private SkillState CurrentSkillState { get; set; }
        private SkillReadyState _skillReadyState;
        private SkillEmptyState _skillEmptyState;


        public void Init(AvatarView avatar)
        {
            Avatar = avatar;
            _skillEmptyState = new SkillEmptyState(Avatar);
            _skillReadyState = new SkillReadyState(Avatar);
            CurrentSkillState = _skillEmptyState;
            if (!Avatar.Avatar.isPlayer())
            {
                return;
            }

            Observable.EveryUpdate().Where(x => { return Input.anyKeyDown; }).Subscribe(x => 
            {
                //Debug.Log(Input.);
                //Event e = Event.current;
                //Event.PopEvent(e);
                //if (e.isKey)
                //{
                //    Debug.Log(e.keyCode);
                //}
            });

            Observable.EveryUpdate()
                .Subscribe(evt =>
                {
                    CurrentSkillState.Run();

                    if (Input.GetKeyDown(KeyCode.Q))
                        SkillReady(1001 * 10 + 0);
                    if (Input.GetKeyDown(KeyCode.W))
                        SkillReady(1003 * 10 + 0);
                    if (Input.GetKeyDown(KeyCode.E))
                        SkillReady(1002 * 10 + 0);
                    if (Input.GetKeyDown(KeyCode.R))
                        GetSkillRef(1001 * 10 + 1).Conjure();
                });

            Observable.EveryUpdate()
                .Where(evt => { return Input.GetMouseButtonDown(1); })
                .Subscribe(evt =>
                {
                    this.CancelReady();
                });

            Observable.EveryUpdate()
                .Where(evt => { return Input.GetMouseButtonDown(0); })
                .Subscribe(evt =>
                {
                    //this.CancelReady();
                });
        }

        public void SkillReady(int skillID)
        {
            CancelReady();
            if (Avatar != null)
            {
                Skill skill;
                if (Avatar.SkillMap.TryGetValue(skillID, out skill))
                {
                    _skillReadyState.CurrentReadySkill = skill;
                    CurrentSkillState = _skillReadyState;
                }
                else
                {
                    Debug.LogError("RpgSkillController:SkillReady Error! Not Found Skill " + skillID);
                }
            }
        }

        public void SkillReady(Skill skill)
        {
            CancelReady();
            _skillReadyState.CurrentReadySkill = skill;
            CurrentSkillState = _skillReadyState;
        }

        public void CancelReady()
        {
            if (CurrentSkillState == _skillEmptyState)
                return;
            if (CurrentSkillState == _skillReadyState)
                _skillReadyState.CurrentReadySkill.CancelReady();
            CurrentSkillState = _skillEmptyState;
        }

        public Skill GetSkillRef(int skillID)
        {
            Skill skill = null;
            Avatar.SkillMap.TryGetValue(skillID, out skill);
            return skill;
        }

        private class SkillState
        {
            protected readonly AvatarView Owner;

            protected SkillState(AvatarView owner)
            {
                Owner = owner;
            }

            public virtual void Run()
            {

            }

            public virtual void FixedRun()
            {

            }
        }

        private class SkillEmptyState : SkillState
        {
            public SkillEmptyState(AvatarView owner) : base(owner)
            {
            }
        }

        private class SkillReadyState : SkillState
        {
            public Skill CurrentReadySkill;

            public SkillReadyState(AvatarView owner) : base(owner)
            {
            }

            public override void Run()
            {
                base.Run();
                CurrentReadySkill.Ready(Owner);
            }
        }

    }
}
