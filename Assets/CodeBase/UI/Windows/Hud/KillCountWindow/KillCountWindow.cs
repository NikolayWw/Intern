using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Hud.KillCountWindow
{
    public class KillCountWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _killCountText;
        private PlayerProgress _playerProgress;

        public void Construct(IPersistentProgressService progressService)
        {
            _playerProgress = progressService.PlayerProgress;
            _playerProgress.OnKillCountChange += Refresh;
        }

        private void Start()
        {
            Refresh();
        }

        private void OnDestroy()
        {
            _playerProgress.OnKillCountChange -= Refresh;
        }

        private void Refresh()
        {
            _killCountText.text = _playerProgress.KillCount.ToString();
        }
    }
}