using CodeBase.Data;
using CodeBase.Logic.Health;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Enemy;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IApplyDamage
    {
        [SerializeField] private Collider2D[] _disableColliders;
        [SerializeField] private EnemyAnimation _animation;

        private EnemyConfig _config;
        private PlayerProgress _playerProgress;

        public Action OnHappened;

        public void Construct(EnemyConfig config, IPersistentProgressService progressService)
        {
            _config = config;
            _playerProgress = progressService.PlayerProgress;
        }

        public void ApplyDamage(float damage)
        {
            OnHappened?.Invoke();
            _animation.PlayDie();
            _playerProgress.IncrementKillCount();

            foreach (Collider2D collider2d in _disableColliders)
                collider2d.enabled = false;
            StartCoroutine(DelayRemove());
        }

        private IEnumerator DelayRemove()
        {
            yield return new WaitForSeconds(_config.DelayRemove);
            Destroy(gameObject);
        }
    }
}