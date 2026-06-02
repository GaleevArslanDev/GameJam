using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputReader : MonoBehaviour
    {
        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }

        public bool JumpPressed { get; private set; }
        public bool SprintHeld { get; private set; }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            JumpPressed = context.ReadValueAsButton();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            SprintHeld = context.ReadValueAsButton();
        }
    }
}