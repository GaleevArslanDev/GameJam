using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.HookGrab
{
    public class HookGrabPlayer : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 5f;

        [SerializeField]
        private float maxDistance = 4f;

        [SerializeField]
        private Transform hookSpawn;

        [SerializeField]
        private HookProjectile hookPrefab;
        
        [SerializeField]
        private HookSeller seller;

        private Vector3 startPosition;

        private bool canShoot = true;

        private bool movementLocked;

        public Transform HookSpawn => hookSpawn;

        private void Awake()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            startPosition = transform.position;

            canShoot = true;

            movementLocked = false;
        }

        private void Update()
        {
            Move();

            Shoot();
        }

        public void SetMovementLocked(
            bool value
        )
        {
            movementLocked = value;
        }

        private void Move()
        {
            if (movementLocked)
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

            pos.x = Mathf.Clamp(
                pos.x,
                startPosition.x - maxDistance,
                startPosition.x + maxDistance
            );

            transform.position = pos;
        }

        private void Shoot()
        {
            if (!canShoot)
                return;

            if (
                Mouse.current.leftButton
                .wasPressedThisFrame
            )
            {
                HookProjectile hook =
                    Instantiate(
                        hookPrefab,
                        hookSpawn.position,
                        hookSpawn.rotation
                    );

                hook.Init(
                    this,
                    seller
                );

                canShoot = false;
            }
        }

        public void Reload()
        {
            canShoot = true;
        }
    }
}