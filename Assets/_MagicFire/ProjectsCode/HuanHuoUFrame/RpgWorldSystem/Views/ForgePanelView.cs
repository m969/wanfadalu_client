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
        [SerializeField]
        private Button _clearMaterialsPanelButton;
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
                        itemTrans = item;
                    });
                    item.GetComponent<Button>().OnPointerExitAsObservable().Subscribe(_x => {
                        //Debug.Log("ForgePanelView:OnPointerExit");
                        var backgroundOutline = item.Find("Background").GetComponent<Outline>();
                        backgroundOutline.effectColor = Color.white;
                        itemTrans = null;
                    });
                }
            }).DisposeWith(this);
            this.OnEvent<OnBagItemEndDragEvent>().Subscribe(x => {
                //Debug.Log("ForgePanelView:OnBagItemBeginDragEvent");
                if (!itemTrans)
                    return;
                var itemImage = itemTrans.Find("Foreground").GetComponent<Image>();
                itemImage.color = Color.white;
                itemImage.sprite = x.BagItem.Find("Foreground").GetComponent<Image>().sprite;
                itemTrans.name = x.BagItem.name;
                var itemText = itemTrans.Find("Text").GetComponent<Text>();
            }).DisposeWith(this);
            _clearMaterialsPanelButton.OnClickAsObservable().Subscribe(ClearMaterialsPanel);
        }

        private void ClearMaterialsPanel(Unit unit)
        {
            var i = 0;
            foreach (Transform item in _materialsPanel)
            {
                item.Find("Background").GetComponent<Outline>().effectColor = Color.white;
                item.Find("Foreground").GetComponent<Image>().color = Color.black;
                item.name = "BagItem_" + i;
                i++;
            }
        }

        public override void RequestTargetItemListExecuted(RequestTargetItemListCommand command)
        {
            Debug.Log("ForgePanelView:RequestTargetItemListExecuted");
            var jo = new JObject();
            var i = 0;
            foreach (Transform item in _materialsPanel)
            {
                ulong propUUID;
                if (ulong.TryParse(item.name, out propUUID))
                {
                    jo.Add(new JProperty(i.ToString(), propUUID));
                    i++;
                }
            }
            Debug.Log(jo);
            this.Avatar.cellCall("requestTargetItemList", jo.ToString());
        }

        public override void RequestForgeExecuted(RequestForgeCommand command)
        {
            Debug.Log("ForgePanelView:RequestForgeExecuted");
            var ja = new JArray();
            foreach (Transform item in _materialsPanel)
            {
                ulong propUUID;
                if (ulong.TryParse(item.name, out propUUID))
                {
                    ja.Add(new JValue(propUUID));
                }
            }
            Debug.Log(ja);
            if (_currentSelectPropID == 0)
            {
                this.Publish(new ShowTipsEvent()
                {
                    TipsContent = "请选择一个目标道具"
                });
                return;
            }
            this.Avatar.cellCall("requestForge", ja.ToString(), _currentSelectPropID);
        }

        private void ClearItems()
        {
            foreach (Transform item in _targetItemListPanel)
            {
                item.Find("Foreground").GetComponent<Image>().color = Color.black;
                item.Find("Text").GetComponent<Text>().text = "物品";
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
