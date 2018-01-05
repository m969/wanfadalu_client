using PathologicalGames;

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
    using UnityEngine.UI;


    public class BagPanelView : BagPanelViewBase
    {
        [SerializeField]
        private Text _goldCountText;
        [SerializeField]
        private Transform _itemsPanel;

        private SpawnPool _avatarViewPool;

        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as AvatarViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Avatar to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            _avatarViewPool = PoolManager.Pools["AvatarViewPool"];
        }

        public override void goldCountChanged(Int32 goldCount)
        {
            base.goldCountChanged(goldCount);
            _goldCountText.text = goldCount.ToString();
        }

        public override void avatarBagChanged(System.Object avatarBagObject)
        {
            base.avatarBagChanged(avatarBagObject);
            var tmpAvatarBag = ((Dictionary<string, object>)avatarBagObject)["values"] as List<int>;
            if (tmpAvatarBag != null)
            {
                foreach (var item in tmpAvatarBag)
                {
                    var goodsItem = _avatarViewPool.Spawn(_avatarViewPool.prefabs["BagItem"]);
                    goodsItem.SetParent(_itemsPanel);
                }
            }
        }
    }
}
