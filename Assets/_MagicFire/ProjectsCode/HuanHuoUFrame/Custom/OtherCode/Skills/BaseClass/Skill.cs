namespace MagicFire.HuanHuoUFrame
{
    using UnityEngine;
    using System.Collections;
    using PathologicalGames;
    using System.Collections.Generic;
    using System;
    using Newtonsoft.Json.Linq;

    public abstract class Skill
    {
        public int GongFaID = 0;
        public int SkillIndex = 0;
        public string ArgsString = "";
        public RpgSkillController SkillController;
        protected readonly SkillEntityView Spellcaster;
        protected GameObject SkillTrajectory;
        public RaycastHit RaycastHit;
        public const int RaycastHitDist = 500;

        public static readonly Dictionary<int, Type> SkillTypeMap = new Dictionary<int, Type>();

        public static void InitSkillTypeMap()
        {
            var skillJsonText = Resources.Load<TextAsset>("JsonConfigDatas/skill_config_Table");
            var skillConfigJson = JObject.Parse(skillJsonText.text);
            foreach (var item in skillConfigJson)
            {
                var skillID = int.Parse(item.Key);
                var skillScript = item.Value["script"].ToString();
                foreach (System.Reflection.Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var script = ass.GetType("MagicFire.HuanHuoUFrame." + skillScript);
                    if (script != null)
                    {
                        SkillTypeMap.Add(skillID, script);
                        break;
                    }
                }
                if (!SkillTypeMap.ContainsKey(skillID))
                    Debug.LogError("Error: not find MagicFire.HuanHuoUFrame." + skillScript);
            }
        }

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
            if (SkillTrajectory.activeInHierarchy == false)
                SkillTrajectory.SetActive(true);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit, RaycastHitDist, 1 << LayerMask.NameToLayer("Terrian"));
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
            Spellcaster.transform.LookAt(new Vector3(RaycastHit.point.x, Spellcaster.transform.position.y, RaycastHit.point.z));
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