using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Player;
using CodeBase.StaticData.Windows;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        WindowStaticData WindowData { get; }
        PlayerStaticData PlayerData { get; }
        LevelStaticData LevelStaticData { get; }


        EnemyConfig ForEnemy(EnemyId id);
    }
}