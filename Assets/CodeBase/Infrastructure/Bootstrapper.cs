using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.States;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private static bool _isStarted;

        private void Awake()
        {
            if (_isStarted)
                return;

            _isStarted = true;
            DontDestroyOnLoad(this);
            RegisterServices _ = new(this);

            AllServices.Container.Single<IGameStateMachine>().Enter<LoadLevelState>();
        }
    }
}