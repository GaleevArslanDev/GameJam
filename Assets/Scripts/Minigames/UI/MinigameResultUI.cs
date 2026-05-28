using TMPro;
using UnityEngine;

namespace Minigames.UI
{
    public class MinigameResultUI : MonoBehaviour
    {
        public static MinigameResultUI Instance;

        [SerializeField]
        private GameObject root;

        [SerializeField]
        private TextMeshProUGUI resultText;

        private void Awake()
        {
            Instance = this;

            root.SetActive(false);
        }

        public void Show(bool success)
        {
            root.SetActive(true);

            resultText.text =
                success
                    ? "DISCOUNT RECEIVED"
                    : "DISCOUNT FAILED";
        }

        public void Hide()
        {
            root.SetActive(false);
        }
    }
}