using System;
using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.StaticData.SpawnData.Enemy
{
    [Serializable]
    public class EnemySpawnConfig
    {
        [SerializeField] private string _inspectorName;
        [field: SerializeField] public EnemyId Id { get; private set; }

        public void OnValidate() =>
            _inspectorName = Id.ToString();
    }
}