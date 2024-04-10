using CodeBase.Data;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerLookAt : MonoBehaviour
    {
        [SerializeField] private Transform _bodyTransform;
        private IInputService _inputService;
        private PlayerProgress _progress;

        public void Construct(IInputService inputService, IPersistentProgressService persistentProgressService)
        {
            _inputService = inputService;
            _progress = persistentProgressService.PlayerProgress;
            _progress.OnHappened += DisableThis;
        }

        private void OnDestroy()
        {
            _progress.OnHappened -= DisableThis;
        }

        private void Update()
        {
            if (_inputService.Hit > 0)
                UpdateLookAt(true);
            else if (_inputService.Hit < 0)
                UpdateLookAt(false);
        }

        private void UpdateLookAt(bool isRight)
        {
            Vector3 scale = new(isRight ? 1 : -1, 1, 1);
            _bodyTransform.localScale = scale;
        }

        private void DisableThis()
        {
            enabled = false;
        }
    }
}