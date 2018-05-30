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
        private int _currentGongFaID;


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
                        SelectGongFa((int)KeyCode.A);
                    if (Input.GetKeyDown(KeyCode.S))//115
                        SelectGongFa((int)KeyCode.S);
                    if (Input.GetKeyDown(KeyCode.D))//100
                        SelectGongFa((int)KeyCode.D);
                    if (Input.GetKeyDown(KeyCode.F))//102
                        SelectGongFa((int)KeyCode.F);
                    if (Input.GetKeyDown(KeyCode.G))//103
                        SelectGongFa((int)KeyCode.G);
                    if (Input.GetKeyDown(KeyCode.H))//104
                        SelectGongFa((int)KeyCode.H);
                    if (Input.GetKeyDown(KeyCode.J))//106
                        SelectGongFa((int)KeyCode.J);
                    if (Input.GetKeyDown(KeyCode.K))//107
                        SelectGongFa((int)KeyCode.K);
                    if (Input.GetKeyDown(KeyCode.L))//108
                        SelectGongFa((int)KeyCode.L);
                    //技能
                    if (Input.GetKeyDown(KeyCode.Q))//113
                        SkillReady(0);
                    if (Input.GetKeyDown(KeyCode.W))//119
                        SkillReady(1);
                    if (Input.GetKeyDown(KeyCode.E))//101
                        SkillReady(2);
                    if (Input.GetKeyDown(KeyCode.R))//114
                        SkillReady(3);
                    if (Input.GetKeyDown(KeyCode.T))//116
                        SkillReady(4);
                    if (Input.GetKeyDown(KeyCode.Y))//121
                        SkillReady(5);
                    if (Input.GetKeyDown(KeyCode.U))//117
                        SkillReady(6);
                    if (Input.GetKeyDown(KeyCode.I))//105
                        SkillReady(7);
                    if (Input.GetKeyDown(KeyCode.O))//111
                        SkillReady(8);
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
            var gongFaID = int.Parse(Avatar.GongFaKeyOptions[keyCode.ToString()].ToString());
            if (gongFaID == 0)
                return;
            _currentGongFaID = gongFaID;
            this.Publish(new OnSelectGongFaEvent(){ GongFaID = gongFaID });
        }

        public void SkillReady(int index)
        {
            CancelReady();
            if (Avatar != null)
            {
                var skillID = _currentGongFaID * 10 + index;
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
