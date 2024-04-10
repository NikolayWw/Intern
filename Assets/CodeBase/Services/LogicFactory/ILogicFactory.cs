using CodeBase.Logic.Infrastructure;
using CodeBase.StaticData.Level;

namespace CodeBase.Services.LogicFactory
{
    public interface ILogicFactory : IService
    {
        EnemySpawner.EnemySpawner EnemySpawner { get; }
        ShowWindowWhenPlayerLose ShowWindowWhenPlayerLose { get; }

        void Cleanup();

        void InitializeEnemySpawner(LevelStaticData levelStaticData);
        void InitializeShowWindowWhenPlayerLose();
    }
}