using UnityEngine;

namespace CodeBase.Services.Input
{
    public class InputService : IInputService
    {
        public float Hit => UnityEngine.Input.GetAxisRaw("Horizontal");
    }
}