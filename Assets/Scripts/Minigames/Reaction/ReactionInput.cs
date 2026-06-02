using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.Reaction
{
    public class ReactionInput : MonoBehaviour
    {
        private ReactionGame game;

        public void Init(ReactionGame g)
        {
            game = g;
        }

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
                game?.CheckHit();
        }
    }
}