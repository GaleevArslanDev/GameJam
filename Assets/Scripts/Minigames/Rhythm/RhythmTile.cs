using System.Collections;
using UnityEngine;

namespace Minigames.Rhythm
{
    public class RhythmTile : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Material idle;
        [SerializeField] private Material blink;
        [SerializeField] private Material active;

        private RhythmGame game;
        private Coroutine routine;

        public void Activate(RhythmGame g)
        {
            game = g;

            if (routine != null)
                StopCoroutine(routine);

            routine = StartCoroutine(Blink());
        }

        private IEnumerator Blink()
        {
            for (int i = 0; i < 3; i++)
            {
                meshRenderer.sharedMaterial = blink;
                yield return new WaitForSeconds(0.15f);

                meshRenderer.sharedMaterial = idle;
                yield return new WaitForSeconds(0.15f);
            }

            meshRenderer.sharedMaterial = active;

            game.EnableInput();
        }

        public void SetIdle()
        {
            if (routine != null)
                StopCoroutine(routine);

            meshRenderer.sharedMaterial = idle;
        }
    }
}