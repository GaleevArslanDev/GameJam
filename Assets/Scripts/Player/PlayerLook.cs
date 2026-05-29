using UnityEngine;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerInputReader input;
        [SerializeField] private Transform cameraRoot;
        [SerializeField] private PlayerSlipState slipState;

        [Header("Settings")]
        [SerializeField] private float sensitivity = 2f;

        [SerializeField] private float minPitch = -80f;
        [SerializeField] private float maxPitch = 80f;

        [Header("Smoothing")]
        [SerializeField] private float smoothSpeed = 12f;

        private float pitch;

        private Vector2 currentLook;

        private bool controlsEnabled = true;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void SetControlEnabled(bool enabled)
        {
            controlsEnabled = enabled;
        }

        private void Update()
        {
            if (!controlsEnabled)
                return;

            Look();
        }

        private void Look()
        {
            Vector2 targetLook = input.Look;

            currentLook = Vector2.Lerp(
                currentLook,
                targetLook,
                smoothSpeed * Time.deltaTime
            );

            float mouseX = currentLook.x * sensitivity;
            float mouseY = currentLook.y * sensitivity;

            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            cameraRoot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}