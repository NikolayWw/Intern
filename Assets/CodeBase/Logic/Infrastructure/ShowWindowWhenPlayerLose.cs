using CodeBase.Infrastructure.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Factory;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Infrastructure
{
    public class ShowWindowWhenPlayerLose
    {
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IUIFactory _uiFactory;
        private readonly ICoroutineRunner _coroutineRunner;

        public ShowWindowWhenPlayerLose(IPersistentProgressService persistentProgressService, IUIFactory uiFactory, ICoroutineRunner coroutineRunner)
        {
            _persistentProgressService = persistentProgressService;
            _uiFactory = uiFactory;
            _coroutineRunner = coroutineRunner;

            _persistentProgressService.PlayerProgress.OnHappened += () => _coroutineRunner.StartCoroutine(ShowWithDelay());
        }

        public void Cleanup()
        {
            _persistentProgressService.PlayerProgress.OnHappened -= () => _coroutineRunner.StartCoroutine(ShowWithDelay());
        }

        private IEnumerator ShowWithDelay()
        {
            yield return new WaitForSeconds(2f);//delay
            _uiFactory.CreateLoseWindow();
        }
    }
}