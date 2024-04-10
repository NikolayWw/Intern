using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Hud.PlayerHealth
{
    public class PlayerHealthWindow : MonoBehaviour
    {
        [SerializeField] private Image _healthImage;

        private PlayerProgress _playerProgress;
        private PlayerStaticData _playerConfig;

        public void Construct(IPersistentProgressService persistentProgressService, IStaticDataService dataService)
        {
            _playerProgress = persistentProgressService.PlayerProgress;
            _playerConfig = dataService.PlayerData;

            _playerProgress.OnHealthChange += Refresh;
        }

        private void Start()
        {
            Refresh();
        }

        private void OnDestroy()
        {
            _playerProgress.OnHealthChange -= Refresh;
        }

        private void Refresh() =>
            _healthImage.fillAmount = _playerProgress.PlayerHealth / _playerConfig.StartHealth;
    }
}