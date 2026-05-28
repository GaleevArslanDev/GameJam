using Minigames.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.Throwing
{
    public class ProjectileThrower : MonoBehaviour
    {
        [SerializeField]
        private GameObject projectilePrefab;

        [SerializeField]
        private Transform shootPoint;

        [SerializeField]
        private float throwForce = 20f;

        [SerializeField]
        private int maxAmmo = 15;

        private int currentAmmo;

        private void Awake()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            currentAmmo = maxAmmo;

            UpdateUI();
        }

        private void Update()
        {
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
                ThrowingGame.Instance.LoseGame();

                return;
            }

            currentAmmo--;

            UpdateUI();

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

        private void UpdateUI()
        {
            ThrowingGameUI.Instance.UpdateAmmo(
                currentAmmo,
                maxAmmo
            );
        }
    }
}