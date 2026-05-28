using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.Core
{
    public class SellerNPC : MonoBehaviour
    {
        [Header("Minigames")]
        [SerializeField]
        private MinigameBase[] minigames;

        [Header("Patrol")]
        [SerializeField]
        private Transform[] patrolPoints;

        [SerializeField]
        private float moveSpeed = 2f;

        [SerializeField]
        private float reachDistance = 0.15f;

        [SerializeField]
        private bool loop = true;

        private int currentPointIndex;
        private bool playerInside;

        private void Update()
        {
            Patrol();

            if (!playerInside)
                return;

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                StartRandomGame();
            }
        }

        private void Patrol()
        {
            if (patrolPoints == null)
                return;

            if (patrolPoints.Length < 2)
                return;

            Transform targetPoint =
                patrolPoints[currentPointIndex];

            Vector3 direction =
                (
                    targetPoint.position -
                    transform.position
                ).normalized;

            transform.position =
                Vector3.MoveTowards(
                    transform.position,
                    targetPoint.position,
                    moveSpeed * Time.deltaTime
                );

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation =
                    Quaternion.LookRotation(direction);

                transform.rotation =
                    Quaternion.Slerp(
                        transform.rotation,
                        targetRotation,
                        8f * Time.deltaTime
                    );
            }

            float distance =
                Vector3.Distance(
                    transform.position,
                    targetPoint.position
                );

            if (distance <= reachDistance)
            {
                currentPointIndex++;

                if (
                    currentPointIndex >=
                    patrolPoints.Length
                )
                {
                    if (loop)
                    {
                        currentPointIndex = 0;
                    }
                    else
                    {
                        currentPointIndex =
                            patrolPoints.Length - 1;
                    }
                }
            }
        }

        private void StartRandomGame()
        {
            if (minigames.Length == 0)
                return;

            int index =
                Random.Range(
                    0,
                    minigames.Length
                );

            MinigameController.Instance
                .StartMinigame(
                    minigames[index],
                    this
                );
        }

        public void HideSeller()
        {
            gameObject.SetActive(false);
        }

        public void ShowSeller()
        {
            gameObject.SetActive(true);
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

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (
                patrolPoints == null ||
                patrolPoints.Length == 0
            )
            {
                return;
            }

            Gizmos.color = Color.yellow;

            for (
                int i = 0;
                i < patrolPoints.Length;
                i++
            )
            {
                if (patrolPoints[i] == null)
                    continue;

                Gizmos.DrawSphere(
                    patrolPoints[i].position,
                    0.2f
                );

                if (i < patrolPoints.Length - 1)
                {
                    Gizmos.DrawLine(
                        patrolPoints[i].position,
                        patrolPoints[i + 1].position
                    );
                }
            }

            if (loop && patrolPoints.Length > 1)
            {
                Gizmos.DrawLine(
                    patrolPoints[
                        patrolPoints.Length - 1
                    ].position,
                    patrolPoints[0].position
                );
            }
        }
#endif
    }
}