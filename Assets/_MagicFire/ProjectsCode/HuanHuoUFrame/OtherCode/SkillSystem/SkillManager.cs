namespace MagicFire.HuanHuoUFrame
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using Mmorpg;

    public class SkillManager
    {
        public AvatarView Owner { get; set; }
        private readonly Dictionary<int, Skill> _skillPool = new Dictionary<int, Skill>();
        private readonly Dictionary<string, Skill> _skillDict = new Dictionary<string, Skill>();
        private SkillState CurrentSkillState { get; set; }
        private SkillReadyState _skillReadyState;
        private SkillEmptyState _skillEmptyState;

        public void Init()
        {
            _skillPool.Add(1, new SkillQ(Owner));
            _skillPool.Add(2, new SkillW(Owner));
            _skillPool.Add(3, new SkillE(Owner));
            _skillPool.Add(4, new GongKan(Owner));

            AddSkill(new SkillQ(Owner));
            AddSkill(new SkillW(Owner));
            AddSkill(new SkillE(Owner));
            AddSkill(new GongKan(Owner));

            _skillEmptyState = new SkillEmptyState(Owner);
            _skillReadyState = new SkillReadyState(Owner);
            CurrentSkillState = _skillEmptyState;
        }

        public void AddSkill(Skill skill)
        {
            _skillDict.Add(skill.SkillName, skill);
        }

        public void Update()
        {
            CurrentSkillState.Run();
        }

        public void SkillReady(int skillId)
        {
            CancelReady();
            if (Owner != null)
            {
                Skill skill;
                _skillPool.TryGetValue(skillId, out skill);
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
            {
                return;
            }
            if (CurrentSkillState == _skillReadyState)
            {
                _skillReadyState.CurrentReadySkill.CancelReady();
            }
            CurrentSkillState = _skillEmptyState;
        }

        public Skill GetSkillRef(int skillId)
        {
            Skill skill = null;
            _skillPool.TryGetValue(skillId, out skill);
            return skill;
        }

        //public void DoSkill(int skillId)
        //{
        //    Skill skill;
        //    _skillPool.TryGetValue(skillId, out skill);
        //    if (skill != null)
        //        skill.Conjure();
        //}

        public void OnCastSkill(string skillName, string argsString)
        {
            Skill skill;
            _skillDict.TryGetValue(skillName, out skill);
            if (skill != null)
                skill.OnCast(argsString);
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