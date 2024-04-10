using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public interface IGameFactory : IService
    {
        void CreatePlayer(Vector2 at);
        void CreateEnemy(EnemyId id, Vector2 at);
        GameObject Player { get; }
    }
}