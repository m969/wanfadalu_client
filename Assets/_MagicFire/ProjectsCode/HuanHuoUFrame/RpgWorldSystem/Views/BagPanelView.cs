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
    using UnityEngine.EventSystems;


    public class BagPanelView : BagPanelViewBase
    {
        [SerializeField]
        private Transform _itemsPanel;
        [SerializeField]
        private Text _lingshiAmountText;
        [SerializeField]
        private Button _sellButton;

        private int _currentSelectProp = 0;


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
            _sellButton.OnClickAsObservable().Subscribe(x => {
                if (_currentSelectProp > 0)
                {
                    Avatar.cellCall("requestSellProp", _currentSelectProp);
                }
                else
                {
                    this.Publish(new ShowTipsEvent()
                    {
                        TipsContent = "请选择一个道具"
                    });
                }
            });
        }

        private void ClearItems()
        {
            foreach (Transform item in _itemsPanel)
            {
                item.Find("Foreground").GetComponent<Image>().color = Color.black;
                item.Find("Text").GetComponent<Text>().text = "";
                item.GetComponent<Button>().interactable = false;
                item.GetComponent<Button>().onClick.RemoveAllListeners();
                item.GetComponent<EventTrigger>().triggers.Clear();
            }
        }

        public override void propListChanged(object arg1)
        {
            //Debug.Log("BagPanelView:propListChanged");
            ClearItems();
            var i = 0;
            var tmpPropList = ((Dictionary<string, object>)arg1)["values"] as List<object>;
            if (tmpPropList != null)
            {
                foreach (var item in tmpPropList)
                {
                    var prop = (Dictionary<string, object>)item;
                    JObject propData = JObject.Parse(prop["propData"] as string);
                    var propItem = _itemsPanel.GetChild(i);
                    var itemImage = propItem.Find("Foreground").GetComponent<Image>();
                    var propID = int.Parse(propData["id"].ToString());
                    var srcName = "PropImages/prop_" + propID;
                    Sprite tempType = itemImage.sprite;
                    itemImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                    var itemText = propItem.Find("Text").GetComponent<Text>();
                    itemText.text = PropSystemController.PropConfigList[propID].name;
                    propItem.GetComponent<EventTrigger>().AsObservableOfPointerDown()
                        .Where(x => { return x.button == PointerEventData.InputButton.Left; })
                        .Subscribe(x => {
                            Debug.Log("BagPanelView:PointerDown " + propID);
                        });

                    //var propItem = Instantiate(_propItemPrefab);
                    //Debug.Log(prop["propUUID"] + " " + propItem);
                    //propItem.name = prop["propUUID"] as string;
                    //var srcName = "PropImages/prop_" + int.Parse(propData["id"].ToString());
                    //var itemImage = propItem.Find("Background").GetComponent<Image>();
                    //Sprite tempType = itemImage.sprite;
                    //itemImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                    //propItem.SetParent(_itemsPanel);
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
