using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.Throwing
{
    public class ProjectileThrower : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;

        [SerializeField] private Transform shootPoint;

        [SerializeField] private float throwForce = 20f;

        [SerializeField] private int maxAmmo = 10;

        private int currentAmmo;

        private bool canThrow;

        private void Awake()
        {
            currentAmmo = maxAmmo;
        }

        public void EnableThrowing()
        {
            canThrow = true;
            currentAmmo = maxAmmo;
        }

        public void DisableThrowing()
        {
            canThrow = false;
        }

        private void Update()
        {
            if (!canThrow)
                return;

            if (
                Mouse.current.leftButton
                .wasPressedThisFrame
            )
            {
                Throw();
            }
        }

        private void Throw()
        {
            if (currentAmmo <= 0)
            {
                FindObjectOfType<ThrowingGame>()
                    .LoseGame();

                return;
            }

            currentAmmo--;

            GameObject projectile =
                Instantiate(
                    projectilePrefab,
                    shootPoint.position,
                    shootPoint.rotation
                );

            Rigidbody rb =
                projectile.GetComponent<Rigidbody>();

            rb.AddForce(
                shootPoint.forward * throwForce,
                ForceMode.Impulse
            );
        }
    }
}