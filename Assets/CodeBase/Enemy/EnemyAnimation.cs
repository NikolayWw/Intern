using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        private readonly int MoveBlendHash = Animator.StringToHash("MoveBlend");
        private readonly int AttackHash = Animator.StringToHash("Attack");
        private readonly int DieHash = Animator.StringToHash("Die");

        private const int IdleId = 0;
        private const int MoveId = 1;

        [SerializeField] private Animator _animator;

        public void PlayAttack()
        {
            _animator.Play(AttackHash, 0, 0);
        }

        public void PlayMove()
        {
            _animator.SetFloat(MoveBlendHash, MoveId);
        }

        public void StopMove()
        {
            _animator.SetFloat(MoveBlendHash, IdleId);
        }

        public void PlayDie()
        {
            _animator.Play(DieHash, 0, 0);
        }
    }
}