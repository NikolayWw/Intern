using CodeBase.Data;
using CodeBase.Infrastructure.States;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.LoseWindow
{
    public class LoseWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _killCounterText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private IGameStateMachine _gameStateMachine;
        private PlayerProgress _playerProgress;

        public void Construct(IGameStateMachine gameStateMachine, IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _playerProgress = persistentProgressService.PlayerProgress;

            _restartButton.onClick.AddListener(() => _gameStateMachine.Enter<LoadLevelState>());
            _exitButton.onClick.AddListener(Application.Quit);
        }

        private void Start()
        {
            Refresh();
        }

        private void Refresh() =>
            _killCounterText.text = _playerProgress.KillCount.ToString();
    }
}