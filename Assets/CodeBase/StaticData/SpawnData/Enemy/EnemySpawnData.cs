using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.SpawnData.Enemy
{
    [Serializable]
    public class EnemySpawnData
    {
        public List<EnemySpawnConfig> Configs;
        [field: SerializeField] public Vector3[] Positions { get; private set; }
        [field: SerializeField] public float DelaySpawn { get; private set; } = 1f;

        public EnemySpawnData(List<EnemySpawnConfig> configs, Vector3[] positions, float delaySpawn)
        {
            Configs = configs;
            Positions = positions;
            DelaySpawn = delaySpawn;
        }

        public void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }
    }
}