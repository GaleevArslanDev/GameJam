using UnityEngine;

namespace Shopping
{
    [CreateAssetMenu(
        fileName = "Product",
        menuName = "Game/Product"
    )]
    public class ProductData : ScriptableObject
    {
        [Header("Info")]
        public string ProductName;

        [Header("Economy")]
        public float BasePrice = 20;

        [Header("Department")]
        public GameObject DepartmentPrefab;
    }
}