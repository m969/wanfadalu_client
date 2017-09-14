namespace MagicFire.HuanHuoUFrame{
    using HuanHuoUFrame;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.MVVM;
    using UniRx;
    using UnityEngine;
    using PathologicalGames;
    using uFrame.MVVM.Views;
    
    
    public class WorldViewService : WorldViewServiceBase {

        private SceneState _worldSceneState = SceneState.Destructed;
        private SceneState _loginSceneState = SceneState.Loaded;

        /// <summary>
        /// This method is invoked whenever the kernel is loading
        /// Since the kernel lives throughout the entire lifecycle  of the game, this will only be invoked once.
        /// </summary>
        public override void Setup() {
            base.Setup();
            // Use the line below to subscribe to events
            // this.OnEvent<MyEvent>().Subscribe(myEventInstance => { TODO });

            this.OnEvent<OnMainAvatarEnterWorldEvent>().ObserveOnMainThread().Subscribe(OnMainAvatarEnterWorld);

            this.OnEvent<OnMainAvatarEnterSpaceEvent>().ObserveOnMainThread().Subscribe(OnMainAvatarEnterSpace);
            this.OnEvent<OnMainAvatarLeaveSpaceEvent>().ObserveOnMainThread().Subscribe(OnMainAvatarLeaveSpace);

            this.OnEvent<onEnterWorldEvent>().ObserveOnMainThread().Subscribe(OnEnterWorld);
            this.OnEvent<onLeaveWorldEvent>().ObserveOnMainThread().Subscribe(OnLeaveWorld);
            this.OnEvent<set_positionEvent>().ObserveOnMainThread().Subscribe(Set_Position);
            this.OnEvent<set_directionEvent>().ObserveOnMainThread().Subscribe(Set_Direction);
            this.OnEvent<updatePositionEvent>().ObserveOnMainThread().Subscribe(UpdatePosition);

            this.OnEvent<SceneLoaderEvent>()
                .Where(x => x.Name == "LoginScene")
                .Where(x => x.State == SceneState.Destructed)
                .Subscribe(evt =>
                {
                    _loginSceneState = SceneState.Destructed;
                });
        }

        private void OnMainAvatarEnterWorld(OnMainAvatarEnterWorldEvent evt)
        {
            //var avatar = evt.Entity;
            //if (!avatar.isPlayer())
            //    return;
            //InstantiateMainAvatarView();
        }

        private void OnMainAvatarEnterSpace(OnMainAvatarEnterSpaceEvent evt)
        {
            Debug.Log("OnMainAvatarEnterSpace");

            if (_loginSceneState == SceneState.Destructed)
            {
                this.Publish(new LoadSceneCommand()
                {
                    SceneName = evt.SpaceName,
                    RestrictToSingleScene = true
                });
                InstantiateAllViews();
            }
            else
                this.OnEvent<SceneLoaderEvent>()
                    .Where(x => x.Name == "LoginScene")
                    .Where(x => x.State == SceneState.Destructed)
                    .Subscribe(evnt => 
                    {
                        this.Publish(new LoadSceneCommand()
                        {
                            SceneName = evt.SpaceName,
                            RestrictToSingleScene = true
                        });
                        InstantiateAllViews();
                    });
        }

        private void OnMainAvatarLeaveSpace(OnMainAvatarLeaveSpaceEvent evt)
        {
            Debug.Log("OnMainAvatarLeaveSpace");
            KBEngine.Event.fireIn("OnLeaveSpaceClientInputInValid");
        }

        private void OnEnterWorld(onEnterWorldEvent evt)
        {
            //var entity = evt.Entity;
            //if (entity.isPlayer())
            //{
            //    CreatePlayer();
            //}
            //else
            //{
            //    UnityEngine.GameObject entityPerfab = null;

            //    float layer = 0.0f;
            //    if (entity.className == "FoodViewModel")
            //    {
            //        entityPerfab = foodsPerfab;
            //        layer = 100.0f;
            //    }
            //    else if (entity.className == "SmashViewModel")
            //    {
            //        layer = -9.0f;

            //        // 粉碎永远都应该在所有avatar和粮食的上面一层
            //        entityPerfab = smashsPerfab;
            //    }
            //    else
            //    {
            //        entityPerfab = avatarPerfab;
            //    }

            //    var cmd = new InstantiateViewCommand()
            //    {
            //        ViewModelObject = entity as ViewModel,
            //        Prefab = entityPerfab
            //    };
            //    this.Publish(cmd);
            //    entity.renderObj = cmd.Result.gameObject;

            //    ((UnityEngine.GameObject)entity.renderObj).name = entity.className + "_" + entity.id;
            //    SetPosition(new setpositionEvent() { Entity = entity });
            //    SetDirection(new setdirectionEvent() { Entity = entity });

            //    if (entity.className == "AvatarViewModel")
            //    {
            //        ((UnityEngine.GameObject)entity.renderObj).GetComponent<EntityCommonView>().isAvatar = true;
            //    }
            //}
        }

        private void OnLeaveWorld(onLeaveWorldEvent evt)
        {
            var entity = evt.Entity;
            if (entity.renderObj == null)
                return;

            UnityEngine.GameObject.Destroy((UnityEngine.GameObject)entity.renderObj);
            entity.renderObj = null;
        }

        private void Set_Position(set_positionEvent evt)
        {
            //var entity = evt.Entity;
            //if (entity.renderObj == null)
            //    return;

            //GameObject go = ((UnityEngine.GameObject)entity.renderObj);
            //Vector3 currpos = new Vector3(entity.position.x, entity.position.z, go.transform.position.z);
            //go.GetComponent<EntityCommonView>().destPosition = currpos;
            //go.GetComponent<EntityCommonView>().position = currpos;
        }

        private void Set_Direction(set_directionEvent evt)
        {
            //var entity = evt.Entity;
            //if (entity.renderObj == null)
            //    return;

            //((UnityEngine.GameObject)entity.renderObj).GetComponent<EntityCommonView>().destDirection =
            //    new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
        }

        private void UpdatePosition(updatePositionEvent evt)
        {
            //var entity = evt.Entity;

            //if (entity.renderObj == null)
            //    return;

            //EntityCommonView EntityCommonView = ((UnityEngine.GameObject)entity.renderObj).GetComponent<EntityCommonView>();
            //GameObject go = ((UnityEngine.GameObject)entity.renderObj);
            //EntityCommonView.destPosition = new Vector3(entity.position.x, entity.position.z, go.transform.position.z);
        }

        private void InstantiateAllViews()
        {
            Debug.Log("InstantiateAllViews");
            InstantiateMainAvatarView();

            var queryResults = KBEngine.KBEngineApp.app.entities.Values.Where(x => x.inWorld && x.renderObj == null).ToObservable();

            queryResults.ObserveOnMainThread().Subscribe(entity => 
            {
                Debug.Log("queryResults Instantiate entity");
            });

            var results = KBEngine.KBEngineApp.app.entities.Values.Where(x => x.inWorld && x.renderObj == null).ToArray();

            results.ToObservable().Subscribe(entity =>
            {
                Debug.Log("results Instantiate " + entity.className);
                var viewType = entity.className.Replace("ViewModel", "View");
                var viewPool = PoolManager.Pools[viewType + "Pool"];
            });

            foreach (var item in results)
            {
                
            }
        }

        private void InstantiateMainAvatarView()
        {
            var avatarViewModel = KBEngine.KBEngineApp.app.player() as AvatarViewModel;
            if (avatarViewModel == null)
                return;

            var viewPool = PoolManager.Pools["AvatarViewPool"];
            var mainAvatarView = viewPool.SpawnView(viewPool.prefabPools["AvatarView"].prefab, avatarViewModel) as AvatarView;
            mainAvatarView.InitializeView(avatarViewModel);
            mainAvatarView.gameObject.AddComponent<MagicFire.Mmorpg.AvatarInputState.AvatarStateController>();
            //MagicFire.Mmorpg.AvatarInputState.AvatarStateController.Instance.gameObject.SetActive(true);
            MagicFire.Mmorpg.PlayerTarget.Instance.ToString();
        }
    }
}
