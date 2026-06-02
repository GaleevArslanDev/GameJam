using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField]
        private GameTimer timer;

        [SerializeField]
        private TextMeshProUGUI timerText;
        
        [SerializeField]
        private Color normalColor = Color.white;

        [SerializeField]
        private Color dangerColor = Color.red;

        [SerializeField]
        private float dangerTime = 60f;

        private void Update()
        {
            if (timer == null)
                return;

            float time = timer.TimeLeft;

            int minutes =
                Mathf.FloorToInt(time / 60f);

            int seconds =
                Mathf.FloorToInt(time % 60f);

            timerText.text =
                $"{minutes:00}:{seconds:00}";
            
            timerText.color =
                time <= dangerTime
                    ? dangerColor
                    : normalColor;
        }
    }
}