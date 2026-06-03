using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerMotor motor;
        [SerializeField] private PlayerInputReader input;
        [SerializeField] private PlayerSlipState slipState;

        private static readonly int SpeedHash =
            Animator.StringToHash("Speed");

        private static readonly int SprintHash =
            Animator.StringToHash("Sprint");

        private static readonly int SlipHash =
            Animator.StringToHash("Slip");

        private void Update()
        {
            float speed =
                new Vector2(
                    motor.MoveDirection.x,
                    motor.MoveDirection.z
                ).magnitude;

            animator.SetFloat(
                SpeedHash,
                speed
            );

            animator.SetBool(
                SprintHash,
                input.SprintHeld
            );

            animator.SetBool(
                SlipHash,
                slipState.IsSlipping
            );
        }
    }
}