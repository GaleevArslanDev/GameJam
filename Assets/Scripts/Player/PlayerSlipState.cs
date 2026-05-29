using UnityEngine;

namespace Player
{
    public class PlayerSlipState : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private PlayerMotor motor;
        [SerializeField] private PlayerLook look;
        [SerializeField] private Transform cameraRoot;

        [Header("Camera")]
        [SerializeField] private float cameraHeightDrop = 0.5f;

        [Header("Rotation Feel")]
        [SerializeField] private float maxTiltUp = 70f;
        [SerializeField] private float maxTiltDown = -80f;

        [Header("Timing")]
        [SerializeField] private float slipTime = 0.25f;
        [SerializeField] private float fallTime = 0.35f;

        [Header("Curves (IMPORTANT)")]
        [SerializeField] private AnimationCurve slipCurve;
        [SerializeField] private AnimationCurve fallCurve;

        private enum State { None, Slip, Fall }
        private State state;

        private float timer;

        private Vector3 camStartPos;
        private float pitch;

        private Vector3 slipDir;

        private void Start()
        {
            camStartPos = cameraRoot.localPosition;
        }

        public void Slip(Vector3 dir)
        {
            if (state != State.None)
                return;

            state = State.Slip;
            timer = 0f;

            slipDir = dir;

            motor.SetControlEnabled(false);
            look.SetControlEnabled(false);
        }

        private void Update()
        {
            if (state == State.None)
                return;

            timer += Time.deltaTime;

            if (state == State.Slip)
                UpdateSlip();

            else if (state == State.Fall)
                UpdateFall();
        }

        private void UpdateSlip()
        {
            float t = timer / slipTime;

            float curve = slipCurve.Evaluate(t);

            // 🧠 лёгкое "потерял равновесие"
            pitch = Mathf.Lerp(0f, maxTiltUp, curve);

            cameraRoot.localPosition = Vector3.Lerp(
                camStartPos,
                camStartPos + Vector3.down * cameraHeightDrop,
                curve
            );

            ApplyCamera();

            if (t >= 1f)
            {
                state = State.Fall;
                timer = 0f;
            }
        }

        private void UpdateFall()
        {
            float t = timer / fallTime;

            float curve = fallCurve.Evaluate(t);

            // 💥 резкий завал назад
            pitch = Mathf.Lerp(maxTiltUp, maxTiltDown, curve);

            cameraRoot.localPosition = Vector3.Lerp(
                camStartPos + Vector3.down * cameraHeightDrop,
                camStartPos + Vector3.down * (cameraHeightDrop * 1.8f),
                curve
            );

            // 🔥 маленький “kick” назад
            cameraRoot.localPosition += slipDir * (curve * 0.15f);

            ApplyCamera();

            if (t >= 1f)
            {
                state = State.None;
            }
        }

        private void ApplyCamera()
        {
            cameraRoot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }
}