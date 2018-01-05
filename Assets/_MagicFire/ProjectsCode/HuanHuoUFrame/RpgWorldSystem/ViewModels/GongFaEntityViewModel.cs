namespace MagicFire.HuanHuoUFrame {
    using MagicFire.HuanHuoUFrame;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    using GongFaName = System.String;
    using SkillName = System.String;


    public partial class GongFaEntityViewModel : GongFaEntityViewModelBase {
        public Dictionary<GongFaName, Dictionary<SkillName, ASkill>> DecodeGongFaListObject(object gongFaListObject)
        {
            var gongFaMap = new Dictionary<GongFaName, Dictionary<SkillName, ASkill>>();
            var gongFaList = ((Dictionary<string, object>)gongFaListObject)["values"] as List<object>;
            foreach (var gongFaInfo in gongFaList)
            {
                var skillList = ((Dictionary<string, object>)gongFaInfo)["values"] as List<object>;
                var gongFaName = ((Dictionary<string, object>)gongFaInfo)["gongFa_name"] as string;
                var skillMap = new Dictionary<string, ASkill>();
                foreach (var skillInfo in skillList)
                {
                    var skill = new ASkill();
                    var skillName = (string)((Dictionary<string, object>)skillInfo)["skill_name"];
                    var skillLevel = (int)((Dictionary<string, object>)skillInfo)["skill_level"];
                    skill.skillName = skillName;
                    skill.skillLevel = skillLevel;
                    skillMap[skillName] = skill;
                }
                gongFaMap[gongFaName] = skillMap;
            }
            return gongFaMap;
        }
    }
}
