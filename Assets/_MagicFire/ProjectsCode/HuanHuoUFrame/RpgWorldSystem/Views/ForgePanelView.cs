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
    using UnityEngine.EventSystems;
    using UniRx.Triggers;
    using Newtonsoft.Json.Linq;


    public class ForgePanelView : ForgePanelViewBase {
        [SerializeField]
        private Transform _materialsPanel;
        [SerializeField]
        private Transform _targetItemListPanel;
        private int _currentSelectPropID = 0;

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
            Transform itemTrans = null;
            this.OnEvent<OnBagItemBeginDragEvent>().Subscribe(x => {
                //Debug.Log("ForgePanelView:OnBagItemBeginDragEvent");
                foreach (Transform item in _materialsPanel)
                {
                    item.GetComponent<Button>().OnPointerEnterAsObservable().Subscribe(_x => {
                        //Debug.Log("ForgePanelView:PointerEnter");
                        var backgroundOutline = item.Find("Background").GetComponent<Outline>();
                        backgroundOutline.effectColor = Color.yellow;
                        if (itemTrans)
                        {
                            backgroundOutline = itemTrans.Find("Background").GetComponent<Outline>();
                            backgroundOutline.effectColor = Color.white;
                        }
                        itemTrans = item;
                    });
                }
            }).DisposeWith(this);
            this.OnEvent<OnBagItemEndDragEvent>().Subscribe(x => {
                //Debug.Log("ForgePanelView:OnBagItemBeginDragEvent");
                var itemImage = itemTrans.Find("Foreground").GetComponent<Image>();
                itemImage.color = Color.white;
                itemImage.sprite = x.BagItem.Find("Foreground").GetComponent<Image>().sprite;
                var itemText = itemTrans.Find("Text").GetComponent<Text>();
            }).DisposeWith(this);
        }

        public override void RequestTargetItemListExecuted(RequestTargetItemListCommand command)
        {
            Debug.Log("ForgePanelView:RequestTargetItemListExecuted");
            var itemUUIDList = new List<ulong>();
            foreach (Transform item in _materialsPanel)
            {
                itemUUIDList.Add(ulong.Parse(item.name));
            }
            var itemUUIDList_json = JsonUtility.ToJson(itemUUIDList);
            Debug.Log(itemUUIDList_json);
            this.Avatar.cellCall("requestTargetItemList", itemUUIDList_json);
        }

        public override void RequestForgeExecuted(RequestForgeCommand command)
        {
            Debug.Log("ForgePanelView:RequestForgeExecuted");
            var itemUUIDList = new List<ulong>();
            foreach (Transform item in _materialsPanel)
            {
                itemUUIDList.Add(ulong.Parse(item.name));
            }
            var itemUUIDList_json = JsonUtility.ToJson(itemUUIDList);
            Debug.Log(itemUUIDList_json);
            if (_currentSelectPropID == 0)
            {
                this.Publish(new ShowTipsEvent()
                {
                    TipsContent = "请选择一个目标道具"
                });
                return;
            }
            this.Avatar.cellCall("requestForge", itemUUIDList_json, _currentSelectPropID);
        }

        private void ClearItems()
        {
            foreach (Transform item in _targetItemListPanel)
            {
                item.Find("Foreground").GetComponent<Image>().color = Color.black;
                item.Find("Text").GetComponent<Text>().text = "";
                item.GetComponent<Button>().interactable = false;
                item.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }

        public override void OnTargetItemListReturnExecuted(OnTargetItemListReturnCommand command)
        {
            Debug.Log("ForgePanelView:OnTargetItemListReturnExecuted");
            ClearItems();
            Debug.Log(command.TargetItemListJson);
            var targetItemList = JObject.Parse(command.TargetItemListJson);
            Debug.Log(targetItemList);
            var i = 0;
            foreach (var item in targetItemList)
            {
                var propItem = _targetItemListPanel.GetChild(i);
                i++;
                var itemImage = propItem.Find("Foreground").GetComponent<Image>();
                var propID = int.Parse(item.Value.ToString());
                var srcName = "PropImages/prop_" + propID;
                Sprite tempType = itemImage.sprite;
                itemImage.sprite = Resources.Load(srcName, tempType.GetType()) as Sprite;
                itemImage.color = Color.white;
                var itemText = propItem.Find("Text").GetComponent<Text>();
                itemText.text = PropSystemController.PropConfigList[propID].name;
                var propItemButton = propItem.GetComponent<Button>();
                propItemButton.interactable = true;
                propItemButton.OnClickAsObservable().Subscribe(x => { _currentSelectPropID = propID; });
            }
        }
    }
}
