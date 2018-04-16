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


    public class ForgePanelView : ForgePanelViewBase {
        [SerializeField]
        private Transform _materialsPanel;

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
            this.OnEvent<OnBagItemBeginDragEvent>().Subscribe(x => {
                foreach (Transform item in _materialsPanel)
                {
                    item.GetComponent<EventTrigger>().AsObservableOfPointerEnter().Subscribe(_x => {
                        Debug.Log("ForgePanelView:PointerEnter");
                        var backgroundOutline = item.Find("Background").GetComponent<Outline>();
                        backgroundOutline.effectColor = Color.yellow;
                    });
                    item.GetComponent<EventTrigger>().AsObservableOfEndDrag().Subscribe(_x => {
                        Debug.Log("ForgePanelView:EndDrag");
                        var itemImage = item.Find("Foreground").GetComponent<Image>();
                        Sprite tempType = itemImage.sprite;
                        itemImage.sprite = x.BagItem.GetComponent<Image>().sprite;
                        var itemText = item.Find("Text").GetComponent<Text>();
                    });
                }
            });
        }

        public override void OnTargetItemListReturnExecuted(OnTargetItemListReturnCommand command)
        {
        }
    }
}
