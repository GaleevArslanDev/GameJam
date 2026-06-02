using Shopping;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ShoppingListUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void OnEnable()
        {
            ShoppingManager.Instance.OnShoppingListChanged += Refresh;
            Refresh();
        }

        private void OnDisable()
        {
            ShoppingManager.Instance.OnShoppingListChanged -= Refresh;
        }

        private void Refresh()
        {
            var list = ShoppingManager.Instance.ActiveProducts;

            string result = "SHOPPING LIST\n\n";

            foreach (var p in list)
            {
                string mark = p.Purchased ? "[X]" : "[ ]";
                result += $"{mark} {p.Data.ProductName}\n";
            }

            text.text = result;
        }
    }
}