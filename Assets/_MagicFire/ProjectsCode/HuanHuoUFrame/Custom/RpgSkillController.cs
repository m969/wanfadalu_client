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
        private readonly Dictionary<string, Skill> _skillMap = new Dictionary<string, Skill>();
        private SkillState CurrentSkillState { get; set; }
        private SkillReadyState _skillReadyState;
        private SkillEmptyState _skillEmptyState;


        public void Init(AvatarView avatar)
        {
            Avatar = avatar;
            //_skillMap.Add(1, new SkillQ(Avatar));
            //_skillMap.Add(2, new SkillW(Avatar));
            //_skillMap.Add(3, new SkillE(Avatar));
            //_skillMap.Add(4, new GongKan(Avatar));

            //AddSkill(new SkillQ(Avatar));
            //AddSkill(new SkillW(Avatar));
            //AddSkill(new SkillE(Avatar));
            //AddSkill(new GongKan(Avatar));

            _skillEmptyState = new SkillEmptyState(Avatar);
            _skillReadyState = new SkillReadyState(Avatar);
            CurrentSkillState = _skillEmptyState;

            Observable.EveryUpdate()
                .Subscribe(evt =>
                {
                    CurrentSkillState.Run();

                    if (Input.GetKeyDown(KeyCode.Q))
                        SkillReady(1001, 0);
                    if (Input.GetKeyDown(KeyCode.W))
                        SkillReady(1003, 0);
                    if (Input.GetKeyDown(KeyCode.E))
                        SkillReady(1002, 0);
                    if (Input.GetKeyDown(KeyCode.R))
                        GetSkillRef(1001, 1).Conjure();
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

        public void AddSkill(Skill skill)
        {
            skill.SkillController = this;
            _skillMap.Add(skill.GongFaID + ":" + skill.SkillIndex, skill);
        }

        public void SkillReady(int gongfaID, int skillIndex)
        {
            CancelReady();
            if (Avatar != null)
            {
                Skill skill;
                Avatar.SkillMap.TryGetValue(gongfaID + ":" + skillIndex, out skill);
                _skillReadyState.CurrentReadySkill = skill;
                CurrentSkillState = _skillReadyState;
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

        public Skill GetSkillRef(int gongfaID, int skillIndex)
        {
            Skill skill = null;
            Avatar.SkillMap.TryGetValue(gongfaID + ":" + skillIndex, out skill);
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
