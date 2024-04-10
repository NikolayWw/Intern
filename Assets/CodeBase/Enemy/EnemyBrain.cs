using CodeBase.BehaviourTree.Tasks;
using CodeBase.BehaviourTree.Tree;
using CodeBase.Data;
using CodeBase.Infrastructure.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyBrain : TreeAI, ICoroutineRunner
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimation _animator;
        [SerializeField] private EnemyMove _move;

        private EnemyConfig _enemyConfig;
        public Transform Target { get; private set; }
        public PlayerProgress PlayerProgress { get; private set; }

        public void Construct(Transform player, IPersistentProgressService progressService, EnemyConfig enemyConfig)
        {
            Target = player;
            _enemyConfig = enemyConfig;
            PlayerProgress = progressService.PlayerProgress;

            _health.OnHappened += Happened;
            PlayerProgress.OnHappened += PlayerDie;
        }

        private void OnDestroy()
        {
            PlayerProgress.OnHappened -= PlayerDie;
        }

        public override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new AttackTask(transform, _enemyConfig.AttackDelay, _health, _enemyConfig.ApplyDamageDelay, this, _enemyConfig.Damage, _move, this, _enemyConfig.AttackDistance, _animator),
                }),
                new Sequence(new List<Node>
                {
                    new FollowTask(transform, this, _move, _animator),
                }),
            });
            return root;
        }

        private void PlayerDie()
        {
            enabled = false;
            _move.PlayerDie();
            _animator.StopMove();
        }

        private void Happened()
        {
            enabled = false;
        }
    }
}