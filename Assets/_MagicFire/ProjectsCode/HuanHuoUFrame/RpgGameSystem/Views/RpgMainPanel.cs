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

        public override void ShowAvatarBagPanelExecuted(ShowAvatarBagPanelCommand command)
        {
            base.ShowAvatarBagPanelExecuted(command);
            if (!_bagPanelView)
            {
                var spawnPool = PoolManager.Pools["AvatarViewPool"];
                _bagPanelView = spawnPool.SpawnView(spawnPool.prefabs["BagPanel"], KBEngine.KBEngineApp.app.player() as ViewModel).GetComponent<BagPanelView>();
                _bagPanelView.transform.SetParent(WorldViewService.MasterCanvas.transform);

                _bagPanelView.transform.localScale = new Vector3(1, 1, 1);
                var rect = _bagPanelView.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(0, 0);
            }
            else
            {
                if (_bagPanelView.isActiveAndEnabled)
                {
                    _bagPanelView.gameObject.SetActive(false);
                }
                else
                {
                    _bagPanelView.gameObject.SetActive(true);
                }
            }
        }

        public override void ShowCharacterInfoPanelExecuted(ShowCharacterInfoPanelCommand command)
        {
            base.ShowCharacterInfoPanelExecuted(command);
            if (!_characterInfoPanelView)
            {
                var spawnPool = PoolManager.Pools["AvatarViewPool"];
                _characterInfoPanelView = spawnPool.SpawnView(spawnPool.prefabs["CharacterInfoPanel"], KBEngine.KBEngineApp.app.player() as ViewModel).GetComponent<CharacterInfoPanelView>();
                _characterInfoPanelView.transform.SetParent(WorldViewService.MasterCanvas.transform);

                _characterInfoPanelView.transform.localScale = new Vector3(1, 1, 1);
                var rect = _characterInfoPanelView.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(0, 0);
            }
            else
            {
                if (_characterInfoPanelView.isActiveAndEnabled)
                {
                    _characterInfoPanelView.gameObject.SetActive(false);
                }
                else
                {
                    _characterInfoPanelView.gameObject.SetActive(true);
                }
            }
        }

        public override void ShowGongFaPanelExecuted(ShowGongFaPanelCommand command)
        {
            base.ShowGongFaPanelExecuted(command);
            if (!_gongFaPanelView)
            {
                var spawnPool = PoolManager.Pools["AvatarViewPool"];
                _gongFaPanelView = spawnPool.SpawnView(spawnPool.prefabs["GongFaPanel"], KBEngine.KBEngineApp.app.player() as ViewModel).GetComponent<GongFaPanelView>();
                _gongFaPanelView.transform.SetParent(WorldViewService.MasterCanvas.transform);

                _gongFaPanelView.transform.localScale = new Vector3(1, 1, 1);
                var rect = _gongFaPanelView.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(0, 0);
            }
            else
            {
                if (_gongFaPanelView.isActiveAndEnabled)
                {
                    _gongFaPanelView.gameObject.SetActive(false);
                }
                else
                {
                    _gongFaPanelView.gameObject.SetActive(true);
                }
            }
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
