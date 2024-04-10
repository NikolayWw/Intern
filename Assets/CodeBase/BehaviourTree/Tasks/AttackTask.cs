using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Logic;
using CodeBase.Logic.CustomTimer;
using UnityEngine;

namespace CodeBase.BehaviourTree.Tasks
{
    public class AttackTask : Node
    {
        private readonly float _attackDelay;
        private readonly float _applyDamageDelay;
        private readonly CustomTimer _timer;
        private readonly float _damage;
        private readonly EnemyMove _move;
        private readonly EnemyBrain _enemyBrain;
        private readonly Transform _transform;
        private readonly float _attackDistance;
        private readonly EnemyAnimation _animation;

        private float _currentDelay;

        public AttackTask(Transform transform, float attackDelay, EnemyHealth enemyHealth, float applyDamageDelay, ICoroutineRunner coroutineRunner, float damage, EnemyMove enemyMove, EnemyBrain enemyBrain, float attackDistance, EnemyAnimation animation)
        {
            _timer = new CustomTimer(coroutineRunner);
            _transform = transform;
            _attackDelay = attackDelay + Random.Range(-0.2f, 0.2f);//extra offset;
            _applyDamageDelay = applyDamageDelay;
            _damage = damage;
            _move = enemyMove;
            _enemyBrain = enemyBrain;
            _attackDistance = attackDistance + Random.Range(-0.4f, 0.1f);//extra offset
            _animation = animation;
            _currentDelay = attackDelay;

            enemyHealth.OnHappened += _timer.Stop;
        }

        public override bool Evaluate()
        {
            if (Vector3.Distance(_transform.position, _enemyBrain.Target.position) > _attackDistance)
            {
                return false;
            }
            _move.StopMove();
            _animation.StopMove();

            _currentDelay += Time.deltaTime;
            if (_currentDelay >= _attackDelay)
            {
                _animation.PlayAttack();
                _timer.Stop();
                _timer.Start(_applyDamageDelay, ApplyDamage);
                _currentDelay = 0;
            }

            return true;
        }

        private void ApplyDamage()
        {
            _enemyBrain.PlayerProgress.DecrementHealth(_damage);
        }
    }
}