using UnityEngine;
using Player;
using Minigames.Throwing;
using Unity.Cinemachine;

namespace Minigames.Core
{
    public class MinigameController : MonoBehaviour
    {
        public static MinigameController Instance;

        [Header("Player")]
        [SerializeField] private GameObject player;

        [SerializeField] private PlayerMotor playerMotor;
        [SerializeField] private PlayerLook playerLook;

        [SerializeField] private CharacterController characterController;

        [Header("Cameras")]
        [SerializeField] private CinemachineCamera fpsCamera;
        [SerializeField] private CinemachineCamera minigameCamera;

        [Header("Minigame")]
        [SerializeField] private SideMoveController sideMoveController;
        [SerializeField] private ProjectileThrower projectileThrower;

        private Vector3 savedPosition;
        private Quaternion savedRotation;

        private ThrowingGame currentGame;

        private void Awake()
        {
            Instance = this;
        }

        public void StartThrowingGame(
            ThrowingGame game,
            Transform playerPoint
        )
        {
            currentGame = game;

            SavePlayerState();

            DisableFPSController();

            TeleportPlayer(playerPoint);

            EnableMinigameSystems();

            SwitchToMinigameCamera();

            currentGame.BeginGame();
        }

        public void FinishCurrentGame(bool success)
        {
            Debug.Log(
                success
                ? "MINIGAME WIN"
                : "MINIGAME LOSE"
            );

            DisableMinigameSystems();

            RestorePlayerState();

            SwitchToFPSCamera();

            EnableFPSController();

            currentGame = null;
        }

        private void SavePlayerState()
        {
            savedPosition = player.transform.position;
            savedRotation = player.transform.rotation;
        }

        private void RestorePlayerState()
        {
            characterController.enabled = false;

            player.transform.position = savedPosition;
            player.transform.rotation = savedRotation;

            characterController.enabled = true;
        }

        private void TeleportPlayer(Transform point)
        {
            characterController.enabled = false;

            player.transform.position = point.position;
            player.transform.rotation = point.rotation;

            characterController.enabled = true;
        }

        private void DisableFPSController()
        {
            playerMotor.enabled = false;
            playerLook.enabled = false;
        }

        private void EnableFPSController()
        {
            playerMotor.enabled = true;
            playerLook.enabled = true;
        }

        private void EnableMinigameSystems()
        {
            sideMoveController.EnableControl();
            projectileThrower.EnableThrowing();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void DisableMinigameSystems()
        {
            sideMoveController.DisableControl();
            projectileThrower.DisableThrowing();
        }

        private void SwitchToMinigameCamera()
        {
            fpsCamera.Priority = 0;
            minigameCamera.Priority = 20;
        }

        private void SwitchToFPSCamera()
        {
            fpsCamera.Priority = 20;
            minigameCamera.Priority = 0;
        }
    }
}