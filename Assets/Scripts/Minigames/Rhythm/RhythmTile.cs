using System.Collections;
using UnityEngine;

namespace Minigames.Rhythm
{
    public class RhythmTile : MonoBehaviour
    {
        [Header("Visual")]
        [SerializeField]
        private MeshRenderer meshRenderer;

        [SerializeField]
        private Material idleMaterial;

        [SerializeField]
        private Material blinkMaterial;

        [SerializeField]
        private Material activeMaterial;

        [Header("Blink")]
        [SerializeField]
        private int blinkCount = 3;

        [SerializeField]
        private float blinkInterval = 0.15f;

        private Coroutine blinkRoutine;

        public bool IsTarget { get; private set; }

        private void Awake()
        {
            SetIdle();
        }

        public void Activate()
        {
            if (blinkRoutine != null)
            {
                StopCoroutine(blinkRoutine);
            }

            blinkRoutine =
                StartCoroutine(BlinkRoutine());
        }

        private IEnumerator BlinkRoutine()
        {
            IsTarget = true;

            for (int i = 0; i < blinkCount; i++)
            {
                meshRenderer.sharedMaterial =
                    blinkMaterial;

                yield return new WaitForSeconds(
                    blinkInterval
                );

                meshRenderer.sharedMaterial =
                    idleMaterial;

                yield return new WaitForSeconds(
                    blinkInterval
                );
            }

            meshRenderer.sharedMaterial =
                activeMaterial;

            RhythmGame.Instance.EnableInput();
        }

        public void SetIdle()
        {
            if (blinkRoutine != null)
            {
                StopCoroutine(blinkRoutine);
            }

            IsTarget = false;

            meshRenderer.sharedMaterial =
                idleMaterial;
        }
    }
}