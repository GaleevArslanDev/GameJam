using Core;
using Shopping;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ResultScreenUI : MonoBehaviour
    {
        [Header("Root")]
        [SerializeField]
        private GameObject root;

        [Header("Texts")]
        [SerializeField]
        private TextMeshProUGUI titleText;

        [SerializeField]
        private TextMeshProUGUI statsText;

        [SerializeField]
        private TextMeshProUGUI reasonText;

        [Header("References")]
        [SerializeField]
        private GameTimer timer;

        private void Awake()
        {
            root.SetActive(false);
        }

        private void Start()
        {
            ShoppingManager.Instance.OnWin += ShowWin;
            ShoppingManager.Instance.OnLose += ShowLose;
        }

        private void OnDestroy()
        {
            if (ShoppingManager.Instance == null)
                return;

            ShoppingManager.Instance.OnWin -= ShowWin;
            ShoppingManager.Instance.OnLose -= ShowLose;
        }

        private void ShowWin()
        {
            root.SetActive(true);

            titleText.text = "YOU WIN";

            float usedTime =
                timer.TimeLimit - timer.TimeLeft;

            statsText.text =
                $"Time Used: {FormatTime(usedTime)}\n" +
                $"Money Spent: {ShoppingManager.Instance.Wallet.SpentMoney:0}";

            reasonText.text = "";
            
            Time.timeScale = 0f;

            Cursor.lockState =
                CursorLockMode.None;

            Cursor.visible = true;
        }

        private void ShowLose(string reason)
        {
            root.SetActive(true);

            titleText.text = "YOU LOSE";

            float usedTime =
                timer.TimeLimit - timer.TimeLeft;

            statsText.text =
                $"Time Used: {FormatTime(usedTime)}\n" +
                $"Money Spent: {ShoppingManager.Instance.Wallet.SpentMoney:0}";

            reasonText.text =
                $"Reason: {reason}";
            
            Time.timeScale = 0f;

            Cursor.lockState =
                CursorLockMode.None;

            Cursor.visible = true;
        }

        private string FormatTime(float seconds)
        {
            int mins = Mathf.FloorToInt(seconds / 60f);
            int secs = Mathf.FloorToInt(seconds % 60f);

            return $"{mins:00}:{secs:00}";
        }
    }
}