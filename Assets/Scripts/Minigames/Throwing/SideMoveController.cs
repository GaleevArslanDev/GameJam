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

        private bool canControl;
        private Vector3 startPosition;
        private bool hasStartPoint;

        public void EnableControl()
        {
            canControl = true;
            startPosition = transform.position;
            hasStartPoint = true;
        }

        public void DisableControl()
        {
            canControl = false;
            hasStartPoint = false;
        }

        private void Update()
        {
            if (!canControl || !hasStartPoint)
                return;

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

            float clampedX = Mathf.Clamp(
                pos.x,
                startPosition.x - maxDistance,
                startPosition.x + maxDistance
            );

            pos.x = clampedX;
            transform.position = pos;
        }
    }
}