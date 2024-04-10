using CodeBase.Data;
using CodeBase.Logic.Health;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData.Player;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerAnimation _animation;
        [SerializeField] private Transform _attackPoint;

        private IInputService _inputService;
        private PlayerStaticData _playerStaticData;
        private PlayerProgress _progress;

        private float _currentTime;
        private bool _toggle;

        public void Construct(IInputService inputService, IPersistentProgressService persistentProgressService, PlayerStaticData playerStaticData)
        {
            _inputService = inputService;
            _progress = persistentProgressService.PlayerProgress;
            _playerStaticData = playerStaticData;
            _currentTime = playerStaticData.DelayBeforeAttack;
            _progress.OnHappened += DisableThis;
        }

        private void OnDestroy()
        {
            _progress.OnHappened -= DisableThis;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_inputService.Hit != 0 && _toggle)
            {
                if (_currentTime >= _playerStaticData.DelayBeforeAttack)
                {
                    _toggle = false;
                    _currentTime = 0;
                    Hit();
                }
            }
            else if (_inputService.Hit == 0 && _toggle == false)
            {
                _toggle = true;
            }
        }

        private void Hit()
        {
            _animation.PlayAttack();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.position, _playerStaticData.AttackRadius);

            foreach (Collider2D collision in colliders)
            {
                if (collision.isTrigger)
                    continue;

                Rigidbody2D attachedRigidbody = collision.attachedRigidbody;
                if (attachedRigidbody != null && attachedRigidbody.TryGetComponent(out IApplyDamage apply))
                {
                    apply.ApplyDamage(_playerStaticData.Damage);
                    break;
                }
            }
        }

        private void DisableThis()
        {
            enabled = false;
        }
    }
}