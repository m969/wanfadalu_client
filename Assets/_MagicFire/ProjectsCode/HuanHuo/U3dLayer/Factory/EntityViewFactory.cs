/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *   类描述: 实例化Entity的View部分，包括ObjectView以及PanelView等
 * -------------------------- */

using KBEngine;
using MagicFire.Common;
using MagicFire.Common.Plugin;
using MagicFire.Mmorpg;
using MagicFire.Mmorpg.UI;

namespace MagicFire {
    using System;
    using System.Reflection;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// 这个类用来实例化Entity的View部分，包括ObjectView以及PanelView等
    /// </summary>
    public class EntityViewFactory : IBaseFactory
    {
        private Object _entityPanelViewPrefab;
        private Object _entity3DPanelViewPrefab;
        private readonly Dictionary<Type, Dictionary<string, Object>> _products = new Dictionary<Type, Dictionary<string, Object>>();

        public TProductType CreateProduct<TProductType>(params object[] productParameters)
        {
            return (TProductType)CreateProduct(typeof(TProductType), productParameters);
        }

        public object CreateProduct(Type productType, params object[] productParameters)
        {
            var entity = productParameters[0] as Entity;
            if (entity == null)
                return null;
            if (productType == typeof(EntityObjectView))
                return CreateRenderObjectView(entity, entity.className);
            if (productType == typeof(EntityPanelView))
                CreateEntityPanelView(entity);
            return null;
        }

        private EntityObjectView CreateRenderObjectView(Entity entity, string viewType)
        {
            Dictionary<string, Object> typeProducts;
            _products.TryGetValue(typeof(EntityObjectView), out typeProducts);
            if (typeProducts == null)
            {
                typeProducts = new Dictionary<string, Object>();
                _products.Add(typeof(EntityObjectView), typeProducts);
            }
            Object productPrefab;

            switch (viewType)
            {
                case "Avatar":
                    typeProducts.TryGetValue(viewType, out productPrefab);
                    break;
                case "Monster":
                    typeProducts.TryGetValue(viewType + entity.getDefinedProperty("modelName"), out productPrefab);
                    break;
                case "Npc":
                    typeProducts.TryGetValue(viewType + entity.getDefinedProperty("modelName"), out productPrefab);
                    break;
                case "Trigger":
                    typeProducts.TryGetValue(viewType, out productPrefab);
                    break;
                default:
                    productPrefab = null;
                    break;
            }

            if (productPrefab == null)
            {
                var productPrefabPath = "";
                var databasePath = "";
                var bundlePath = "Prefabs";
                var bundleName = "";
                var assetName = "";

                switch (viewType)
                {
                    case "Avatar":
                        productPrefabPath = "Player/Avatar";
                        bundleName = "player_bundle";
                        assetName = "Avatar";
                        break;
                    case "Monster":
                        productPrefabPath = "Monster/" + entity.getDefinedProperty("modelName");
                        bundleName = "monster_bundle";
                        assetName = "" + entity.getDefinedProperty("modelName");
                        break;
                    case "Npc":
                        productPrefabPath = "Npc/" + entity.getDefinedProperty("modelName");
                        bundleName = "npc_bundle";
                        assetName = "" + entity.getDefinedProperty("modelName");
                        break;
                    case "Trigger":
                        productPrefabPath = "Trigger/Trigger";
                        bundleName = "trigger_bundle";
                        assetName = "Trigger";
                        break;
                }
                databasePath = AssetTool.Assets__Prefabs_ + productPrefabPath + ".prefab";
                productPrefab = AssetTool.LoadAsset_Database_Or_Bundle(databasePath, bundlePath, bundleName, assetName);
                if (productPrefab != null)
                {
                    switch (viewType)
                    {
                        case "Avatar":
                            typeProducts.Add(viewType, productPrefab);
                            break;
                        case "Monster":
                            typeProducts.Add(viewType + entity.getDefinedProperty("modelName"), productPrefab);
                            break;
                        case "Npc":
                            typeProducts.Add(viewType + entity.getDefinedProperty("modelName"), productPrefab);
                            break;
                        case "Trigger":
                            typeProducts.Add(viewType, productPrefab);
                            break;
                        default:
                            productPrefab = null;
                            break;
                    }
                }
                else
                {
                    Debug.LogError(entity.className + " " + entity.getDefinedProperty("entityName") + " no " + entity.getDefinedProperty("modelName") + " prefab!");
                    return null;
                }
            }
            EntityObjectView entityView = null;
            if (productPrefab != null)
            {
                var gameObject = Object.Instantiate(productPrefab, entity.position, Quaternion.identity) as GameObject;
                entity.renderObj = gameObject;
                if (gameObject != null)
                {
                    gameObject.name = entity.className + ":" + entity.getDefinedProperty("entityName");
                    entityView = gameObject.GetComponent<EntityObjectView>();
                    entityView.InitializeView(entity as KBEngine.Model);
                    if (entity.isPlayer())
                    {
                        SingletonGather.WorldMediator.MainAvatarView = entityView as AvatarView;
                        gameObject.AddComponent<PlayerInputController>();
                    }
                }
            }
            return entityView;
        }

