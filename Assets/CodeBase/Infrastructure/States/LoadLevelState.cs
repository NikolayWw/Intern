using CodeBase.Data;
using CodeBase.Infrastructure.Logic;
using CodeBase.Services.Cleanup;
using CodeBase.Services.Factory;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Level;
using CodeBase.UI.Services.Factory;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _dataService;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly ICleanupService _cleanupService;
        private readonly ILogicFactory _logicFactory;

        public LoadLevelState(IGameStateMachine stateMachine, SceneLoader sceneLoader,
            IStaticDataService dataService,
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            ICleanupService cleanupService,
            ILogicFactory logicFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _dataService = dataService;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _cleanupService = cleanupService;
            _logicFactory = logicFactory; ;
        }

        public void Enter()
        {
            _cleanupService.Cleanup();

            if (CurrentSceneKey() == GameConstants.GameSceneKey)
                _sceneLoader.Load(GameConstants.ReloadSceneKey, () => 
                    _sceneLoader.Load(GameConstants.GameSceneKey, OnLoaded));
            else
                _sceneLoader.Load(GameConstants.GameSceneKey, OnLoaded);
        }

        public void Exit()
        { }

        private void OnLoaded()
        {
            LevelStaticData levelData = _dataService.LevelStaticData;

            _logicFactory.InitializeEnemySpawner(levelData);
            _logicFactory.InitializeShowWindowWhenPlayerLose();

            _uiFactory.CreateUIRoot();
            _uiFactory.CreateHUD();

            _gameFactory.CreatePlayer(levelData.PlayerInitialPoint);
            _logicFactory.EnemySpawner.StartSpawn();

            _stateMachine.Enter<LoopState>();
        }

        private static string CurrentSceneKey() =>
            SceneManager.GetActiveScene().name;
    }
}