using Minigames.Core;
using Shopping;
using UnityEngine;

namespace World
{
    public class DepartmentProduct : MonoBehaviour
    {
        [SerializeField] private ProductData product;
        [SerializeField] private SellerNPC seller;
        public GameObject root;

        private bool playerInside;

        public ProductData Product => product;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                playerInside = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                playerInside = false;
        }

        private void Update()
        {
            if (!playerInside) return;

            ProductInstance instance =
                ShoppingManager.Instance.GetProduct(product);

            if (instance == null)
                return;

            if (instance.Purchased)
                return;

            if (instance.DiscountResolved)
                return;

            if (UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
            {
                StartDiscount();
            }

            if (UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame)
            {
                TryBuy();
            }
        }

        private void StartDiscount()
        {
            ProductInstance instance =
                ShoppingManager.Instance.GetProduct(product);

            if (instance == null)
                return;

            if (instance.Purchased)
                return;

            if (instance.DiscountResolved)
                return;

            if (MinigameController.Instance.IsBusy)
                return;

            MinigameController.Instance.RequestStart(
                seller
            );
        }

        private void TryBuy()
        {
            if (ShoppingManager.Instance.TryBuy(product))
            {
                Audio.AudioManager.Instance.PlayPickup();
            }
        }

        public void ApplyDiscountResult(bool success)
        {
            ShoppingManager.Instance.RegisterDiscountResult(
                product,
                success
            );
        }
    }
}