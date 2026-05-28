using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Minigames.Core
{
    public class SpinTransition : MonoBehaviour
    {
        public static SpinTransition Instance;

        [Header("References")]
        [SerializeField]
        private Transform spinPivot;

        [SerializeField]
        private Volume globalVolume;

        [Header("Spin")]
        [SerializeField]
        private int spinLoops = 3;
        
        [SerializeField]
        private float maxSpeed = 1440f;

        [SerializeField]
        private float acceleration = 6f;

        [SerializeField]
        private float deceleration = 2.5f;

        private MotionBlur motionBlur;

        private void Awake()
        {
            Instance = this;

            globalVolume.profile.TryGet(
                out motionBlur
            );
        }

        public IEnumerator Play(
            System.Action midAction
        )
        {
            float speed = 0f;
            float angle = 0f;

            float targetAngle =
                360f * spinLoops;

            float triggerAngle =
                targetAngle * 0.5f;

            bool triggered = false;

            while (angle < targetAngle)
            {
                float normalized =
                    angle / targetAngle;

                if (normalized < 0.5f)
                {
                    speed = Mathf.Lerp(
                        speed,
                        maxSpeed,
                        acceleration * Time.deltaTime
                    );
                }
                else
                {
                    speed = Mathf.Lerp(
                        speed,
                        0f,
                        deceleration * Time.deltaTime
                    );
                }

                float delta =
                    speed * Time.deltaTime;

                angle += delta;

                spinPivot.Rotate(
                    0f,
                    delta,
                    0f,
                    Space.Self
                );

                float blurTarget =
                    normalized < 0.5f
                        ? 1f
                        : 0f;

                motionBlur.intensity.value =
                    Mathf.Lerp(
                        motionBlur.intensity.value,
                        blurTarget,
                        6f * Time.deltaTime
                    );

                if (
                    !triggered &&
                    angle >= triggerAngle
                )
                {
                    triggered = true;

                    midAction?.Invoke();
                }

                yield return null;
            }

            motionBlur.intensity.value = 0f;

            spinPivot.localRotation =
                Quaternion.identity;
        }
    }
}