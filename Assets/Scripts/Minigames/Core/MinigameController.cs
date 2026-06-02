using System.Collections;
using Minigames.Core;
using Minigames.HookGrab;
using Minigames.Reaction;
using Minigames.Rhythm;
using Minigames.Throwing;
using Player;
using UnityEngine;
using Unity.Cinemachine;
using Minigames.UI;
using Shopping;

namespace Minigames.Core
{
    public class MinigameController : MonoBehaviour
    {
        public static MinigameController Instance { get; private set; }

        [SerializeField] private GameObject player;

        [SerializeField] private CharacterController controller;
        [SerializeField] private PlayerMotor motor;
        [SerializeField] private PlayerLook look;

        [SerializeField] private Transform cameraRoot;

        [SerializeField] private CinemachineCamera fpsCamera;
        [SerializeField] private CinemachineCamera minigameCamera;

        [Header("Systems")]
        [SerializeField] private MinigamePlayerSystems systems;

        [Header("UI")]
        [SerializeField] private ThrowingGameUI throwingUI;
        [SerializeField] private MinigameResultUI resultUI;

        private MinigameBase currentGame;
        private SellerNPC currentSeller;
        private MinigameContext context;
        
        private Vector3 savedPosition;

        public bool IsBusy { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void RequestStart(SellerNPC seller)
        {
            if (IsBusy) return;

            StartCoroutine(StartRoutine(seller));
        }

        private IEnumerator StartRoutine(SellerNPC seller)
        {
            IsBusy = true;
            currentSeller = seller;

            // 1. CREATE CONTEXT
            context = new MinigameContext
            {
                Player = player,
                PlayerTransform = player.transform,
                Systems = systems,
                ThrowingUI = throwingUI,
                ResultUI = resultUI,
                Seller = seller,
                CharacterController = controller,
                Motor = motor,
                Look = look
            };

            // 2. PICK GAME
            MinigameBase prefab = seller.GetRandomMinigame();

            currentGame = Instantiate(
                prefab,
                seller.ArenaSpawnPoint.position,
                seller.ArenaSpawnPoint.rotation
            );

            currentGame.Initialize(context);

            currentGame.OnFinished += OnGameFinished;

            // 3. DISABLE PLAYER CONTROL BEFORE TRANSITION
            motor.SetControlEnabled(false);
            look.SetControlEnabled(false);

            // 4. CAMERA SWITCH PREP (before spin)
            
            
            savedPosition = player.transform.position;
            
            Debug.Log("Before spin");

            // 5. SPIN TRANSITION + TELEPORT
            yield return SpinTransition.Instance.Play(() =>
            {
                controller.enabled = false;
                // TELEPORT PLAYER DURING SPIN (midAction)
                player.transform.position = currentGame.PlayerPoint.position;
                player.transform.rotation = currentGame.PlayerPoint.rotation;
                controller.enabled = true;
                fpsCamera.Priority = 0;
                minigameCamera.Priority = 20;
                currentSeller.HideSeller();

                // ALIGN CAMERA ROOT IF NEEDED
                if (currentGame.LookAtPoint != null)
                {
                    minigameCamera.Follow = currentGame.PlayerPoint;
                    minigameCamera.LookAt = currentGame.LookAtPoint;
                }
            });
            Debug.Log("Before binding");
            
            
            currentGame.BindSystems(systems);
            Debug.Log("Binded");

            // 6. RE-ENABLE CONTROLLER FOR MINIGAME (if needed)
            motor.SetControlEnabled(false);
            look.SetControlEnabled(false);

            // 7. START GAME
            currentGame.StartGame();
        }

        private void OnGameFinished(bool success)
        {
            StartCoroutine(FinishRoutine(success));
        }

        private IEnumerator FinishRoutine(bool success)
        {
            currentGame.OnFinished -= OnGameFinished;

            currentGame.StopGame();
            Destroy(currentGame.gameObject);
            
            var dept = currentSeller.DepartmentProduct;
            
            if (dept != null)
            {
                dept.ApplyDiscountResult(success);

                bool bought = ShoppingManager.Instance.TryBuy(
                    currentSeller.Product
                );

                if (bought)
                {
                    dept.enabled = false;
                }
            }
            else
            {
                Debug.LogError("No dept set in Seller.");
            }

            resultUI.Show(success);
            
            minigameCamera.Priority = 0;
            fpsCamera.Priority = 20;
            
            controller.enabled = false;
            player.transform.position = savedPosition;
            controller.enabled = true;

            yield return new WaitForSeconds(2f);

            resultUI.Hide();
            
            motor.SetControlEnabled(true);
            look.SetControlEnabled(true);

            systems.DisableAll();

            currentSeller.ShowSeller();

            currentGame = null;
            currentSeller = null;
            context = null;

            IsBusy = false;
        }
    }
}