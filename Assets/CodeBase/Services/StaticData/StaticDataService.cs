using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Player;
using CodeBase.StaticData.Windows;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string WindowStaticDataPath = "Windows/WindowStaticData";
        private const string PlayerStaticDataPath = "Player/PlayerStaticData";
        private const string EnemyStaticDataPath = "Enemy/EnemyStaticData";
        private const string LevelStaticDataPath = "Level/LevelStaticData";

        public WindowStaticData WindowData { get; private set; }
        public PlayerStaticData PlayerData { get; private set; }
        public LevelStaticData LevelStaticData { get; private set; }
        private Dictionary<EnemyId, EnemyConfig> _enemyConfigs;


        public StaticDataService()
        {
            Load();
        }


        public EnemyConfig ForEnemy(EnemyId id) =>
            _enemyConfigs.TryGetValue(id, out EnemyConfig cfg) ? cfg : null;

        private void Load()
        {
            PlayerData = Resources.Load<PlayerStaticData>(PlayerStaticDataPath);
            _enemyConfigs = Resources.Load<EnemyStaticData>(EnemyStaticDataPath).EnemyConfigs.ToDictionary(x => x.Id, x => x);
            LevelStaticData = Resources.Load<LevelStaticData>(LevelStaticDataPath);
            WindowData = Resources.Load<WindowStaticData>(WindowStaticDataPath);
        }
    }
}