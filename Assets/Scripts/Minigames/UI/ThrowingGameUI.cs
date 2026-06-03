using TMPro;
using UnityEngine;

namespace Minigames.UI
{
    public class ThrowingGameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hitsText;
        [SerializeField] private TextMeshProUGUI missesText;
        [SerializeField] private TextMeshProUGUI ammoText;

        public void UpdateHits(int current, int target)
        {
            hitsText.text = $"Hits: {current}/{target}";
            Audio.AudioManager.Instance.PlayStatUpdate();
        }

        public void UpdateMisses(int current, int target)
        {
            missesText.text = $"Misses: {current}/{target}";
            Audio.AudioManager.Instance.PlayStatUpdate();
        }

        public void UpdateAmmo(int current, int max)
        {
            ammoText.text = $"Ammo: {current}/{max}";
            Audio.AudioManager.Instance.PlayStatUpdate();
        }
    }
}