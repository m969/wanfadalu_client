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
                    //功法
                    if (Input.GetKeyDown(KeyCode.A))//97
                        SkillReady((int)KeyCode.A);
                    if (Input.GetKeyDown(KeyCode.S))//115
                        SkillReady((int)KeyCode.S);
                    if (Input.GetKeyDown(KeyCode.D))//100
                        SkillReady((int)KeyCode.D);
                    if (Input.GetKeyDown(KeyCode.F))//102
                        SkillReady((int)KeyCode.F);
                    if (Input.GetKeyDown(KeyCode.G))//103
                        SkillReady((int)KeyCode.G);
                    if (Input.GetKeyDown(KeyCode.H))//104
                        SkillReady((int)KeyCode.H);
                    if (Input.GetKeyDown(KeyCode.J))//106
                        SkillReady((int)KeyCode.J);
                    if (Input.GetKeyDown(KeyCode.K))//107
                        SkillReady((int)KeyCode.K);
                    if (Input.GetKeyDown(KeyCode.L))//108
                        SkillReady((int)KeyCode.L);
                    //技能
                    if (Input.GetKeyDown(KeyCode.Q))//113
                        SkillReady((int)KeyCode.Q);
                    if (Input.GetKeyDown(KeyCode.W))//119
                        SkillReady((int)KeyCode.W);
                    if (Input.GetKeyDown(KeyCode.E))//101
                        SkillReady((int)KeyCode.E);
                    if (Input.GetKeyDown(KeyCode.R))//114
                        SkillReady((int)KeyCode.R);
                    if (Input.GetKeyDown(KeyCode.T))//116
                        SkillReady((int)KeyCode.T);
                    if (Input.GetKeyDown(KeyCode.Y))//121
                        SkillReady((int)KeyCode.Y);
                    if (Input.GetKeyDown(KeyCode.U))//117
                        SkillReady((int)KeyCode.U);
                    if (Input.GetKeyDown(KeyCode.I))//105
                        SkillReady((int)KeyCode.I);
                    if (Input.GetKeyDown(KeyCode.O))//111
                        SkillReady((int)KeyCode.O);
                    CurrentSkillState.Run();
                });
            Observable.EveryUpdate()
                .Where(evt => { return Input.GetMouseButtonDown(1); })
                .Subscribe(evt =>
                {
                    this.CancelReady();
                });
        }

        private void SelectGongFa(int keyCode)
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
