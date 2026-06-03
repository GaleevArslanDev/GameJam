using UnityEngine;

namespace Animation
{
    public class SideMoveAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private Vector3 lastPos;

        private static readonly int SideHash =
            Animator.StringToHash("Side");

        private void Start()
        {
            lastPos = transform.position;
        }

        private void Update()
        {
            float delta =
                transform.position.x -
                lastPos.x;

            animator.SetFloat(
                SideHash,
                delta / Time.deltaTime
            );

            lastPos = transform.position;
        }
    }
}