using UnityEngine;

namespace Animation
{
    public class HookPlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private static readonly int ShootHash =
            Animator.StringToHash("Shoot");

        public void Shoot()
        {
            animator.SetTrigger(
                ShootHash
            );
        }
    }
}