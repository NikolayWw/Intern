using CodeBase.Infrastructure.Logic;
using CodeBase.Services.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.SpawnData.Enemy;
using System.Collections;
using UnityEngine;

namespace CodeBase.EnemySpawner
{
    public class EnemySpawner
    {
        private readonly LevelStaticData _config;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public EnemySpawner(LevelStaticData config, ICoroutineRunner coroutineRunner, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _config = config;
            _coroutineRunner = coroutineRunner;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void StartSpawn()
        {
            _coroutineRunner.StartCoroutine(Start());
        }

        private IEnumerator Start()
        {
            EnemySpawnData enemySpawnData = _config.EnemySpawnData;
            WaitForSeconds wait = new(enemySpawnData.DelaySpawn);

            while (_progressService.PlayerProgress.Happened == false)
            {
                EnemyId id = _config.EnemySpawnData.Configs[Random.Range(0, enemySpawnData.Configs.Count)].Id;
                foreach (Vector3 position in enemySpawnData.Positions)
                {
                    if (_progressService.PlayerProgress.Happened)
                        break;

                    _gameFactory.CreateEnemy(id, position);
                }
                yield return wait;
            }
        }
    }
}