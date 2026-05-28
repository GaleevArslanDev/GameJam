using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.Rhythm
{
    public class RhythmPlayerMover : MonoBehaviour
    {
        [Header("Tiles")]
        [SerializeField]
        private RhythmTile forwardTile;

        [SerializeField]
        private RhythmTile backwardTile;

        [SerializeField]
        private RhythmTile leftTile;

        [SerializeField]
        private RhythmTile rightTile;

        [Header("Current")]
        [SerializeField]
        private RhythmTile currentTile;

        [Header("Movement")]
        [SerializeField]
        private float moveSpeed = 10f;

        [SerializeField]
        private float returnSpeed = 8f;

        [SerializeField]
        private float playerY = 1f;

        [Header("Center")]
        [SerializeField]
        private Transform centerPoint;

        private Vector3 targetPosition;

        private RhythmTile targetTile;

        private bool moving;

        private Coroutine returnRoutine;

        private void Awake()
        {
            enabled = false;
        }

        private void OnDisable()
        {
            StopAllCoroutines();

            moving = false;
        }

        private void Update()
        {
            HandleInput();

            Move();
        }

        private void HandleInput()
        {
            if (!RhythmGame.Instance.InputEnabled)
                return;

            if (moving)
                return;

            if (Keyboard.current.wKey.wasPressedThisFrame)
            {
                StartMove(forwardTile);
            }

            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                StartMove(backwardTile);
            }

            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                StartMove(leftTile);
            }

            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                StartMove(rightTile);
            }
        }

        private void StartMove(
            RhythmTile tile
        )
        {
            if (tile == null)
                return;

            if (returnRoutine != null)
            {
                StopCoroutine(returnRoutine);
            }

            targetTile = tile;

            targetPosition =
                new Vector3(
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

            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    moveSpeed * Time.deltaTime
                );

            if (
                Vector3.Distance(
                    transform.position,
                    targetPosition
                ) <= 0.02f
            )
            {
                transform.position =
                    targetPosition;

                moving = false;

                currentTile = targetTile;

                RhythmGame.Instance.TryStep(currentTile);

                if (RhythmGame.Instance.IsActive)
                {
                    returnRoutine =
                        StartCoroutine(
                            ReturnToCenterRoutine()
                        );
                }
            }
        }

        private IEnumerator ReturnToCenterRoutine()
        {
            yield return new WaitForSeconds(0.1f);

            while (
                Vector3.Distance(
                    transform.position,
                    centerPoint.position
                ) > 0.02f
            )
            {
                transform.position =
                    Vector3.MoveTowards(
                        transform.position,
                        centerPoint.position,
                        returnSpeed * Time.deltaTime
                    );

                yield return null;
            }

            transform.position =
                centerPoint.position;
        }
    }
}