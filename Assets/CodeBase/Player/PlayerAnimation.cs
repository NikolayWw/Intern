using System;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private readonly int AttackHash = Animator.StringToHash("Attack");
        [SerializeField] private Animator _animator;

        public void PlayAttack()
        {
            _animator.Play(AttackHash);
        }
    }
}