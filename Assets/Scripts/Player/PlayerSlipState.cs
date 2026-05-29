using UnityEngine;

namespace Player
{
    public class PlayerSlipState : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerMotor motor;
        [SerializeField] private PlayerLook look;
        [SerializeField] private PlayerInputReader input;
        [SerializeField] private Transform cameraRoot;

        [Header("Slip Timing")]
        [SerializeField] private float slipTime = 0.25f;

        [SerializeField] private float fallTime = 0.35f;

        [SerializeField] private float recoverTime = 5f;

        [Header("Camera")]
        [SerializeField] private float maxTiltUp = 70f;

        [SerializeField] private float maxTiltDown = -85f;

        [SerializeField] private float cameraHeightDrop = 0.6f;

        [SerializeField] private float recoverSpeed = 6f;

        [Header("Curves")]
        [SerializeField] private AnimationCurve slipCurve;

        [SerializeField] private AnimationCurve fallCurve;

        [Header("Balance Minigame")]
        [SerializeField] private float balance = 50f;

        [SerializeField] private float balanceDrain = 35f;

        [SerializeField] private float tapForce = 18f;

        [SerializeField] private float safeZoneMin = 40f;

        [SerializeField] private float safeZoneMax = 60f;

        [SerializeField] private float failPenalty = 0.5f;

        private enum State
        {
            None,
            Slip,
            Fall,
            Grounded,
            Recovering
        }

        private State state;

        private float timer;
        private float recoverProgress;

        private float pitch;

        private bool previousJump;

        private Vector3 originalCamPos;
        private Vector3 slipDir;

        public bool IsSlipping => state != State.None;

        public float Balance => balance;

        public float RecoverProgressNormalized =>
            recoverProgress / recoverTime;

        public float SafeZoneMin => safeZoneMin / 100f;

        public float SafeZoneMax => safeZoneMax / 100f;

        private void Start()
        {
            originalCamPos = cameraRoot.localPosition;
        }

        private void Update()
        {
            switch (state)
            {
                case State.Slip:
                    UpdateSlip();
                    break;

                case State.Fall:
                    UpdateFall();
                    break;

                case State.Grounded:
                    UpdateGrounded();
                    break;

                case State.Recovering:
                    UpdateRecovering();
                    break;
            }
        }

        public void Slip(Vector3 direction)
        {
            if (state != State.None)
                return;

            state = State.Slip;

            timer = 0f;
            recoverProgress = 0f;

            balance = 50f;

            slipDir = direction.normalized;

            motor.SetControlEnabled(false);
            look.SetControlEnabled(false);
        }

        private void UpdateSlip()
        {
            timer += Time.deltaTime;

            float t = timer / slipTime;

            float curve = slipCurve.Evaluate(t);

            pitch = Mathf.Lerp(
                0f,
                maxTiltUp,
                curve
            );

            cameraRoot.localPosition = Vector3.Lerp(
                originalCamPos,
                originalCamPos + Vector3.down * cameraHeightDrop,
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
            timer += Time.deltaTime;

            float t = timer / fallTime;

            float curve = fallCurve.Evaluate(t);

            pitch = Mathf.Lerp(
                maxTiltUp,
                maxTiltDown,
                curve
            );

            Vector3 targetPos =
                originalCamPos +
                Vector3.down * (cameraHeightDrop * 1.8f);

            targetPos += slipDir * 0.15f;

            cameraRoot.localPosition = Vector3.Lerp(
                cameraRoot.localPosition,
                targetPos,
                Time.deltaTime * 10f
            );

            ApplyCamera();

            if (t >= 1f)
            {
                state = State.Grounded;
            }
        }

        private void UpdateGrounded()
        {
            // баланс падает вниз
            balance -= balanceDrain * Time.deltaTime;

            // tap detection
            bool currentJump = input.JumpPressed;

            if (currentJump && !previousJump)
            {
                balance += tapForce;
            }

            previousJump = currentJump;

            balance = Mathf.Clamp(balance, 0f, 100f);

            bool insideZone =
                balance >= safeZoneMin &&
                balance <= safeZoneMax;

            if (insideZone)
            {
                recoverProgress += Time.deltaTime;
            }
            else
            {
                recoverProgress -= Time.deltaTime * failPenalty;
            }

            recoverProgress = Mathf.Clamp(
                recoverProgress,
                0f,
                recoverTime
            );

            // слишком потерял баланс
            if (balance <= 0f)
            {
                balance = 15f;

                recoverProgress *= 0.5f;
            }

            // успешно встал
            if (recoverProgress >= recoverTime)
            {
                state = State.Recovering;
                timer = 0f;
            }
        }

        private void UpdateRecovering()
        {
            timer += Time.deltaTime;

            float t = timer / 0.5f;

            pitch = Mathf.Lerp(
                maxTiltDown,
                0f,
                t
            );

            cameraRoot.localPosition = Vector3.Lerp(
                cameraRoot.localPosition,
                originalCamPos,
                Time.deltaTime * recoverSpeed
            );

            ApplyCamera();

            if (t >= 1f)
            {
                FinishRecovery();
            }
        }

        private void FinishRecovery()
        {
            state = State.None;

            pitch = 0f;

            cameraRoot.localPosition = originalCamPos;
            cameraRoot.localRotation = Quaternion.identity;

            motor.SetControlEnabled(true);
            look.SetControlEnabled(true);
        }

        private void ApplyCamera()
        {
            cameraRoot.localRotation =
                Quaternion.Euler(pitch, 0f, 0f);
        }
    }
}