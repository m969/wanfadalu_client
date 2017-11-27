namespace MagicFire.HuanHuoUFrame {
    using MagicFire.HuanHuoUFrame;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Kernel;
    using uFrame.Kernel.Serialization;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.MVVM.Services;
    using uFrame.MVVM.ViewModels;
    using UniRx;
    using UnityEngine;
    using GongFaName = System.String;
    using SkillName = System.String;


    public class GongFaEntityView : GongFaEntityViewBase {
        
        protected Dictionary<GongFaName, Dictionary<SkillName, ASkill>> _gongFaMap;

        public struct ASkill
        {
            public int skillLevel;
        }

        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as GongFaEntityViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.GongFaEntity to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

        public override void gongFaListChanged(object arg1)
        {
            //var gongFaList = ((Dictionary<string, object>)arg1)["values"] as List<Dictionary<string, object>>;
            //foreach (var gongFaInfo in gongFaList)
            //{
            //    var skillList = gongFaInfo["values"] as List<Dictionary<string, object>>;
            //    var gongFaName = gongFaInfo["gongFa_name"] as string;
            //    var skillMap = new Dictionary<string, ASkill>();
            //    foreach (var skillInfo in skillList)
            //    {
            //        var skill = new ASkill();
            //        skill.skillLevel = (int)skillInfo["skill_level"];
            //        skillMap[skillInfo["skill_name"] as string] = skill;
            //    }
            //    this._gongFaMap[gongFaName] = skillMap;
            //}
        }
    }
}
