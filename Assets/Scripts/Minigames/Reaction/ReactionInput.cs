using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.Reaction
{
    public class ReactionInput : MonoBehaviour
    {
        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (
                Mouse.current.leftButton
                .wasPressedThisFrame
            )
            {
                ReactionGame.Instance.CheckHit();
            }
        }
    }
}