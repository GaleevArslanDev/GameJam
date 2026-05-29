using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMotor : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerInputReader input;
        [SerializeField] private PlayerSlipState slipState;

        private CharacterController controller;

        [Header("Movement")]
        [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float sprintSpeed = 7f;

        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float deceleration = 15f;

        [Header("Jump")]
        [SerializeField] private float jumpForce = 7f;

        [SerializeField] private float gravity = -25f;

        [SerializeField] private float fallMultiplier = 2f;

        [SerializeField] private float groundedGravity = -2f;

        [Header("Ground Check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundRadius = 0.3f;
        [SerializeField] private LayerMask groundMask;

        [Header("Air Control")]
        [SerializeField] private float airControl = 0.5f;

        private Vector3 velocity;
        private Vector3 currentMove;
        
        private Vector3 externalMove;
        private float controlMultiplier = 1f;

        private bool isGrounded;
        private bool jumpConsumed;

        private bool controlsEnabled = true;

        public Vector3 MoveDirection => currentMove;
        public bool IsGrounded => isGrounded;
        public float VerticalVelocity => velocity.y;
        
        public void ApplyExternalMovement(Vector3 move)
        {
            externalMove += move;
        }

        public void SetExternalSlowdown(float value)
        {
            controlMultiplier = Mathf.Clamp01(value);
        }

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        public void SetControlEnabled(bool enabled)
        {
            controlsEnabled = enabled;

            if (!enabled)
            {
                currentMove = Vector3.zero;
                velocity = Vector3.zero;
            }
        }

        private void Update()
        {
            GroundCheck();

            if (!controlsEnabled)
            {
                ApplyGravity();
                return;
            }

            Move();

            Jump();

            ApplyGravity();
        }

        private void GroundCheck()
        {
            isGrounded = Physics.CheckSphere(
                groundCheck.position,
                groundRadius,
                groundMask
            );

            if (isGrounded)
            {
                jumpConsumed = false;

                if (velocity.y < 0)
                {
                    velocity.y = groundedGravity;
                }
            }
        }

        private void Move()
        {
            Vector2 moveInput = input.Move;

            Vector3 inputMove =
                transform.right * moveInput.x +
                transform.forward * moveInput.y;

            float targetSpeed =
                input.SprintHeld ? sprintSpeed : walkSpeed;

            Vector3 targetMove = inputMove * targetSpeed;

            targetMove *= controlMultiplier;

            float control = isGrounded ? 1f : airControl;

            float smooth =
                moveInput.magnitude > 0.1f
                    ? acceleration
                    : deceleration;

            Vector3 desiredMove = Vector3.Lerp(
                currentMove,
                targetMove,
                smooth * control * Time.deltaTime
            );

            desiredMove += externalMove;

            externalMove = Vector3.Lerp(
                externalMove,
                Vector3.zero,
                5f * Time.deltaTime
            );

            currentMove = desiredMove;

            controller.Move(currentMove * Time.deltaTime);
        }

        private void Jump()
        {
            if (!input.JumpPressed) return;
            if (!isGrounded) return;
            if (jumpConsumed) return;

            jumpConsumed = true;

            velocity.y = jumpForce;

            input.ConsumeJump();
        }

        private void ApplyGravity()
        {
            float g = gravity;

            if (velocity.y < 0)
                g *= fallMultiplier;

            velocity.y += g * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
}