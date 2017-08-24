using MagicFire.Common;

namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;

    public class Panel : MonoBehaviour
    {
        private enum LayerEnum
        {
            LayerBack,
            Layer1,
            LayerFront
        }

        [System.Serializable]
        private struct LayoutStruct
        {
            public Vector2 _anchorMin;
            public Vector2 _anchorMax;
            public Vector2 _pivot;
            public Vector2 _offsetMax;
            public Vector2 _offsetMin;
            public Vector2 _anchoredPosition;
            public Vector2 _sizeDelta;
        }

        [SerializeField]
        private LayerEnum _parentLayer;
        [SerializeField]
        private LayoutStruct _layoutStruct;

        protected int _selectItemID = -1;
        protected string _selectItemName = "";//JC

        protected virtual void Start()
        {
            switch (_parentLayer)
            {
                case LayerEnum.LayerBack:
                    transform.SetParent(SingletonGather.UiManager.CanvasLayerBack.transform);
                    break;
                case LayerEnum.Layer1:
                    transform.SetParent(SingletonGather.UiManager.CanvasLayers[1].transform);
                    break;
                case LayerEnum.LayerFront:
                    transform.SetParent(SingletonGather.UiManager.CanvasLayerFront.transform);
                    break;
            }

            transform.localScale = new Vector3(1, 1, 1);
            var rect = GetComponent<RectTransform>();

            rect.anchorMin = _layoutStruct._anchorMin;
            rect.anchorMax = _layoutStruct._anchorMax;
            rect.pivot = _layoutStruct._pivot;
            rect.offsetMax = _layoutStruct._offsetMax;
            rect.offsetMin = _layoutStruct._offsetMin;
            rect.anchoredPosition = _layoutStruct._anchoredPosition;
            rect.sizeDelta = _layoutStruct._sizeDelta;
        }

        protected virtual void Update()
        {

        }

        public virtual void Initialize()
        {
            
        }

        //伸展布局
        public virtual void StretchLayout()
        {
            transform.SetParent(SingletonGather.UiManager.CanvasLayerFront.transform);
            transform.localScale = new Vector3(1, 1, 1);
            var rect = GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.0f, 0.0f);
            rect.anchorMax = new Vector2(1, 1);
            rect.offsetMax = new Vector2(-0, 0);
            rect.offsetMin = new Vector2(0, 0);
        }

        public virtual void SetChildSelect(int childID)
        {
            _selectItemID = childID;
        }
    }

}