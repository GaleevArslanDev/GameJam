using TMPro;
using UnityEngine;

namespace Minigames.UI
{
    public class MinigameResultUI : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private TextMeshProUGUI resultText;

        private void Awake()
        {
            root.SetActive(false);
        }

        public void Show(bool success)
        {
            root.SetActive(true);
            if (success)
            {
                Audio.AudioManager.Instance.PlayRoundWin();
            }
            else
            {
                Audio.AudioManager.Instance.PlayRoundLose();
            }

            resultText.text = success
                ? "DISCOUNT RECEIVED"
                : "DISCOUNT FAILED";
        }

        public void Hide()
        {
            root.SetActive(false);
        }
    }
}