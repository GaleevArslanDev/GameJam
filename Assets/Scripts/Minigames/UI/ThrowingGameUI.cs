using TMPro;
using UnityEngine;

namespace Minigames.UI
{
    public class ThrowingGameUI : MonoBehaviour
    {
        public static ThrowingGameUI Instance;

        [SerializeField]
        private TextMeshProUGUI hitsText;

        [SerializeField]
        private TextMeshProUGUI missesText;

        [SerializeField]
        private TextMeshProUGUI ammoText;

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateHits(
            int current,
            int target
        )
        {
            hitsText.text =
                $"Hits: {current}/{target}";
        }

        public void UpdateMisses(
            int current,
            int target
        )
        {
            missesText.text =
                $"Misses: {current}/{target}";
        }

        public void UpdateAmmo(
            int current,
            int max
        )
        {
            ammoText.text =
                $"Ammo: {current}/{max}";
        }
    }
}