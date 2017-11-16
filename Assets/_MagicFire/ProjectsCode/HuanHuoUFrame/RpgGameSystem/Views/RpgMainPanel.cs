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
            this.AddBinding(this.OnEvent<ShowAvatarBagEvent>().Subscribe(x =>
            {
                if (!_bagPanelView)
                {
                    var spawnPool = PoolManager.Pools["AvatarViewPool"];
                    _bagPanelView = spawnPool.Spawn("BagPanel").GetComponent<BagPanelView>();
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
            }));
        }

        public override void ShowCharacterInfoPanelExecuted(ShowCharacterInfoPanelCommand command)
        {
            base.ShowCharacterInfoPanelExecuted(command);
        }

        public override void ShowAvatarBagPanelExecuted(ShowAvatarBagPanelCommand command)
        {
            base.ShowAvatarBagPanelExecuted(command);
            this.Publish(new ShowAvatarBagEvent());
        }

        public override void ExitGameExecuted(ExitGameCommand command)
        {
            base.ExitGameExecuted(command);
        }
    }
}
