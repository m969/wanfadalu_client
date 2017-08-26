/* --------------------------
 * Company: MagicFire Studio
 *   Autor: Changmin Yang
 *   类描述: 实例化Entity的View部分，包括ObjectView以及PanelView等
 * -------------------------- */

using KBEngine;
using MagicFire.Common;
using MagicFire.Common.Plugin;
using MagicFire.Mmorpg;
using MagicFire.Mmorpg.AvatarInputState;
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
        private readonly Dictionary<Type, Dictionary<string, Object>> _typeDictionaryDictionary = new Dictionary<Type, Dictionary<string, Object>>();

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
                CreateRenderObjectView(entity);
            if (productType == typeof(EntityPanelView))
                CreateEntityPanelView(entity);
            return null;
        }

        private void CreateRenderObjectView(Entity entity)
        {
            Dictionary<string, Object> viewPrefabDictionary;

            _typeDictionaryDictionary.TryGetValue(typeof(EntityObjectView), out viewPrefabDictionary);
            if (viewPrefabDictionary == null)
            {
                viewPrefabDictionary = new Dictionary<string, Object>();
                _typeDictionaryDictionary.Add(typeof(EntityObjectView), viewPrefabDictionary);
            }

            var viewPrefab = TryGetPrefabFromViewPrefabDictionary(entity, viewPrefabDictionary);

            if (viewPrefab == null)
            {
                viewPrefab = LoadPrefabByViewType(entity);

                if (viewPrefab != null)
                    AddPrefabToViewPrefabDictionary(entity, viewPrefabDictionary, viewPrefab);
                else
                    return;
            }
            InstantiateObjectView(entity, viewPrefab);
        }

        private void InstantiateObjectView(Entity entity, Object viewPrefab)
        {
            if (viewPrefab != null)
            {
                var gameObject = Object.Instantiate(viewPrefab, entity.position, Quaternion.identity) as GameObject;
                entity.renderObj = gameObject;
                if (gameObject != null)
                {
                    gameObject.name = entity.className + ":" + entity.getDefinedProperty(EntityPropertys.EntityName);
                    var entityView = gameObject.GetComponent<EntityObjectView>();
                    entityView.InitializeView(entity as KBEngine.Model);
                    if (entity.isPlayer())
                    {
                        Object.Destroy(SingletonGather.AvatarStateController);
                        gameObject.AddComponent<AvatarStateController>();
                        SingletonGather.WorldMediator.MainAvatarView = entityView as AvatarView;
                    }
                }
            }
        }

        private void AddPrefabToViewPrefabDictionary(Entity entity, Dictionary<string, Object> viewPrefabDictionary, Object viewPrefab)
        {
            var viewType = entity.className;
            switch (viewType)
            {
                case "Avatar":
                    viewPrefabDictionary.Add(viewType, viewPrefab);
                    break;
                case "Monster":
                    viewPrefabDictionary.Add(viewType + entity.getDefinedProperty(EntityPropertys.ModelName), viewPrefab);
                    break;
                case "Npc":
                    viewPrefabDictionary.Add(viewType + entity.getDefinedProperty(EntityPropertys.ModelName), viewPrefab);
                    break;
                case "Trigger":
                    viewPrefabDictionary.Add(viewType + entity.getDefinedProperty(EntityPropertys.EntityName), viewPrefab);
                    break;
                case "Space":
                    viewPrefabDictionary.Add(viewType, viewPrefab);
                    break;
                case "SpacesManager":
                    viewPrefabDictionary.Add(viewType, viewPrefab);
                    break;
                case "Camp":
                    viewPrefabDictionary.Add(viewType, viewPrefab);
                    break;
            }
        }

        private Object TryGetPrefabFromViewPrefabDictionary(Entity entity, Dictionary<string, Object> viewPrefabDictionary)
        {
            var viewType = entity.className;
            Object viewPrefab = null;

            switch (viewType)
            {
                case "Avatar":
                    viewPrefabDictionary.TryGetValue(viewType, out viewPrefab);
                    break;
                case "Monster":
                    viewPrefabDictionary.TryGetValue(viewType + entity.getDefinedProperty(EntityPropertys.ModelName), out viewPrefab);
                    break;
                case "Npc":
                    viewPrefabDictionary.TryGetValue(viewType + entity.getDefinedProperty(EntityPropertys.ModelName), out viewPrefab);
                    break;
                case "Trigger":
                    viewPrefabDictionary.TryGetValue(viewType + entity.getDefinedProperty(EntityPropertys.EntityName), out viewPrefab);
                    break;
                case "Space":
                    viewPrefabDictionary.TryGetValue(viewType, out viewPrefab);
                    break;
                case "SpacesManager":
                    viewPrefabDictionary.TryGetValue(viewType, out viewPrefab);
                    break;
                case "Camp":
                    viewPrefabDictionary.TryGetValue(viewType, out viewPrefab);
                    break;
            }
            return viewPrefab;
        }

        private Object LoadPrefabByViewType(Entity entity)
        {
            var viewType = entity.className;
            string assetName;

            switch (viewType)
            {
                case "Avatar":

                    assetName = "Avatar";
                    return AssetTool.LoadAvatarAssetByName(assetName);

                case "Monster":

                    assetName = "" + entity.getDefinedProperty(EntityPropertys.ModelName);
                    return AssetTool.LoadMonsterAssetByName(assetName);

                case "Npc":

                    assetName = "" + entity.getDefinedProperty(EntityPropertys.ModelName);
                    return AssetTool.LoadNpcAssetByName(assetName);

                case "Trigger":

                    assetName = "" + entity.getDefinedProperty(EntityPropertys.EntityName);
                    return AssetTool.LoadTriggerAssetByName(assetName);

                case "Space":
                    break;

                case "SpacesManager":
                    break;

                case "Camp":
                    break;
            }
            return null;
        }

        private void CreateEntityPanelView(Entity entity)
        {
            if (!_entityPanelViewPrefab)
                _entityPanelViewPrefab = AssetTool.LoadUiPanelPanelsAssetByName("EntityPanel");

            if (!_entity3DPanelViewPrefab)
                _entity3DPanelViewPrefab = AssetTool.LoadUiPanelPanelsAssetByName("3DEntityPanel");

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
            var view = entityPanelObj.GetComponent<EntityPanelView>();
            view.InitializeView(entity as KBEngine.Model);

            if (entity3DPanelObj == null) return;
            var threeDView = entity3DPanelObj.GetComponent<ThreeDEntityPanelView>();
            threeDView.InitializeView(entity as KBEngine.Model);
        }
    }
}
