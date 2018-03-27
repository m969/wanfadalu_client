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
    using PathologicalGames;


    public class GongFaEntityView : GongFaEntityViewBase {
        
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
            //Debug.Log("GongFaEntityView:gongFaListChanged");
            //var gongFaMap = this.GongFaEntity.DecodeGongFaListObject(arg1);
            //foreach (var item in gongFaMap)
            //{
            //    var spawnPool = PoolManager.Pools["MagicWeaponPool"];
            //    var weapon = spawnPool.Spawn(spawnPool.prefabs["weapon_" + item.Key]);
            //    weapon.SetParent(_weaponListNode.GetChild(item.Value.index));
            //    weapon.localScale = Vector3.one;
            //    weapon.localPosition = Vector3.zero;
            //}
        }
    }
}
