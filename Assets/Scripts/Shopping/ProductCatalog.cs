using System.Collections.Generic;
using UnityEngine;

namespace Shopping
{
    public class ProductCatalog : MonoBehaviour
    {
        [SerializeField]
        private List<ProductData> products =
            new List<ProductData>();

        public IReadOnlyList<ProductData> Products =>
            products;
    }
}