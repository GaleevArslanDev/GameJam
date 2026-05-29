using System.Collections;
using UnityEngine;
using Player;
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

        [SerializeField]
        private CharacterController characterController;

        [Header("Cameras")]
        [SerializeField]
        private CinemachineCamera fpsCamera;

        [SerializeField]
        private CinemachineCamera minigameCamera;

        private Vector3 savedPosition;
        private Quaternion savedRotation;

        private MinigameBase currentGame;
        private SellerNPC currentSeller;
        
        public bool IsBusy { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void StartMinigame(
            MinigameBase game,
            SellerNPC seller
        )
        {
            if (IsBusy)
                return;

            IsBusy = true;

            StartCoroutine(
                StartMinigameRoutine(
                    game,
                    seller
                )
            );
        }

        private IEnumerator StartMinigameRoutine(
            MinigameBase game,
            SellerNPC seller
        )
        {
            currentGame = game;
            currentSeller = seller;

            yield return SpinTransition.Instance.Play(() =>
            {
                SavePlayerState();

                DisableFPSController();

                seller.HideSeller();

                TeleportPlayer(game.PlayerPoint);

                minigameCamera.LookAt = game.LookAtPoint;
                minigameCamera.Follow = game.LookAtPoint;

                EnableMinigameSystems();

                SwitchToMinigameCamera();

                currentGame.gameObject.SetActive(true);
            });
            currentGame.StartGame();

            currentGame.OnMinigameFinished +=
                FinishCurrentGame;
        }

        private void FinishCurrentGame(bool success)
        {
            StartCoroutine(
                FinishRoutine(success)
            );
        }

        private IEnumerator FinishRoutine(bool success)
        {
            currentGame.OnMinigameFinished -=
                FinishCurrentGame;

            currentGame.StopGame();

            Minigames.UI.MinigameResultUI
                .Instance.Show(success);
            
            DisableMinigameSystems();

            RestorePlayerState();

            currentSeller.ShowSeller();

            SwitchToFPSCamera();

            EnableFPSController();

            currentGame = null;
            currentSeller = null;

            yield return new WaitForSeconds(2f);

            Minigames.UI.MinigameResultUI
                .Instance.Hide();
            
            IsBusy = false;
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
            foreach (
                MonoBehaviour system
                in currentGame.GameplaySystems
            )
            {
                system.enabled = true;
            }

            Cursor.lockState =
                CursorLockMode.Locked;

            Cursor.visible = false;
        }

        private void DisableMinigameSystems()
        {
            foreach (
                MonoBehaviour system
                in currentGame.GameplaySystems
            )
            {
                system.enabled = false;
            }
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