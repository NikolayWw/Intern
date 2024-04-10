using CodeBase.Logic.SpawnMarkers;
using CodeBase.Logic.SpawnMarkers.Enemy;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.SpawnData.Enemy;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public class CollectLevelDataEditor
    {
        private const string LevelStaticDataPath = "Level/LevelStaticData";

        [MenuItem("Tools/Collect Level Data")]
        private static void CollectLevelData()
        {
            LevelStaticData levelStaticData = Resources.Load<LevelStaticData>(LevelStaticDataPath);

            Vector3 playerInitialPoint = Object.FindObjectOfType<PlayerInitialPoint>().transform.position;

            levelStaticData.SetData(CollectEnemyData(), playerInitialPoint);

            EditorUtility.SetDirty(levelStaticData);
        }

        private static EnemySpawnData CollectEnemyData()
        {
            EnemySpawnMarker enemySpawnMarker = Object.FindObjectOfType<EnemySpawnMarker>();
            EnemySpawnData enemySpawnData = new EnemySpawnData(
                enemySpawnMarker.Configs,
                enemySpawnMarker.SpawnPoints.Select(x => x.position).ToArray(),
                enemySpawnMarker.DelaySpawn);

            return enemySpawnData;
        }
    }
}