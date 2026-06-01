using UnityEngine;

namespace Shopping
{
    [System.Serializable]
    public class ProductInstance
    {
        public ProductData Data;

        public bool Purchased;

        public bool DiscountResolved;

        public bool DiscountSuccess;

        public float CurrentPrice;

        public ProductInstance(ProductData data)
        {
            Data = data;

            CurrentPrice = data.BasePrice;
        }
    }
}