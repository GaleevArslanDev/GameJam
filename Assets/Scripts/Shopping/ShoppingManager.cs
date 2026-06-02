using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shopping
{
    public class ShoppingManager : MonoBehaviour
    {
        public static ShoppingManager Instance;

        [Header("References")]
        [SerializeField]
        private ProductCatalog catalog;

        [SerializeField]
        private Wallet wallet;

        [Header("Gameplay")]
        [SerializeField]
        private int productsToChoose = 5;

        [SerializeField]
        private float discountPercent = 0.3f;

        [SerializeField]
        private float failPenaltyPercent = 0.15f;

        [SerializeField]
        private float startMoney = 200;

        private readonly List<ProductInstance>
            activeProducts =
                new();

        public IReadOnlyList<ProductInstance>
            ActiveProducts =>
                activeProducts;

        public Wallet Wallet => wallet;

        public event Action OnShoppingListChanged;

        public event Action OnWin;

        public event Action<string> OnLose;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            wallet.Initialize(startMoney);

            GenerateShoppingList();
        }

        private void GenerateShoppingList()
        {
            activeProducts.Clear();

            List<ProductData> source =
                catalog.Products.ToList();

            for (
                int i = 0;
                i < productsToChoose;
                i++
            )
            {
                int index =
                    UnityEngine.Random.Range(
                        0,
                        source.Count
                    );

                ProductData data =
                    source[index];

                source.RemoveAt(index);

                activeProducts.Add(
                    new ProductInstance(data)
                );
            }

            OnShoppingListChanged?.Invoke();
        }

        public ProductInstance GetProduct(
            ProductData data
        )
        {
            return activeProducts.FirstOrDefault(
                p => p.Data == data
            );
        }

        public bool IsProductRequired(
            ProductData data
        )
        {
            return GetProduct(data) != null;
        }

        public void RegisterDiscountResult(
            ProductData data,
            bool success
        )
        {
            ProductInstance product =
                GetProduct(data);

            if (product == null)
                return;

            if (product.DiscountResolved)
                return;

            product.DiscountResolved = true;
            product.DiscountSuccess = success;

            if (success)
            {
                product.CurrentPrice =
                    data.BasePrice *
                    (1f - discountPercent);
            }
            else
            {
                product.CurrentPrice =
                    data.BasePrice *
                    (1f + failPenaltyPercent);
            }

            OnShoppingListChanged?.Invoke();
        }

        public bool TryBuy(
            ProductData data
        )
        {
            ProductInstance product =
                GetProduct(data);

            if (product == null)
                return false;

            if (product.Purchased)
                return false;

            if (
                !wallet.CanSpend(
                    product.CurrentPrice
                )
            )
            {
                Lose(
                    "Not enough money"
                );

                return false;
            }

            wallet.Spend(
                product.CurrentPrice
            );

            product.Purchased = true;

            OnShoppingListChanged?.Invoke();

            CheckWin();

            return true;
        }

        private void CheckWin()
        {
            bool allBought =
                activeProducts.All(
                    x => x.Purchased
                );

            if (allBought)
            {
                OnWin?.Invoke();
            }
        }

        public void Lose(
            string reason
        )
        {
            OnLose?.Invoke(reason);
        }
    }
}