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
    
    
    public class RpgMainPanel : RpgMainPanelBase
    {
        [Inject("WorldViewService")]
        public WorldViewService WorldViewService;

        private BagPanelView _bagPanelView;
        private CharacterInfoPanelView _characterInfoPanelView;
        private GongFaPanelView _gongFaPanelView;
        private RankingListPanelView _rankingListPanelView;
        private SectPanelView _sectPanelView;

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
                {
                    panelView.gameObject.SetActive(false);
                }
                else
                {
                    panelView.gameObject.SetActive(true);
                }
            }
            return panelView;
        }

        public override void ShowAvatarBagPanelExecuted(ShowAvatarBagPanelCommand command)
        {
            base.ShowAvatarBagPanelExecuted(command);
            _bagPanelView = ShowAvatarPanel(_bagPanelView, "AvatarViewPool", "BagPanel");
        }

        public override void ShowCharacterInfoPanelExecuted(ShowCharacterInfoPanelCommand command)
        {
            base.ShowCharacterInfoPanelExecuted(command);
            _characterInfoPanelView = ShowAvatarPanel(_characterInfoPanelView, "AvatarViewPool", "CharacterInfoPanel");
        }

        public override void ShowGongFaPanelExecuted(ShowGongFaPanelCommand command)
        {
            base.ShowGongFaPanelExecuted(command);
            _gongFaPanelView = ShowAvatarPanel(_gongFaPanelView, "AvatarViewPool", "GongFaPanel");
        }

        public override void ShowRankingListPanelExecuted(ShowRankingListPanelCommand command)
        {
            base.ShowRankingListPanelExecuted(command);
            _rankingListPanelView = ShowAvatarPanel(_rankingListPanelView, "AvatarViewPool", "RankingListPanel");
        }

        public override void ShowSectPanelExecuted(ShowSectPanelCommand command)
        {
            base.ShowSectPanelExecuted(command);
            _sectPanelView = ShowAvatarPanel(_sectPanelView, "AvatarViewPool", "SectPanel");
        }

        public override void ExitArenaExecuted(ExitArenaCommand command)
        {
            base.ExitArenaExecuted(command);
            Debug.Log("RpgMainPanel:ExitArenaExecuted");
        }

        public override void ExitGameExecuted(ExitGameCommand command)
        {
            base.ExitGameExecuted(command);
            Debug.Log("RpgMainPanel:ExitGameExecuted");
        }
    }
}
