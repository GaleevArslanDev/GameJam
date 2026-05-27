using UnityEngine;
using UnityEngine.InputSystem;
using Minigames.Throwing;

namespace Minigames.Core
{
    public class MinigameTrigger : MonoBehaviour
    {
        [SerializeField] private ThrowingGame throwingGame;

        [SerializeField] private Transform playerTeleportPoint;

        private bool playerInside;

        private void Update()
        {
            if (!playerInside)
                return;

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                MinigameController.Instance
                    .StartThrowingGame(
                        throwingGame,
                        playerTeleportPoint
                    );
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInside = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInside = false;
            }
        }
    }
}