        private void CreateEntityPanelView(Entity entity)
        {
            if (!_entityPanelViewPrefab)
            {
                _entityPanelViewPrefab = 
                    AssetTool.LoadAsset_Database_Or_Bundle(
                        AssetTool.Assets__Prefabs_UIPanel_Panels_ + "EntityPanel.prefab",
                        "Prefabs",
                        "uipanel_bundle",
                        "EntityPanel");
                if (_entityPanelViewPrefab == null)
                {
                    Debug.LogError("_entityPanelViewPrefab == null!");
                    return;
                }
            }

            if (!_entity3DPanelViewPrefab)
            {
                _entity3DPanelViewPrefab =
                    AssetTool.LoadAsset_Database_Or_Bundle(
                        AssetTool.Assets__Prefabs_UIPanel_Panels_ + "3DEntityPanel.prefab",
                        "Prefabs",
                        "uipanel_bundle",
                        "3DEntityPanel");
                if (_entity3DPanelViewPrefab == null)
                {
                    Debug.LogError("_entity3DPanelViewPrefab == null!");
                    return;
                }
            }

            var entityPanelPosition = Camera.main.WorldToScreenPoint(entity.position);
            entityPanelPosition = new Vector3(entityPanelPosition.x, entityPanelPosition.y, 0);
            var entity3DPanelPosition = new Vector3(entity.position.x, entity.position.z, -1);
            GameObject entityPanelObj;
            GameObject entity3DPanelObj;
            switch (entity.className)
            {
                case "Avatar":

                    entityPanelObj = Object.Instantiate(_entityPanelViewPrefab) as GameObject;
                    if (entityPanelObj != null) entityPanelObj.AddComponent<AvatarPanelView>();

                    entity3DPanelObj = Object.Instantiate(_entity3DPanelViewPrefab) as GameObject;
                    if (entity3DPanelObj != null) entity3DPanelObj.AddComponent<AvatarThreeDPanelView>();

                    break;
                case "Monster":

                    entityPanelObj = Object.Instantiate(_entityPanelViewPrefab) as GameObject;
                    if (entityPanelObj != null) entityPanelObj.AddComponent<MonsterPanelView>();

                    entity3DPanelObj = Object.Instantiate(_entity3DPanelViewPrefab) as GameObject;
                    if (entity3DPanelObj != null) entity3DPanelObj.AddComponent<MonsterThreeDPanelView>();

                    break;
                case "Npc":

                    entityPanelObj = Object.Instantiate(_entityPanelViewPrefab) as GameObject;
                    if (entityPanelObj != null) entityPanelObj.AddComponent<NpcPanelView>();

                    entity3DPanelObj = Object.Instantiate(_entity3DPanelViewPrefab) as GameObject;
                    if (entity3DPanelObj != null) entity3DPanelObj.AddComponent<NpcThreeDPanelView>();

                    break;
                default:
                    return;
            }

            if (entityPanelObj == null) return;
            entityPanelObj.transform.SetParent(SingletonGather.UiManager.CanvasLayerBack.transform);
            entityPanelObj.transform.localPosition = entityPanelPosition;
            entityPanelObj.transform.localEulerAngles = Vector3.zero;
            var view = entityPanelObj.GetComponent<EntityPanelView>();
            view.InitializeView(entity as KBEngine.Model);

            if (entity3DPanelObj == null) return;
            entity3DPanelObj.transform.SetParent(SingletonGather.UiManager.Canvas3D.transform);
            entity3DPanelObj.transform.localPosition = entity3DPanelPosition;
            entity3DPanelObj.transform.localEulerAngles = Vector3.zero;
            var threeDView = entity3DPanelObj.GetComponent<ThreeDEntityPanelView>();
            threeDView.InitializeView(entity as KBEngine.Model);
        }
    }
}
