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

    public class GongFa
    {
        public int index;
        public int gongFaID;
        public Dictionary<int, ASkill> skillList;
    }

    public partial class GongFaEntityViewModel : GongFaEntityViewModelBase {
        public Dictionary<int, GongFa> DecodeGongFaListObject(object gongFaListObject)
        {
            var gongFaMap = new Dictionary<int, GongFa>();
            var gongFaList = ((Dictionary<string, object>)gongFaListObject)["values"] as List<object>;
            foreach (var gongFaInfo in gongFaList)
            {
                var gongFa = new GongFa();
                var skillList = ((Dictionary<string, object>)gongFaInfo)["values"] as List<object>;
                var gongFaID = (int)((Dictionary<string, object>)gongFaInfo)["gongFaID"];
                var index = (int)((Dictionary<string, object>)gongFaInfo)["index"];
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
                gongFa.index = index;
                gongFa.gongFaID = gongFaID;
                gongFa.skillList = skillMap;
                gongFaMap[index] = gongFa;
            }
            return gongFaMap;
        }
    }
}
