namespace MagicFire.HuanHuoUFrame
{
    using UnityEngine;
    using System.Collections;
    using PathologicalGames;

    public class Skill
    {
        public int GongFaID = 0;
        public int SkillIndex = 0;
        public string ArgsString = "";
        public RpgSkillController SkillController;
        protected readonly SkillEntityView Spellcaster;
        protected GameObject SkillTrajectory;


        protected Skill(SkillEntityView spellcaster)
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
            Spellcaster.SkillEntity.Execute(new RequestCastSkillCommand()
            {
                gongFaID = GongFaID,
                skillIndex = SkillIndex,
                argsString = ArgsString
            });
        }
        
        //技能表现
        public virtual void OnCast(string argsString)
        {
            ((AvatarView)Spellcaster).Animator.SetTrigger("Attack_2");
        }
    }
}