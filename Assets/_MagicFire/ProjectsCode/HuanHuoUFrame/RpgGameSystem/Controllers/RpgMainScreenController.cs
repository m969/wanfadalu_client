namespace MagicFire.HuanHuoUFrame {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UniRx;

    public class RpgMainScreenController : RpgMainScreenControllerBase {
        
        public override void InitializeRpgMainScreen(RpgMainScreenViewModel viewModel) {
            base.InitializeRpgMainScreen(viewModel);
            // This is called when a RpgMainScreenViewModel is created

            this.OnEvent<ShowAvatarBagEvent>().Subscribe(ShowAvatarBag);
            this.OnEvent<ShowCharacterInfoPanelEvent>().Subscribe(ShowCharacterInfoPanel);
            this.OnEvent<ShowGongFaPanelEvent>().Subscribe(ShowGongFaPanel);
            this.OnEvent<ShowDialogPanelEvent>().Subscribe(ShowDialogPanelEvent);
        }

        private void ShowAvatarBag(ShowAvatarBagEvent evt)
        {
            this.ShowAvatarBagPanel(this.RpgMainScreen, new ShowAvatarBagPanelCommand());
        }

        private void ShowCharacterInfoPanel(ShowCharacterInfoPanelEvent evt)
        {
            this.ShowCharacterInfoPanel(this.RpgMainScreen, new ShowCharacterInfoPanelCommand());
        }

        private void ShowGongFaPanel(ShowGongFaPanelEvent evt)
        {
            this.RpgMainScreen.Execute(new ShowGongFaPanelCommand());
            //this.ShowGongFaPanel(this.RpgMainScreen, new ShowGongFaPanelCommand());
        }

        private void ShowDialogPanelEvent(ShowDialogPanelEvent evt)
        {
            this.RpgMainScreen.Execute(new ShowDialogPanelCommand());
            this.RpgMainScreen.ShowDialogPanel.Action.Invoke(new ShowDialogPanelCommand());
            //this.ShowDialogPanel(this.RpgMainScreen, new ShowDialogPanelCommand());
            Debug.Log("RpgMainScreenController:ShowDialogPanelEvent");
        }

        public override void ExitArena(RpgMainScreenViewModel viewModel, ExitArenaCommand arg)
        {
            base.ExitArena(viewModel, arg);
            Debug.Log("RpgMainScreenController:ExitArena");
            this.Publish(new ExitArenaEvent());
        }
    }
}
