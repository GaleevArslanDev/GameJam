using UnityEngine;

namespace Animation
{
    public class RhythmPlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private Vector3 lastPos;

        private static readonly int ForwardHash =
            Animator.StringToHash("Forward");

        private static readonly int SideHash =
            Animator.StringToHash("Side");

        private void Start()
        {
            lastPos = transform.position;
        }

        private void Update()
        {
            Vector3 delta =
                transform.position -
                lastPos;

            Vector3 local =
                transform.InverseTransformDirection(
                    delta / Time.deltaTime
                );

            animator.SetFloat(
                ForwardHash,
                local.z
            );

            animator.SetFloat(
                SideHash,
                local.x
            );

            lastPos = transform.position;
        }
    }
}