using System.Collections;
using UnityEngine;

namespace Minigames.Reaction
{
    public class ReactionZone : MonoBehaviour
    {
        [SerializeField]
        private Transform visual;

        [SerializeField]
        private ParticleSystem particles;

        [SerializeField]
        private BoxCollider triggerCollider;

        [Header("Fade")]
        [SerializeField]
        private float fadeDuration = 1f;

        private ParticleSystem.EmissionModule emission;
        private Coroutine moveRoutine;

        public bool SellerInside { get; private set; }
        public bool IsTransitioning { get; private set; }

        private void Awake()
        {
            emission = particles.emission;
        }

        public void MoveTo(
            float x,
            float width,
            float particlesWidth
        )
        {
            if (moveRoutine != null)
            {
                StopCoroutine(moveRoutine);
            }

            moveRoutine =
                StartCoroutine(
                    MoveRoutine(
                        x,
                        width,
                        particlesWidth
                    )
                );
        }

        private IEnumerator MoveRoutine(
            float x,
            float width,
            float particlesWidth
        )
        {
            IsTransitioning = true;

            SellerInside = false;

            yield return FadeOut();

            Teleport(
                x,
                width,
                particlesWidth
            );

            yield return FadeIn();

            IsTransitioning = false;
        }
        
        public void ResetZone()
        {
            if (moveRoutine != null)
            {
                StopCoroutine(moveRoutine);
                moveRoutine = null;
            }

            IsTransitioning = false;

            SellerInside = false;

            particles.Clear(true);
            particles.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );

            emission.rateOverTime = 40f;
        }

        private IEnumerator FadeOut()
        {
            float timer = 0f;

            ParticleSystem.Particle[] particlesArray =
                new ParticleSystem.Particle[
                    particles.main.maxParticles
                ];

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;

                float t = timer / fadeDuration;

                // Отключаем спавн новых
                emission.rateOverTime =
                    Mathf.Lerp(40f, 0f, t);

                int count =
                    particles.GetParticles(
                        particlesArray
                    );

                for (int i = 0; i < count; i++)
                {
                    // Принудительно уменьшаем жизнь
                    particlesArray[i].remainingLifetime *=
                        (1f - Time.deltaTime / fadeDuration);
                }

                particles.SetParticles(
                    particlesArray,
                    count
                );

                yield return null;
            }

            particles.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );
        }

        private IEnumerator FadeIn()
        {
            particles.Play();
            
            float targetRate = 40f;

            float timer = 0f;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;

                float t = timer / fadeDuration;

                emission.rateOverTime =
                    Mathf.Lerp(
                        0f,
                        targetRate,
                        t
                    );

                yield return null;
            }

            emission.rateOverTime =
                targetRate;
        }

        private void Teleport(
            float x,
            float width,
            float particlesWidth
        )
        {
            var shape = particles.shape;

            shape.radius = particlesWidth;

            SellerInside = false;

            Vector3 pos = transform.position;
            pos.x = x;

            transform.position = pos;

            Vector3 scale =
                visual.localScale;

            scale.x = width;

            visual.localScale = scale;

            Vector3 colliderSize =
                triggerCollider.size;

            colliderSize.x = width;

            triggerCollider.size =
                colliderSize;
        }

        private void OnTriggerEnter(
            Collider other
        )
        {
            if (
                other.CompareTag("Seller")
            )
            {
                SellerInside = true;
            }
        }

        private void OnTriggerExit(
            Collider other
        )
        {
            if (
                other.CompareTag("Seller")
            )
            {
                SellerInside = false;
            }
        }
    }
}