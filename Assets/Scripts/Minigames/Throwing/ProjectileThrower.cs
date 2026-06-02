using UnityEngine;
using UnityEngine.InputSystem;
using Minigames.Core;

namespace Minigames.Throwing
{
    public class ProjectileThrower : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float throwForce = 20f;
        [SerializeField] private int maxAmmo = 15;

        private int ammo;
        private ThrowingGame game;

        public void Init(ThrowingGame g)
        {
            game = g;
        }

        private void OnEnable()
        {
            ammo = maxAmmo;
            UpdateAmmoUI();
        }
        
        private void UpdateAmmoUI()
        {
            if (game == null)
                return;

            game.Context.ThrowingUI.UpdateAmmo(ammo, maxAmmo);
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
                Throw();
        }

        private void Throw()
        {
            if (ammo <= 0)
            {
                game?.RegisterMiss();
                return;
            }

            ammo--;
            
            UpdateAmmoUI();

            GameObject obj = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

            Projectile p = obj.GetComponent<Projectile>();
            p.Init(game);

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.AddForce(shootPoint.forward * throwForce, ForceMode.Impulse);
        }
    }
}