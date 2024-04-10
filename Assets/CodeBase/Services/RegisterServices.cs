using CodeBase.Data;
using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Cleanup;
using CodeBase.Services.Factory;
using CodeBase.Services.Input;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Services
{
    public class RegisterServices
    {
        public RegisterServices(ICoroutineRunner coroutineRunner)
        {
            Register(coroutineRunner);
        }

        private void Register(ICoroutineRunner coroutineRunner)
        {
            AllServices services = AllServices.Container;
            SceneLoader sceneLoader = new(coroutineRunner);

            services.RegisterSingle<IStaticDataService>(new StaticDataService());
            services.RegisterSingle<IGameFactory>(new GameFactory(services));
            services.RegisterSingle<ILogicFactory>(new LogicFactory.LogicFactory(services, coroutineRunner));
            services.RegisterSingle<IUIFactory>(new UIFactory(services));
            RegisterPersistentProgress(services);
            services.RegisterSingle<IInputService>(new InputService());

            services.RegisterSingle<ICleanupService>(new CleanupService(services.Single<ILogicFactory>(),
                services.Single<IPersistentProgressService>(),
                services.Single<IStaticDataService>()));

            services.RegisterSingle<IGameStateMachine>(new GameStateMachine(sceneLoader, services, coroutineRunner));
        }

        private void RegisterPersistentProgress(AllServices services)
        {
            float startHealth = services.Single<IStaticDataService>().PlayerData.StartHealth;
            PersistentProgressService progressService = new();
            progressService.PlayerProgress = new PlayerProgress(startHealth);
            services.RegisterSingle<IPersistentProgressService>(progressService);
        }
    }
}