namespace MagicFire.Mmorpg.Skill
{
    using UnityEngine;
    using System.Collections;
    using MagicFire.Mmorpg;
    using MagicFire.Common.Plugin;

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
            {
                InstantiateSkillTrajectory();
            }
        }

        private void InstantiateSkillTrajectory()
        {
            var className = this.GetType().Name;
            var prefab = AssetTool.LoadTriggerAssetByName(className + "_SkillTrajectory");

            if (prefab != null)
            {
                SkillTrajectory = Object.Instantiate(prefab) as GameObject;
                if (SkillTrajectory != null) SkillTrajectory.SetActive(false);
            }
            else
            {
                Debug.LogError(className + "_SkillTrajectory prefab == null!");
            }
        }

        //技能预备
        public virtual void Ready(AvatarView spellcaster)
        {
            if (SkillTrajectory == null)
            {
                InstantiateSkillTrajectory();
            }
            else
            {
                if (SkillTrajectory.activeInHierarchy == false)
                {
                    SkillTrajectory.SetActive(true);
                }
            }
        }

        //取消技能预备
        public virtual void CancelReady()
        {
            if (SkillTrajectory != null)
            {
                SkillTrajectory.SetActive(false);
            }
        }

        //技能施放
        public virtual void Conjure(params object[] args)
        {

        }
    }
}