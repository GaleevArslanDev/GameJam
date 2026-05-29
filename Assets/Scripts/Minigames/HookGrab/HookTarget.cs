using UnityEngine;

namespace Minigames.HookGrab
{
    public class HookTarget : MonoBehaviour
    {
        [SerializeField]
        private Renderer targetRenderer;

        [SerializeField]
        private Material idleMaterial;

        [SerializeField]
        private Material activeMaterial;

        private Transform followTarget;

        private Vector3 followOffset;

        private Vector3 startPosition;

        private Quaternion startRotation;

        public bool IsActive { get; private set; }
        public bool IsHooked { get; private set; }

        private void Awake()
        {
            startPosition =
                transform.position;

            startRotation =
                transform.rotation;
        }

        private void Update()
        {
            if (
                followTarget != null &&
                IsHooked
            )
            {
                transform.position =
                    followTarget.position +
                    followOffset;
            }
        }

        public void ResetTarget()
        {
            gameObject.SetActive(true);

            transform.position =
                startPosition;

            transform.rotation =
                startRotation;

            followTarget = null;

            IsHooked = false;

            SetInactive();
        }

        public void SetActiveTarget()
        {
            IsActive = true;

            targetRenderer.material =
                activeMaterial;
        }

        public void SetInactive()
        {
            IsActive = false;

            targetRenderer.material =
                idleMaterial;
        }

        public void HookTo(
            Transform hook
        )
        {
            IsHooked = true;

            followTarget = hook;

            followOffset =
                transform.position -
                hook.position;
        }

        public void Collect()
        {
            gameObject.SetActive(false);
        }
    }
}