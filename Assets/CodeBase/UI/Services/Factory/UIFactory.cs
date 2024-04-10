using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Windows.Hud.KillCountWindow;
using CodeBase.UI.Windows.Hud.PlayerHealth;
using CodeBase.UI.Windows.LoseWindow;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private Transform _uiRoot;
        private readonly AllServices _allServices;

        public UIFactory(AllServices allServices)
        {
            _allServices = allServices;
        }

        public void CreateUIRoot()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            _uiRoot = Object.Instantiate(dataService.WindowData.UIRoot).transform;
        }

        public void CreateHUD()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IPersistentProgressService progressService = GetService<IPersistentProgressService>();

            GameObject prefab = dataService.WindowData.HudPrefab;
            GameObject instantiate = Object.Instantiate(prefab, _uiRoot);
            instantiate.GetComponentInChildren<KillCountWindow>().Construct(progressService);
            instantiate.GetComponentInChildren<PlayerHealthWindow>().Construct(progressService, dataService);
        }

        public void CreateLoseWindow()
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IGameStateMachine gameStateMachine = GetService<IGameStateMachine>();
            IPersistentProgressService progressService = GetService<IPersistentProgressService>();

            LoseWindow prefab = dataService.WindowData.LoseWindowPrefab;
            LoseWindow loseWindow = Object.Instantiate(prefab, _uiRoot);
            loseWindow.Construct(gameStateMachine, progressService);
        }

        private TService GetService<TService>() where TService : IService =>
            _allServices.Single<TService>();
    }
}