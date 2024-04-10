using System;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        [field: SerializeField] public float PlayerHealth { get; private set; }
        public bool Happened { get; private set; }
        public int KillCount { get; private set; }

        public Action OnHealthChange;
        public Action OnHappened;

        public Action OnKillCountChange;

        public PlayerProgress(float playerHealth)
        {
            PlayerHealth = playerHealth;
        }

        public void IncrementKillCount()
        {
            KillCount++;
            OnKillCountChange?.Invoke();
        }

        public void DecrementHealth(float value)
        {
            if (Happened)
                return;

            PlayerHealth -= value;
            if (PlayerHealth <= 0)
            {
                PlayerHealth = 0;
                Happened = true;
                OnHappened?.Invoke();
            }

            OnHealthChange?.Invoke();
        }
    }
}