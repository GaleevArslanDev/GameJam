using System.Collections.Generic;
using Shopping;
using UnityEngine;

namespace World
{
    public class DepartmentSelector : MonoBehaviour
    {
        [SerializeField]
        private ShelfRow[] rows;

        private void Start()
        {
            SelectProducts();
        }

        private void SelectProducts()
        {
            List<ProductData> selectedProducts = new();

            foreach (ShelfRow row in rows)
            {
                int index = Random.Range(
                    0,
                    row.Shelves.Count
                );

                for (int i = 0; i < row.Shelves.Count; i++)
                {
                    bool active = i == index;

                    row.Shelves[i].gameObject.SetActive(active);

                    if (active)
                    {
                        selectedProducts.Add(
                            row.Shelves[i].Product
                        );
                    }
                }
            }

            ShoppingManager.Instance.InitializeProducts(
                selectedProducts
            );
        }
    }
}