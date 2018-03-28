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
        private Transform _itemsPanel;
        [SerializeField]
        private Transform _propItemPrefab;
        [SerializeField]
        private Text _lingshiAmountText;


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
            //Debug.Log("BagPanelView:propListChanged");
            _itemsPanel.DestroyChildren();
            var tmpPropList = ((Dictionary<string, object>)arg1)["values"] as List<object>;
            if (tmpPropList != null)
            {
                foreach (var item in tmpPropList)
                {
                    var prop = (Dictionary<string, object>)item;
                    JObject propData = JObject.Parse(prop["propData"] as string);
                    var propItem = Instantiate(_propItemPrefab);
                    //Debug.Log(prop["propUUID"] + " " + propItem);
                    propItem.name = prop["propUUID"] as string;
                    var srcName = "PropImages/prop_" + int.Parse(propData["id"].ToString());
                    var itemImage = propItem.Find("Background").GetComponent<Image>();
                    Sprite tempType = itemImage.sprite;
                    itemImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                    propItem.SetParent(_itemsPanel);
                }
            }
        }

        public override void lingshiAmountChanged(int arg1)
        {
            if (_lingshiAmount != 0)
            {
                var change = arg1 - _lingshiAmount;
                if (change < 0)
                {
                    this.Publish(new ShowTipsEvent() { TipsContent = string.Format("失去{0}灵石", change) });
                }
                else
                {
                    this.Publish(new ShowTipsEvent() { TipsContent = string.Format("得到{0}灵石", change) });
                }
            }
            _lingshiAmount = arg1;
            _lingshiAmountText.text = arg1.ToString();
        }
    }
}
