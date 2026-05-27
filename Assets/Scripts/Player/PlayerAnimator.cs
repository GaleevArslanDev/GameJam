using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerMotor motor;
        [SerializeField] private PlayerInputReader input;

        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            float speed =
                new Vector2(
                    motor.MoveDirection.x,
                    motor.MoveDirection.z
                ).magnitude;

            animator.SetFloat("Speed", speed);

            animator.SetBool(
                "IsGrounded",
                motor.IsGrounded
            );

            animator.SetFloat(
                "VerticalVelocity",
                motor.VerticalVelocity
            );

            animator.SetBool(
                "IsSprinting",
                input.SprintHeld
            );
        }
    }
}