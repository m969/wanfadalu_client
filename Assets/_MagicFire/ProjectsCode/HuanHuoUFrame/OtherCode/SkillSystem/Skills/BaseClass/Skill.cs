namespace MagicFire.HuanHuoUFrame
{
    using UnityEngine;
    using System.Collections;
    using MagicFire.Common.Plugin;
    using PathologicalGames;

    public class Skill
    {
        public string SkillName = "";
        public string ArgsString = "";
        protected readonly AvatarView Spellcaster;
        protected GameObject SkillTrajectory;


        protected Skill(AvatarView spellcaster)
        {
            Spellcaster = spellcaster;
            if (SkillTrajectory == null)
                InstantiateSkillTrajectory();
        }

        private void InstantiateSkillTrajectory()
        {
            var className = this.GetType().Name;
            var pool = PoolManager.Pools["SkillTrajectoryPool"];
            if (pool.prefabs.ContainsKey(className + "_SkillTrajectory"))
            {
                SkillTrajectory = pool.Spawn(pool.prefabs[className + "_SkillTrajectory"]).gameObject;
                if (SkillTrajectory != null)
                    SkillTrajectory.SetActive(false);
            }
        }

        //技能预备
        public virtual void Ready(AvatarView spellcaster)
        {
            if (SkillTrajectory == null)
                InstantiateSkillTrajectory();
            else
                if (SkillTrajectory.activeInHierarchy == false)
                    SkillTrajectory.SetActive(true);
        }

        //取消技能预备
        public virtual void CancelReady()
        {
            if (SkillTrajectory != null)
                SkillTrajectory.SetActive(false);
        }

        //技能施放
        public virtual void Conjure(params object[] args)
        {
            Spellcaster.Avatar.Execute(new RequestCastSkillByNameCommand()
            {
                skillName = SkillName,
                argsString = ArgsString
            });
        }
        
        //技能表现
        public virtual void OnCast(string argsString)
        {
            Spellcaster.Animator.SetTrigger("Attack_2");
        }
    }
}