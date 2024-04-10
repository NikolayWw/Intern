using CodeBase.Enemy;
using CodeBase.Player;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly AllServices _allServices;
        public GameObject Player { get; private set; }

        public GameFactory(AllServices allServices)
        {
            _allServices = allServices;
        }

        public void CreatePlayer(Vector2 at)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IInputService inputService = GetService<IInputService>();
            IPersistentProgressService persistentProgressService = GetService<IPersistentProgressService>();

            GameObject instance = Object.Instantiate(dataService.PlayerData.PlayerPrefab, at, Quaternion.identity);
            instance.GetComponent<PlayerLookAt>().Construct(inputService, persistentProgressService);
            instance.GetComponent<PlayerAttack>().Construct(inputService, persistentProgressService, dataService.PlayerData);
            Player = instance;
        }

        public void CreateEnemy(EnemyId id, Vector2 at)
        {
            IStaticDataService dataService = GetService<IStaticDataService>();
            IPersistentProgressService progressService = GetService<IPersistentProgressService>();

            EnemyConfig config = dataService.ForEnemy(id);

            GameObject instantiate = Object.Instantiate(config.Prefab, at, Quaternion.identity);
            instantiate.GetComponent<EnemyBrain>().Construct(Player.transform, progressService, config);
            instantiate.GetComponent<EnemyMove>().Construct(config);
            instantiate.GetComponent<EnemyHealth>().Construct(config, progressService);
        }

        private TService GetService<TService>() where TService : IService =>
            _allServices.Single<TService>();
    }
}