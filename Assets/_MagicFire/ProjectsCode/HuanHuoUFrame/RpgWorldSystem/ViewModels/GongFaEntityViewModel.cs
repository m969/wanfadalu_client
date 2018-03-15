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
    using GongFaID = System.Int32;
    using SkillID = System.Int32;


    public partial class GongFaEntityViewModel : GongFaEntityViewModelBase {
        public Dictionary<GongFaID, Dictionary<SkillID, ASkill>> DecodeGongFaListObject(object gongFaListObject)
        {
            var gongFaMap = new Dictionary<GongFaID, Dictionary<SkillID, ASkill>>();
            var gongFaList = ((Dictionary<string, object>)gongFaListObject)["values"] as List<object>;
            foreach (var gongFaInfo in gongFaList)
            {
                var skillList = ((Dictionary<string, object>)gongFaInfo)["values"] as List<object>;
                var gongFaID = (int)((Dictionary<string, object>)gongFaInfo)["gongFaID"];
                var skillMap = new Dictionary<int, ASkill>();
                foreach (var skillInfo in skillList)
                {
                    var skill = new ASkill();
                    var skillIndex = (int)((Dictionary<string, object>)skillInfo)["skillIndex"];
                    var skillLevel = (int)((Dictionary<string, object>)skillInfo)["skillLevel"];
                    skill.skillIndex = skillIndex;
                    skill.skillLevel = skillLevel;
                    skillMap[skillIndex] = skill;
                }
                gongFaMap[gongFaID] = skillMap;
            }
            return gongFaMap;
        }
    }
}
