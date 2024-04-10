using UnityEngine;

namespace CodeBase.Logic.SpawnMarkers
{
    public class PlayerInitialPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}