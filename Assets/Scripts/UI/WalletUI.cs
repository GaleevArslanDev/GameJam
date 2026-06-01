using Shopping;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WalletUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void Start()
        {
            Debug.Log("Shopping manager: ");
            Debug.Log(ShoppingManager.Instance);
            ShoppingManager.Instance.Wallet.OnMoneyChanged += UpdateUI;
            UpdateUI(ShoppingManager.Instance.Wallet.CurrentMoney);
        }

        private void OnDisable()
        {
            ShoppingManager.Instance.Wallet.OnMoneyChanged -= UpdateUI;
        }

        private void UpdateUI(float money)
        {
            text.text = $"Money: {money:0}";
        }
    }
}