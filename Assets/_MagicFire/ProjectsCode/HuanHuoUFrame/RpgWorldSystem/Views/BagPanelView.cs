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
    using Newtonsoft.Json.Linq;


    public class BagPanelView : BagPanelViewBase
    {
        [SerializeField]
        private Text _goldCountText;
        [SerializeField]
        private Transform _itemsPanel;

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
        }

        public override void propListChanged(object arg1)
        {
            base.propListChanged(arg1);
            var tmpPropList = ((Dictionary<string, object>)arg1)["values"] as List<object>;
            if (tmpPropList != null)
            {
                var spawnPool = PoolManager.Pools["UIPanelPool"];
                foreach (var item in tmpPropList)
                {
                    var prop = (Dictionary<string, object>)item;
                    JObject propData = JObject.Parse(prop["propData"] as string);
                    var propItem = spawnPool.Spawn(spawnPool.prefabs["BagItem"]);
                    propItem.name = prop["propUUID"] as string;
                    var srcName = "PropImages/prop_" + int.Parse(propData["id"].ToString());
                    var itemImage = propItem.Find("Background").GetComponent<Image>();
                    Sprite tempType = itemImage.sprite;
                    itemImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                    propItem.SetParent(_itemsPanel);
                }
            }
        }
    }
}
