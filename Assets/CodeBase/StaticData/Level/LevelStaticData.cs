using CodeBase.StaticData.SpawnData.Enemy;
using UnityEngine;

namespace CodeBase.StaticData.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level Static Data", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public EnemySpawnData EnemySpawnData;
        [field: SerializeField] public Vector2 PlayerInitialPoint { get; private set; }

        public void SetData(EnemySpawnData enemySpawnData, Vector2 playerInitialPoint)
        {
            EnemySpawnData = enemySpawnData;
            PlayerInitialPoint = playerInitialPoint;
        }

        private void OnValidate()
        {
            EnemySpawnData.Configs.ForEach(x => x.OnValidate());
        }
    }
}