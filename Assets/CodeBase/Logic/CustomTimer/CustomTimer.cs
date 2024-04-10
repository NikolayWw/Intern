using System;
using System.Collections;
using CodeBase.Infrastructure.Logic;
using UnityEngine;

namespace CodeBase.Logic.CustomTimer
{
    public class CustomTimer
    {
        private class CoroutineCounter
        {
            private readonly ICoroutineRunner _coroutineRunner;
            private bool _isStop;

            public CoroutineCounter(ICoroutineRunner coroutineRunner, float delay, Action onElapsed)
            {
                _coroutineRunner = coroutineRunner;
                _coroutineRunner.StartCoroutine(Process(delay, onElapsed));
            }

            public void Stop()
            {
                _isStop = true;
            }

            private IEnumerator Process(float delay, Action onElapsed)
            {
                while (delay > 0.0f)
                {
                    delay -= Time.deltaTime;
                    yield return null;
                }
                if (_isStop == false)
                    onElapsed?.Invoke();
            }
        }

        private readonly ICoroutineRunner _coroutineRunner;

        private CoroutineCounter _currentTimer;

        public CustomTimer(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Start(float delay, Action onElapsed) =>
            _currentTimer = new CoroutineCounter(_coroutineRunner, delay, onElapsed);

        public void Stop() =>
            _currentTimer?.Stop();
    }
}