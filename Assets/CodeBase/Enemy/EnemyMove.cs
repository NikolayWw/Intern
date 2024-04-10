using CodeBase.StaticData.Enemy;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private EnemyHealth _health;

        private EnemyConfig _config;
        private bool _canMove;
        private bool _isRight;
        private float _startScaleX;

        public void Construct(EnemyConfig config)
        {
            _config = config;
            _startScaleX = _bodyTransform.localScale.x;
            _health.OnHappened += Happened;
        }

        private void FixedUpdate()
        {
            UpdateMove();
        }

        public void Move(bool isRight)
        {
            _canMove = true;
            _isRight = isRight;
            UpdateDirectionBody(isRight);
        }

        public void StopMove()
        {
            _canMove = false;
        }

        public void PlayerDie()
        {
            Happened();
        }

        private void UpdateMove()
        {
            if (_canMove == false)
            {
                _rigidbody.isKinematic = true;
                _rigidbody.velocity *= 0;
                return;
            }

            _rigidbody.isKinematic = false;
            float speed = _config.Speed * Time.fixedDeltaTime * 10; //extra speed
            Vector2 input = _isRight ? Vector2.right : Vector2.left;
            _rigidbody.velocity = new Vector2(input.x * speed, _rigidbody.velocity.y);
        }

        private void UpdateDirectionBody(bool isRight)
        {
            Vector3 scale = _bodyTransform.localScale;
            scale.x = isRight ? -_startScaleX : _startScaleX;
            _bodyTransform.localScale = scale;
        }

        private void Happened()
        {
            enabled = false;
            _rigidbody.isKinematic = true;
            _rigidbody.velocity *= 0;
        }
    }
}