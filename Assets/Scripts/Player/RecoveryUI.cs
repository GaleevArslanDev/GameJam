using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class RecoveryUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerSlipState slip;

        [SerializeField] private RectTransform marker;

        [SerializeField] private RectTransform safeZone;

        [SerializeField] private Slider progressSlider;

        [Header("UI")]
        [SerializeField] private float minY = -180f;

        [SerializeField] private float maxY = 180f;

        private void Update()
        {
            UpdateMarker();
            UpdateSafeZone();
            UpdateProgress();
        }

        private void UpdateMarker()
        {
            float normalized = slip.Balance / 100f;

            Vector2 pos = marker.anchoredPosition;

            pos.y = Mathf.Lerp(
                minY,
                maxY,
                normalized
            );

            marker.anchoredPosition = pos;
        }

        private void UpdateSafeZone()
        {
            float zoneMin = slip.SafeZoneMin;
            float zoneMax = slip.SafeZoneMax;

            float center = (zoneMin + zoneMax) * 0.5f;

            float height = (zoneMax - zoneMin) * (maxY - minY);

            Vector2 pos = safeZone.anchoredPosition;

            pos.y = Mathf.Lerp(minY, maxY, center);

            safeZone.anchoredPosition = pos;

            safeZone.sizeDelta = new Vector2(
                safeZone.sizeDelta.x,
                height
            );
        }

        private void UpdateProgress()
        {
            progressSlider.value =
                slip.RecoverProgressNormalized;
        }
    }
}