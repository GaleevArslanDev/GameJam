using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.Reaction
{
    public class ReactionInput : MonoBehaviour
    {
        [SerializeField]
        private ReactionSeller seller;

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