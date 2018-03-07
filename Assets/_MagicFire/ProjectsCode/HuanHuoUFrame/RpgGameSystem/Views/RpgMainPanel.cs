using PathologicalGames;
using uFrame.IOC;

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


    public class RpgMainPanel : RpgMainPanelBase
    {
        [Inject("WorldViewService")]
        public WorldViewService WorldViewService;

        private BagPanelView 
            _bagPanelView;
        private CharacterInfoPanelView 
            _characterInfoPanelView;
        private GongFaPanelView
            _gongFaPanelView;
        private RankingListPanelView 
            _rankingListPanelView;
        private SectPanelView 
            _sectPanelView;
        private DialogPanelView
            _dialogPanelView;

        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as RpgMainScreenViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.RpgMainScreen to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }

        private T ShowAvatarPanel<T>(T panelView, string poolName, string panelName) where T : PanelView
        {
            if (panelView == null)
            {
                var spawnPool = PoolManager.Pools[poolName];
                panelView = spawnPool.SpawnView(spawnPool.prefabs[panelName], KBEngine.KBEngineApp.app.player() as ViewModel).GetComponent<T>();
                panelView.transform.SetParent(WorldViewService.MasterCanvas.transform);
                panelView.transform.localScale = new Vector3(1, 1, 1);
                var rect = panelView.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(0, 0);
            }
            else
            {
                if (panelView.isActiveAndEnabled)
                    panelView.gameObject.SetActive(false);
                else
                    panelView.gameObject.SetActive(true);
            }
            return panelView;
        }

        public override void ShowAvatarBagPanelExecuted(ShowAvatarBagPanelCommand command)
        {
            _bagPanelView = ShowAvatarPanel(_bagPanelView, "AvatarViewPool", "BagPanel");
        }

        public override void ShowCharacterInfoPanelExecuted(ShowCharacterInfoPanelCommand command)
        {
            _characterInfoPanelView = ShowAvatarPanel(_characterInfoPanelView, "AvatarViewPool", "CharacterInfoPanel");
        }

        public override void ShowGongFaPanelExecuted(ShowGongFaPanelCommand command)
        {
            _gongFaPanelView = ShowAvatarPanel(_gongFaPanelView, "AvatarViewPool", "GongFaPanel");
        }

        public override void ShowRankingListPanelExecuted(ShowRankingListPanelCommand command)
        {
            _rankingListPanelView = ShowAvatarPanel(_rankingListPanelView, "AvatarViewPool", "RankingListPanel");
        }

        public override void ShowSectPanelExecuted(ShowSectPanelCommand command)
        {
            _sectPanelView = ShowAvatarPanel(_sectPanelView, "AvatarViewPool", "SectPanel");
        }

        public override void ShowDialogPanelExecuted(ShowDialogPanelCommand command)
        {
            Debug.Log("RpgMainPanel:ShowDialogPanelExecuted");
            _dialogPanelView = ShowAvatarPanel(_dialogPanelView, "UIPanelPool", "DialogPanel");
            if (_dialogPanelView.isActiveAndEnabled)
            {
                _dialogPanelView.ClearDialogItem();
                if (command.NpcView._npcType == 1)
                {
                    _dialogPanelView.AddDialogItem("我要上擂台", evt => {
                        _dialogPanelView.Avatar.Execute(new RequestEnterArenaCommand() { ArenaID = command.NpcView._arenaID });
                        _dialogPanelView.gameObject.SetActive(false);
                    });
                    _dialogPanelView.AddDialogItem("算了，我怂了", evt => { _dialogPanelView.gameObject.SetActive(false); });
                }
                else if (command.NpcView._npcType == 2)
                {
                    _dialogPanelView.AddDialogItem("我要购买道具", evt => { _dialogPanelView.gameObject.SetActive(false); });
                    _dialogPanelView.AddDialogItem("算了，没钱", evt => { _dialogPanelView.gameObject.SetActive(false); });
                }
            }
        }

        public override void ExitArenaExecuted(ExitArenaCommand command)
        {
            Debug.Log("RpgMainPanel:ExitArenaExecuted");
        }

        public override void ExitGameExecuted(ExitGameCommand command)
        {
            Debug.Log("RpgMainPanel:ExitGameExecuted");
        }
    }
}
