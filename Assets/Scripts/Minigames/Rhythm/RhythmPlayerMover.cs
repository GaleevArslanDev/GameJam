using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Minigames.Rhythm;

namespace Minigames.Rhythm
{
    public class RhythmPlayerMover : MonoBehaviour
    {
        [HideInInspector] public RhythmTile forwardTile;
        [HideInInspector] public RhythmTile backwardTile;
        [HideInInspector] public RhythmTile leftTile;
        [HideInInspector] public RhythmTile rightTile;

        [Header("Movement")]
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float returnSpeed = 8f;
        [SerializeField] private float playerY = 1f;

        [HideInInspector] public Transform centerPoint;

        private RhythmGame game;

        private Vector3 targetPosition;
        private RhythmTile targetTile;

        private bool moving;
        private Coroutine returnRoutine;

        private bool initialized;

        public void Initialize(RhythmGame rhythmGame)
        {
            game = rhythmGame;
            initialized = true;
        }

        private void Awake()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            moving = false;
            targetTile = null;

            if (returnRoutine != null)
            {
                StopCoroutine(returnRoutine);
                returnRoutine = null;
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            moving = false;
        }

        private void Update()
        {
            if (!initialized || game == null)
                return;

            HandleInput();
            Move();
        }

        private void HandleInput()
        {
            if (!game.InputEnabled)
                return;

            if (moving)
                return;

            if (Keyboard.current.wKey.wasPressedThisFrame)
                StartMove(forwardTile);

            if (Keyboard.current.sKey.wasPressedThisFrame)
                StartMove(backwardTile);

            if (Keyboard.current.aKey.wasPressedThisFrame)
                StartMove(leftTile);

            if (Keyboard.current.dKey.wasPressedThisFrame)
                StartMove(rightTile);
        }

        private void StartMove(RhythmTile tile)
        {
            if (tile == null)
                return;

            if (returnRoutine != null)
            {
                StopCoroutine(returnRoutine);
                returnRoutine = null;
            }

            targetTile = tile;
            Audio.AudioManager.Instance.PlayRhythmJump();

            targetPosition = new Vector3(
                tile.transform.position.x,
                playerY,
                tile.transform.position.z
            );

            moving = true;
        }

        private void Move()
        {
            if (!moving)
                return;

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) <= 0.02f)
            {
                transform.position = targetPosition;
                moving = false;

                game.TryStep(targetTile);

                if (game.IsActive)
                {
                    returnRoutine = StartCoroutine(ReturnToCenterRoutine());
                }
            }
        }

        private IEnumerator ReturnToCenterRoutine()
        {
            yield return new WaitForSeconds(0.1f);

            while (Vector3.Distance(transform.position, centerPoint.position) > 0.02f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    centerPoint.position,
                    returnSpeed * Time.deltaTime
                );

                yield return null;
            }

            transform.position = centerPoint.position;
        }
    }
}