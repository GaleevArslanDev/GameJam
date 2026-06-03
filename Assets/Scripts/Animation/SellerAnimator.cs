using UnityEngine;

namespace Animation
{
    public class SellerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private Vector3 lastPosition;

        private static readonly int SpeedHash =
            Animator.StringToHash("Speed");

        private void Start()
        {
            lastPosition = transform.position;
        }

        private void Update()
        {
            Vector3 velocity =
                (transform.position - lastPosition) /
                Time.deltaTime;

            float speed =
                new Vector3(
                    velocity.x,
                    0,
                    velocity.z
                ).magnitude;

            animator.SetFloat(
                SpeedHash,
                speed
            );

            lastPosition = transform.position;
        }
    }
}