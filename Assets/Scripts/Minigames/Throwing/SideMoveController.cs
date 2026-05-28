using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.Throwing
{
    public class SideMoveController : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 7f;

        [SerializeField]
        private float maxDistance = 5f;

        private Vector3 startPosition;

        private void Awake()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            float input = 0f;

            if (Keyboard.current.aKey.isPressed)
            {
                input = -1f;
            }

            if (Keyboard.current.dKey.isPressed)
            {
                input = 1f;
            }

            Vector3 pos = transform.position;

            pos.x +=
                input *
                moveSpeed *
                Time.deltaTime;

            pos.x = Mathf.Clamp(
                pos.x,
                startPosition.x - maxDistance,
                startPosition.x + maxDistance
            );

            transform.position = pos;
        }
    }
}