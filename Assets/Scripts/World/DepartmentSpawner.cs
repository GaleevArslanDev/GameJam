using Shopping;
using UnityEngine;

namespace World
{
    public class DepartmentSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPoints;

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            var products = ShoppingManager.Instance.ActiveProducts;

            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];

                Transform point = spawnPoints[i % spawnPoints.Length];

                GameObject obj = Instantiate(
                    product.Data.DepartmentPrefab,
                    point.position,
                    point.rotation
                );
            }
        }
    }
}