using Minigames.Core;
using Shopping;
using UnityEngine;

namespace World
{
    public class DepartmentProduct : MonoBehaviour
    {
        [SerializeField] private ProductData product;
        [SerializeField] private SellerNPC seller;

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
            if (Minigames.Core.MinigameController.Instance.IsBusy)
                return;

            Minigames.Core.MinigameController.Instance.RequestStart(seller);
        }

        private void TryBuy()
        {
            ShoppingManager.Instance.TryBuy(product);
        }

        public void ApplyDiscountResult(bool success)
        {
            ShoppingManager.Instance.RegisterDiscountResult(product, success);
        }
    }
}