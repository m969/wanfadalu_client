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
    using UnityEngine.UI;
    using PathologicalGames;
    
    
    public class DialogPanelView : DialogPanelViewBase {
        [SerializeField]
        private Text _npcNameText;
        [SerializeField]
        private Text _npcDialogText;
        [SerializeField]
        private GameObject _npcPanel;
        [SerializeField]
        private GameObject _playerPanel;

        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as NpcViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Npc to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

        public void AddDialogItem(Transform dialogItem)
        {
            Debug.Log("DialogPanelView:AddDialogItem");
            dialogItem.SetParent(_playerPanel.transform);
        }

        public void AddDialogItem(string dialogContent, Action<Unit> action)
        {
            Debug.Log("DialogPanelView:AddDialogItem");
            var spawnPool = PoolManager.Pools["UIPanelPool"];
            var item = spawnPool.Spawn(spawnPool.prefabs["DialogItem"]);
            item.GetComponent<Button>().OnClickAsObservable().Subscribe(action);
            item.Find("Text").GetComponent<Text>().text = dialogContent;
            item.SetParent(_playerPanel.transform);
        }

        public void ClearDialogItem()
        {
            Debug.Log("DialogPanelView:ClearDialogItem childCount=" + _playerPanel.transform.childCount);
            _playerPanel.DestroyChildren();
        }

        public void RefreshPanel(object DialogItemsObject)
        {
            ClearDialogItem();
            var itemList = ((Dictionary<string, object>)DialogItemsObject)["values"] as List<object>;
            foreach (var item in itemList)
            {
                var itemInfo = item as Dictionary<string, object>;
                AddDialogItem((string)itemInfo["content"], evt =>
                {
                    KBEngine.KBEngineApp.app.player().cellCall("selectDialogItem", (int)itemInfo["id"]);
                    gameObject.SetActive(false);
                });
            }
        }
    }
}
