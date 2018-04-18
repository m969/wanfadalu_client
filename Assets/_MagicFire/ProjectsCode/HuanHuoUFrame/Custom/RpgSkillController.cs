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
                return;
            Observable.EveryUpdate()
                .Subscribe(evt =>
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                        SkillReady((int)KeyCode.Q);
                    if (Input.GetKeyDown(KeyCode.W))
                        SkillReady((int)KeyCode.W);
                    if (Input.GetKeyDown(KeyCode.E))
                        SkillReady((int)KeyCode.E);
                    if (Input.GetKeyDown(KeyCode.A))
                        SkillReady((int)KeyCode.A);
                    if (Input.GetKeyDown(KeyCode.S))
                        SkillReady((int)KeyCode.S);
                    if (Input.GetKeyDown(KeyCode.D))
                        SkillReady((int)KeyCode.D);
                    if (Input.GetKeyDown(KeyCode.Z))
                        SkillReady((int)KeyCode.Z);
                    if (Input.GetKeyDown(KeyCode.X))
                        SkillReady((int)KeyCode.X);
                    if (Input.GetKeyDown(KeyCode.C))
                        SkillReady((int)KeyCode.C);
                    CurrentSkillState.Run();
                });
            Observable.EveryUpdate()
                .Where(evt => { return Input.GetMouseButtonDown(1); })
                .Subscribe(evt =>
                {
                    this.CancelReady();
                });
        }

        public void SkillReady(int keyCode)
        {
            CancelReady();
            if (Avatar != null)
            {
                var skillID = int.Parse(Avatar.SkillKeyOptions[keyCode.ToString()].ToString());
                if (skillID == 0)
                    return;
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
