using DG.Tweening;
using PathologicalGames;
using UnityEngine.UI;

namespace MagicFire.HuanHuoUFrame {
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
    
    
    public class SkillEntityView : SkillEntityViewBase {
        private GameObject _iceImprisonEffect;
        public readonly Dictionary<int, Skill> SkillMap = new Dictionary<int, Skill>();


        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as SkillEntityViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.SkillEntity to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

        public void AddSkill(Skill skill)
        {
            skill.SkillController = GetComponent<RpgSkillController>();
            var skillID = skill.GongFaID * 10 + skill.SkillIndex;
            if (SkillMap.ContainsKey(skillID))
            {
                Debug.LogError("SkillMap already exits skill " + skillID);
                return;
            }
            SkillMap.Add(skill.GongFaID * 10 + skill.SkillIndex, skill);
        }

        public override void isIceFreezingChanged(Int32 arg1)
        {
            var isIceFreezing = arg1;
            if (isIceFreezing != 0)
            {
                if (_iceImprisonEffect == null)
                {
                    _iceImprisonEffect = PoolManager.Pools["SkillTrajectoryPool"].Spawn("SkillE_IceImprisonEffect").gameObject;

                    if (_iceImprisonEffect != null)
                        _iceImprisonEffect.transform.SetParent(transform);
                }
                if (_iceImprisonEffect != null)
                {
                    _iceImprisonEffect.SetActive(true);
                    _iceImprisonEffect.transform.localPosition = new Vector3(0, 5, 1);
                }
            }
            else
            {
                if (_iceImprisonEffect != null)
                {
                    _iceImprisonEffect.SetActive(false);
                }
            }
        }

        public override void OnSkillStartCastExecuted(OnSkillStartCastCommand command)
        {
            base.OnSkillStartCastExecuted(command);
            Skill skill;
            SkillMap.TryGetValue(command.skillID, out skill);
            if (skill != null)
                skill.OnCast(command.argsString);
        }

        public override void gongFaListChanged(object arg1)
        {
            SkillMap.Clear();
            var gongFaMap = this.SkillEntity.DecodeGongFaListObject(arg1);
            foreach (var item in gongFaMap)
            {
                foreach (var skill in item.Value.skillList)
                {
                    Type skillType;
                    if (Skill.SkillTypeMap.TryGetValue(item.Value.gongFaID * 10 + skill.Key, out skillType))
                    {
                        Skill s = Activator.CreateInstance(skillType, new object[] { this }) as Skill;
                        s.GongFaID = item.Value.gongFaID;
                        s.SkillIndex = skill.Key;
                        AddSkill(s);
                    }
                    else
                    {
                        Debug.LogError("Error! Not Found skill " + (item.Value.gongFaID * 10 + skill.Key));
                    }
                }
            }
        }
    }
}
