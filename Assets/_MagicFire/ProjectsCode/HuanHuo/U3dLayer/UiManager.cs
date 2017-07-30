namespace MagicFire.Common
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using System.Collections;
    using System.Collections.Generic;
    using Mmorpg.UI;
    using Common.Plugin;

    public class UiManager : MagicFire.BaseSingleton<UiManager>, IUiManager
    {
        private GameObject _canvas3D;
        private GameObject _canvas;
        private GameObject _eventSystem;
        private Dictionary<string, GameObject> _panels = new Dictionary<string, GameObject>();

        public Dictionary<string, GameObject> Panels
        {
            get
            {
                return _panels;
            }
            set
            {
                _panels = value;
            }
        }

        public GameObject Canvas3D
        {
            get
            {
                if (_canvas3D)
                    return _canvas3D;
                else
                {
                    _canvas3D = GameObject.Find("3DCanvas");
                    if (_canvas3D)
                    {
                        return _canvas3D;
                    }
                    else
                    {
                        _canvas3D = 
                            Object.Instantiate(AssetTool.LoadUiPanelPanelsAssetByName("3DCanvas"),
                                new Vector3(0, 0, 0),
                                Quaternion.identity) as GameObject;
                        if (_canvas3D != null)
                        {
                            _canvas3D.transform.eulerAngles = new Vector3(90, 0, 0);
                        }
                        return _canvas3D;
                    }
                }
            }
        }

        public GameObject Canvas
        {
            get
            {
                if (_canvas)
                    return _canvas;
                else
                {
                    _canvas = GameObject.Find("Canvas");
                    if (_canvas)
                    {
                        return _canvas;
                    }
                    else
                    {
                        _eventSystem =
                            Object.Instantiate(
                                AssetTool.LoadUiPanelPanelsAssetByName("EventSystem"),
                                new Vector3(0, 0, 0),
                                Quaternion.identity) as GameObject;

                        Instance.Canvas3D.ToString();

                        return _canvas = 
                            Object.Instantiate(
                                AssetTool.LoadUiPanelPanelsAssetByName("Canvas"),
                                new Vector3(0, 0, 0),
                                Quaternion.identity) as GameObject;
                    }
                }
            }
            set { _canvas = value; }
        }

        public GameObject CanvasLayerFront
        {
            get
            {
                var layerFront = Canvas.transform.Find("LayerFront");
                if (layerFront)
                {
                    return layerFront.gameObject;
                }
                else
                {
                    var layerObj = Instance.Canvas.CreateChildByName("LayerFront");
                    layerObj.AddComponent<RectTransform>();
                    return layerObj;
                }
            }
        }

        public GameObject CanvasLayerBack
        {
            get
            {
                var layerBack = Canvas.transform.Find("LayerBack");
                if (layerBack)
                {
                    return layerBack.gameObject;
                }
                else
                {
                    var layerObj = Instance.Canvas.CreateChildByName("LayerBack");
                    layerObj.AddComponent<RectTransform>();
                    return layerObj;
                }
            }
        }

        public readonly CanvasLayer CanvasLayers = new CanvasLayer();


        public class CanvasLayer
        {
            public GameObject this[int index]
            {
                get
                {
                    var layer = Instance.Canvas.transform.Find("Layer" + index);
                    if (layer)
                    {
                        return layer.gameObject;
                    }
                    else
                    {
                        var layerObj = Instance.Canvas.CreateChildByName("Layer" + index);
                        layerObj.AddComponent<RectTransform>();
                        return layerObj;
                    }
                }
            }
        }


        private UiManager()
        {
            mInstance = this;
        }

        public GameObject TryGetOrCreatePanel(string panelName)
        {
            GameObject tempPanel;

            if (!_panels.ContainsKey(panelName))
            {
                tempPanel = Object.Instantiate(AssetTool.LoadUiPanelPanelsAssetByName(panelName)) as GameObject;
                _panels.Add(panelName, tempPanel);
            }
            else
            {
                _panels.TryGetValue(panelName, out tempPanel);
                if (tempPanel == null)
                {
                    tempPanel = Object.Instantiate(AssetTool.LoadUiPanelPanelsAssetByName(panelName)) as GameObject;
                    _panels.Remove(panelName);
                    _panels.Add(panelName, tempPanel);
                }
            }
            if (tempPanel != null)
            {
                tempPanel.GetComponent<RectTransform>().SetAsLastSibling();
            }
            return tempPanel;
        }
    } 
}