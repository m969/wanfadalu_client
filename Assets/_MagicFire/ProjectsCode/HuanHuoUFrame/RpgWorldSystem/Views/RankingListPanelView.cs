﻿namespace MagicFire.HuanHuoUFrame {
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
    
    
    public class RankingListPanelView : RankingListPanelViewBase {
        [SerializeField]
        private GameObject _rankingItemsPanel;
        [SerializeField]
        private GameObject _selfRankingPanel;
        [SerializeField]
        private GameObject _rankingItem;

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
            this.Avatar.Execute(new RequestRankingListCommand());
            this.Avatar.Execute(new RequestSelfRankingCommand());
            //this.Avatar.RequestRankingList.OnNext(new RequestRankingListCommand());
            //this.Avatar.RequestSelfRanking.OnNext(new RequestSelfRankingCommand());
        }

        private void OnEnable()
        {
            if (this.Avatar == null) return;
            this.Avatar.Execute(new RequestRankingListCommand());
            this.Avatar.Execute(new RequestSelfRankingCommand());
        }

        public override void OnRequestRankingListReturnExecuted(OnRequestRankingListReturnCommand command)
        {
            Debug.Log("RankingListPanelView:OnRequestRankingListReturnExecuted");
            var rankingList = ((Dictionary<string, object>)command.RankingList)["values"] as List<object>;
            foreach (var item in rankingList)
            {
                var rankingItemInfo = (Dictionary<string, object>)item;
                var spawnPool = PathologicalGames.PoolManager.Pools["UIPanelPool"];
                var rankingItem = _rankingItemsPanel.transform.GetChild((int)(ulong)rankingItemInfo["ranking"] - 1);
                rankingItem.Find("Text").GetComponent<UnityEngine.UI.Text>().text = rankingItemInfo["ranking"] + "：" + rankingItemInfo["avatarName"] + " 胜利场次：" + rankingItemInfo["winAmount"];
            }
        }

        public override void OnRequestSelfRankingReturnExecuted(OnRequestSelfRankingReturnCommand command)
        {
            Debug.Log("RankingListPanelView:OnRequestSelfRankingReturnExecuted");
            var rankingInfo = (Dictionary<string, object>)command.RankingInfo;
            _selfRankingPanel.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = rankingInfo["avatarName"] + " 第" + rankingInfo["ranking"] + "名" + " 胜利场次：" + rankingInfo["winAmount"];
        }
    }
}
