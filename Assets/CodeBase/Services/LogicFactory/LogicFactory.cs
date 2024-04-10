using CodeBase.Infrastructure.Logic;
using CodeBase.Logic.Infrastructure;
using CodeBase.Services.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Level;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Services.LogicFactory
{
    public class LogicFactory : ILogicFactory
    {
        private readonly AllServices _allServices;
        private readonly ICoroutineRunner _coroutineRunner;
        public EnemySpawner.EnemySpawner EnemySpawner { get; private set; }
        public ShowWindowWhenPlayerLose ShowWindowWhenPlayerLose { get; private set; }

        public LogicFactory(AllServices allServices, ICoroutineRunner coroutineRunner)
        {
            _allServices = allServices;
            _coroutineRunner = coroutineRunner;
        }

        public void Cleanup()
        {
            EnemySpawner = null;

            ShowWindowWhenPlayerLose?.Cleanup();
            ShowWindowWhenPlayerLose = null;
        }

        public void InitializeShowWindowWhenPlayerLose()
        {
            IUIFactory uiFactory = GetService<IUIFactory>();
            IPersistentProgressService progressService = GetService<IPersistentProgressService>();

            ShowWindowWhenPlayerLose = new ShowWindowWhenPlayerLose(progressService, uiFactory, _coroutineRunner);
        }

        public void InitializeEnemySpawner(LevelStaticData levelStaticData)
        {
            IGameFactory gameFactory = GetService<IGameFactory>();
            IPersistentProgressService persistentProgressService = GetService<IPersistentProgressService>();

            EnemySpawner = new EnemySpawner.EnemySpawner(levelStaticData, _coroutineRunner, gameFactory, persistentProgressService);
        }

        private TService GetService<TService>() where TService : IService =>
            _allServices.Single<TService>();
    }
}