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
    using UniRx.Triggers;

    [RequireComponent(typeof(RectTransform))]
    public class PanelView : PanelViewBase {
        [SerializeField]
        private Image _headBar;
        private Vector3 _lastPosition;

        protected override void InitializeViewModel(uFrame.MVVM.ViewModels.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as PanelViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Panel to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_headBar == null)
                return;
            _headBar.GetComponent<Image>().OnBeginDragAsObservable().Subscribe(evt =>
            {
                Vector3 currentPosition;

                RectTransformUtility.ScreenPointToWorldPointInRectangle(this.GetComponent<RectTransform>(),
                    evt.position, evt.pressEventCamera, out currentPosition);

                var v = GameObject.Find("MasterCanvas").GetComponent<RectTransform>().rect.size / 2;
                _lastPosition = currentPosition - new Vector3(v.x, v.y) - this.transform.localPosition;
            }).DisposeWith(this);

            _headBar.GetComponent<Image>().OnDragAsObservable().Subscribe(evt =>
            {
                Vector3 currentPosition;

                RectTransformUtility.ScreenPointToWorldPointInRectangle(this.GetComponent<RectTransform>(),
                    evt.position, evt.pressEventCamera, out currentPosition);

                var point = this.transform.localPosition;
                var v = GameObject.Find("MasterCanvas").GetComponent<RectTransform>().rect.size / 2;
                this.transform.localPosition = currentPosition - new Vector3(v.x, v.y) - _lastPosition;

            }).DisposeWith(this);
        }
    }
}
