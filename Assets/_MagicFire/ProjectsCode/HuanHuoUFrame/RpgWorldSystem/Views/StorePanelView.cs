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


    public class StorePanelView : StorePanelViewBase {
        [SerializeField]
        private Transform _itemListPanel;
        [SerializeField]
        private Transform _itemInfoPanel;
        private int _npcID;
        private Transform _currentSelectItem;
        //[SerializeField]
        //private Transform _outlineImage;

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
            foreach (var item in _itemListPanel)
            {
                var button = ((Transform)item).GetComponent<Button>();
                button.onClick.AsObservable().Subscribe(x=>
                {
                    if (_currentSelectItem != null)
                        _currentSelectItem.Find("Describtion").gameObject.SetActive(false);
                    _currentSelectItem = (Transform)item;
                    _currentSelectItem.Find("Describtion").gameObject.SetActive(true);
                    var propID = int.Parse(_currentSelectItem.name.Split("_".ToCharArray())[0]);
                    var propPrice = int.Parse(_currentSelectItem.name.Split("_".ToCharArray())[1]);
                    _itemInfoPanel.Find("ItemInfo/Description/Text").GetComponent<Text>().text = WorldViewService.ConfigTableMap["prop_config_Table"][propID.ToString()]["description"].ToString();
                    _itemInfoPanel.Find("ItemInfo/LingshiAmount").GetComponent<Text>().text = propPrice.ToString();
                    //_outlineImage.gameObject.SetActive(true);
                    //_outlineImage.SetParent(_currentSelectItem);
                });
                button.transform.Find("Describtion").gameObject.SetActive(false);
                button.interactable = false;
            }
        }

        public void RefreshPanel(int npcID, object StorePropList)
        {
            foreach (var item in _itemListPanel)
            {
                var button = ((Transform)item).GetComponent<Button>();
                button.interactable = false;
            }
            //_outlineImage.gameObject.SetActive(false);
            if (_currentSelectItem != null)
                _currentSelectItem.Find("Describtion").gameObject.SetActive(false);
            _currentSelectItem = null;
            _npcID = npcID;
            var itemList = ((Dictionary<string, object>)StorePropList)["values"] as List<object>;
            foreach (var item in itemList)
            {
                var index = itemList.IndexOf(item);
                var itemInfo = item as Dictionary<string, object>;
                var childItem = _itemListPanel.GetChild(index);
                childItem.name = itemInfo["propID"] + "_" + itemInfo["propPrice"] + "_" + index;
                childItem.Find("Describtion").GetComponent<Text>().text = WorldViewService.ConfigTableMap["prop_config_Table"][itemInfo["propID"].ToString()]["name"].ToString();
                childItem.GetComponent<Button>().interactable = true;
                var srcName = "PropImages/prop_" + itemInfo["propID"];
                var itemImage = childItem.GetComponent<Image>();
                Sprite tempType = itemImage.sprite;
                itemImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
            }
        }

        public void Buy()
        {
            if (_currentSelectItem != null)
                KBEngine.KBEngineApp.app.player().cellCall("requestBuyProp", _npcID, int.Parse(_currentSelectItem.name.Split("_".ToCharArray())[2]));
        }
    }
}
