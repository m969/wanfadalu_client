namespace MagicFire.HuanHuoUFrame
{
    using UnityEngine;
    using System.Collections;
    using MagicFire.Common.Plugin;
    using PathologicalGames;

    public class Skill
    {
        public GameObject SkillTrajectory
        {
            get;
            set;
        }

        protected AvatarView _spellcaster;

        public Skill(AvatarView spellcaster)
        {
            _spellcaster = spellcaster;
            if (SkillTrajectory == null)
                InstantiateSkillTrajectory();
        }

        private void InstantiateSkillTrajectory()
        {
            var className = this.GetType().Name;
            var pool = PoolManager.Pools["SkillTrajectoryPool"];
            SkillTrajectory = pool.Spawn(pool.prefabs[className + "_SkillTrajectory"]).gameObject;
            if (SkillTrajectory != null) SkillTrajectory.SetActive(false);
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

        }
    }
}