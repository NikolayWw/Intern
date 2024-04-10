using CodeBase.StaticData.SpawnData.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.SpawnMarkers.Enemy
{
    public class EnemySpawnMarker : MonoBehaviour
    {
        public List<EnemySpawnConfig> Configs;
        public List<Transform> SpawnPoints;
        [field: SerializeField] public float DelaySpawn { get; private set; } = 1f;

        private void OnValidate()
        {
            Configs.ForEach(x => x.OnValidate());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (Transform point in SpawnPoints)
            {
                Gizmos.DrawSphere(point.position, 0.5f);
            }
        }
    }
}