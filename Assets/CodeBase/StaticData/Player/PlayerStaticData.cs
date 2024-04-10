using UnityEngine;

namespace CodeBase.StaticData.Player
{
    [CreateAssetMenu(menuName = "Static Data/Player Static Data", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
        [field: SerializeField] public float StartHealth { get; private set; } = 5;

        [field: SerializeField] public float AttackRadius { get; private set; } = 2;
        [field: SerializeField] public float Damage { get; private set; } = 10;
        [field: SerializeField] public float DelayBeforeAttack { get; private set; } = 1f;
        [field: SerializeField] public float DelayBeforeApplyDamage { get; private set; } = 0.4f;
    }
}