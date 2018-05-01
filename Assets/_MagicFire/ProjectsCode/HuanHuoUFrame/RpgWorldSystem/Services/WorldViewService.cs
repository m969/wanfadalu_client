using uFrame.MVVM.ViewModels;

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
    using uFrame.ECS.Components;
    using Newtonsoft.Json.Linq;
    using DG.Tweening;


    public class WorldViewService : WorldViewServiceBase {
        public static Dictionary<string, JObject> ConfigTableMap = new Dictionary<string, JObject>();

        [SerializeField]
        private GameObject _playerTargetPrefab;
        private SceneState _loginSceneState = SceneState.Loaded;
        private SceneState _worldSceneState = SceneState.Destructed;
        private string _currentSpaceName;
        private GameObject _masterCanvas;
        private GameObject _canvas3D;
        private GameObject _eventSystem;
        private GameObject _uiManager;
        private List<KBEngine.Entity> _entitiesPool = new List<KBEngine.Entity>();


        public GameObject MasterCanvas
        {
            get
            {
                if (_masterCanvas == null)
                {
                    _masterCanvas = GameObject.Find("MasterCanvas");
                    _eventSystem = GameObject.Find("EventSystem");
                    _uiManager = GameObject.Find("UIManager");
                    DontDestroyOnLoad(_uiManager);
                    DontDestroyOnLoad(_masterCanvas);
                    DontDestroyOnLoad(_eventSystem);
                }
                return _masterCanvas;
            }
        }

        public GameObject Canvas3D
        {
            get
            {
                if (_canvas3D == null)
                {
                    _canvas3D = GameObject.Find("3DCanvas");
                    DontDestroyOnLoad(_canvas3D);
                }
                return _canvas3D;
            }
        }

        private RpgMainPanel _rpgMainPanel;


        /// <summary>
        /// This method is invoked whenever the kernel is loading
        /// Since the kernel lives throughout the entire lifecycle  of the game, this will only be invoked once.
        /// </summary>
        public override void Setup() {
            base.Setup();
            // Use the line below to subscribe to events
            // this.OnEvent<MyEvent>().Subscribe(myEventInstance => { TODO });
            MasterCanvas.ToString();
            Canvas3D.ToString();
            LoadAllConfigTable();
            var tipsText = MasterCanvas.transform.Find("TipsText").GetComponent<UnityEngine.UI.Text>();
            this.OnEvent<ShowTipsEvent>().Subscribe(evt => { tipsText.text = evt.TipsContent; tipsText.gameObject.SetActive(true); });
            this.OnEvent<OnMainAvatarEnterSpaceEvent>().ObserveOnMainThread().Subscribe(OnMainAvatarEnterSpace);
            this.OnEvent<OnMainAvatarLeaveSpaceEvent>().ObserveOnMainThread().Subscribe(OnMainAvatarLeaveSpace);
            this.OnEvent<onEnterWorldEvent>().ObserveOnMainThread().Subscribe(OnEnterWorld);
            this.OnEvent<onLeaveWorldEvent>().ObserveOnMainThread().Subscribe(OnLeaveWorld);
            this.OnEvent<set_positionEvent>().ObserveOnMainThread().Subscribe(Set_Position);
            this.OnEvent<set_directionEvent>().ObserveOnMainThread().Subscribe(Set_Direction);
            this.OnEvent<updatePositionEvent>().ObserveOnMainThread().Subscribe(UpdatePosition);
            this.OnEvent<SceneLoaderEvent>().Where(x => x.Name == "LoginScene").Where(x => x.State == SceneState.Destructed).Subscribe(OnLoginSceneDestructed);
            this.OnEvent<SceneLoaderEvent>().Where(x => x.State == SceneState.Loaded).Subscribe(OnSceneLoaded);
            this.OnEvent<OnMatchEndCommand>().Subscribe(OnMatchEndEvent);
            Skill.InitSkillTypeMap();
            PropSystemController.InitPropConfigTable();
        }

        private void LoadAllConfigTable()
        {
            LoadConfigTable("arena_config_Table");
            LoadConfigTable("dialog_config_Table");
            LoadConfigTable("forge_config_Table");
            LoadConfigTable("gongFa_config_Table");
            LoadConfigTable("npc_config_Table");
            LoadConfigTable("prop_config_Table");
            LoadConfigTable("sect_config_Table");
            LoadConfigTable("skill_config_Table");
            LoadConfigTable("space_config_Table");
            LoadConfigTable("store_config_Table");
            LoadConfigTable("trigger_config_Table");
        }

        private JObject LoadConfigTable(string tableName)
        {
            var resPath = "JsonConfigDatas/" + tableName;
            var jsonText = Resources.Load<TextAsset>(resPath);
            var configJson = JObject.Parse(jsonText.text);
            ConfigTableMap.Add(tableName, configJson);
            return configJson;
        }

        private void OnLoginSceneDestructed(SceneLoaderEvent @event)
        {
            _loginSceneState = SceneState.Destructed;
            MasterCanvas.GetComponentInChildren<LoginPanel>().gameObject.SetActive(false);
        }

        private void OnSceneLoaded(SceneLoaderEvent @event)
        {
            var newScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(@event.Name);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(newScene);
        }

        private void OnSceneUnLoaded(SceneLoaderEvent @event)
        {
            Debug.Log("OnSceneUnLoaded = " + @event.Name);
        }

        private void OnMainAvatarEnterSpace(OnMainAvatarEnterSpaceEvent evt)
        {
            if (_loginSceneState == SceneState.Destructed)
            {
                if (_worldSceneState == SceneState.Loaded)
                {
                    this.Publish(new UnloadSceneCommand()
                    {
                        SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
                    });
                    _worldSceneState = SceneState.Destructed;
                }
                this.Publish(new LoadSceneCommand()
                {
                    SceneName = evt.SpaceName
                });
                _worldSceneState = SceneState.Loaded;
                InstantiateAllViews();
            }
            else
            {
                this.OnEvent<SceneLoaderEvent>()
                    .Where(x => x.Name == "LoginScene")
                    .Where(x => x.State == SceneState.Destructed)
                    .Subscribe(evnt => 
                    {
                        _worldSceneState = SceneState.Destructed;
                        this.Publish(new LoadSceneCommand()
                        {
                            SceneName = evt.SpaceName,
                        });
                        _worldSceneState = SceneState.Loaded;
                        InstantiateAllViews();
                    });
            }
        }

        private void OnMainAvatarLeaveSpace(OnMainAvatarLeaveSpaceEvent evt)
        {
            Debug.Log("OnMainAvatarLeaveSpace");
            KBEngine.Event.fireIn("OnLeaveSpaceClientInputInValid");
        }

        private EntityCommonView InstantiateModelView(KBEngine.Entity entity)
        {
            var viewModel = entity as EntityCommonViewModel;
            var entityType = entity.className.Replace("ViewModel", "");
            var viewPool = PoolManager.Pools[entityType + "ViewPool"];
            Transform viewPrefab = null;
            var viewName = "";
            if (entity.className == "AvatarViewModel")
                viewName = "AvatarView";
            else if (entity.className == "NpcViewModel")
                viewName = "NpcView";
            else if (entity.className == "SectViewModel")
                viewName = "SectView";
            else
                viewName = viewModel.entityName;
            viewPrefab = viewPool.prefabs[viewName];
            return viewPool.SpawnEntityCommonViewObject<EntityCommonView>(viewPrefab, viewModel);
        }

        private void InstantiateViews(KBEngine.Entity entity)
        {
            if (entity.className != "CampViewModel" && entity.className != "SpaceViewModel" && entity.renderObj == null)
            {
                var modelView = InstantiateModelView(entity);
                if (entity.isPlayer())
                {
                    var playerTarget = Instantiate(_playerTargetPrefab).GetComponent<RpgLookAtPlayerComponent>();
                    playerTarget.Target = modelView.transform;
                    var viewPool = PoolManager.Pools["UIPanelPool"];
                    if (viewPool != null)
                    {
                        var viewModel = entity as EntityCommonViewModel;
                        var viewPrefab = viewPool.prefabs["MainAvatarInfoPanelView"];
                        var mainAvatarInfoPanelView = viewPool.SpawnView<MainAvatarInfoPanelView>(viewPrefab, viewModel);
                        var moveContoller = mainAvatarInfoPanelView.GetComponent<RpgMoveController>();
                        moveContoller.MainAvatarController = modelView.GetComponent<CharacterController>();
                        moveContoller.MainAvatarView = modelView.GetComponent<AvatarView>();
                    }
                }
                if (entity.className == "AvatarViewModel")
                {
                    var skillContoller = modelView.GetComponent<RpgSkillController>();
                    skillContoller.Init(modelView as AvatarView);
                    var entityType = entity.className.Replace("ViewModel", "");
                    var viewPool = PoolManager.Pools[entityType + "ViewPool"];
                    var viewModel = entity as EntityCommonViewModel;
                    var viewPrefab = viewPool.prefabs[entityType + "PanelView"];
                    viewPool.SpawnEntityCommonView<EntityCommonView>(viewPrefab, viewModel);
                    viewPrefab = viewPool.prefabs[entityType + "RingView"];
                    viewPool.SpawnEntityCommonView<EntityCommonView>(viewPrefab, viewModel);
                }
                if (entity.className == "NpcViewModel")
                {
                    var entityType = entity.className.Replace("ViewModel", "");
                    var viewPool = PoolManager.Pools[entityType + "ViewPool"];
                    var viewModel = entity as EntityCommonViewModel;
                    var viewPrefab = viewPool.prefabs[entityType + "PanelView"];
                    viewPool.SpawnEntityCommonView<EntityCommonView>(viewPrefab, viewModel);
                }
            }
        }

        private void InstantiateAllViews()
        {
            foreach (var entity in _entitiesPool)
            {
                InstantiateViews(entity);
            }
            if (!_rpgMainPanel)
            {
                _rpgMainPanel = MasterCanvas.transform.Find("RpgMainPanel").GetComponent<RpgMainPanel>();
                _rpgMainPanel.gameObject.SetActive(true);
            }
        }

        private void OnEnterWorld(onEnterWorldEvent evt)
        {
            if (_worldSceneState != SceneState.Loaded)
            {
                _entitiesPool.Add(evt.Entity);
                return;
            }
            var entity = evt.Entity;
            InstantiateViews(entity);
        }

        private void OnLeaveWorld(onLeaveWorldEvent evt)
        {
            //var entity = evt.Entity;
            //if (entity.renderObj == null)
            //    return;

            //var viewObj = (GameObject)entity.renderObj;
            //if (viewObj.activeInHierarchy == true)
            //{
            //    viewObj.transform.parent.GetComponent<SpawnPool>().Despawn(viewObj.transform);
            //    entity.renderObj = null;
            //}
            //viewObj.GetComponent<ViewBase>().ViewModelObject = null;
        }

        private void Set_Position(set_positionEvent evt)
        {
            var entity = evt.Entity;
            if (entity.renderObj == null)
                return;
            GameObject go = ((UnityEngine.GameObject)entity.renderObj);
            Vector3 currpos = (Vector3)entity.getDefinedProperty("position");
            Debug.Log(entity.className + " Set_Position: " + currpos);
            go.transform.position = currpos;
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

        private void OnMatchEndEvent(OnMatchEndCommand cmd)
        {
            Debug.Log("WorldViewService:OnMatchEndEvent " + cmd.IsWin);
            var matchResultPanel = MasterCanvas.transform.Find("MatchResultPanel");
            //matchResultPanel.localScale = Vector3.one;
            matchResultPanel.gameObject.SetActive(true);
            if (cmd.IsWin == 1)
            {
                matchResultPanel.Find("Win").gameObject.SetActive(true);
                matchResultPanel.Find("Lose").gameObject.SetActive(false);
            }
            else
            {
                matchResultPanel.Find("Lose").gameObject.SetActive(true);
                matchResultPanel.Find("Win").gameObject.SetActive(false);
            }
            //var tweens = DOTween.TweensById("1");
            //tweens[0].Play();
        }
    }
}
