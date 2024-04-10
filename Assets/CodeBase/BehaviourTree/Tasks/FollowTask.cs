using CodeBase.BehaviourTree.Tree;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.BehaviourTree.Tasks
{
    public class FollowTask : Node
    {
        private readonly EnemyBrain _enemyBrain;
        private readonly EnemyMove _move;
        private readonly EnemyAnimation _animation;
        private readonly Transform _transform;
        private readonly bool _isRightDirection;

        public FollowTask(Transform transform, EnemyBrain enemyBrain, EnemyMove move, EnemyAnimation animation)
        {
            _transform = transform;
            _enemyBrain = enemyBrain;
            _move = move;
            _animation = animation;

            _isRightDirection = _enemyBrain.Target.position.x - _transform.position.x > 0;
        }

        public override bool Evaluate()
        {
            _animation.PlayMove();
            _move.Move(_isRightDirection);
            return true;
        }
    }
}