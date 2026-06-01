using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames.Core
{
    public class SellerNPC : MonoBehaviour
    {
        [SerializeField] private MinigameBase[] minigamePrefabs;
        [SerializeField] private Transform arenaSpawnPoint;

        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float reachDistance = 0.15f;
        [SerializeField] private bool loop = true;

        private int currentPointIndex;
        private bool playerInside;
        
        public System.Action<SellerNPC> OnInteract;

        public Transform ArenaSpawnPoint => arenaSpawnPoint;

        private void Update()
        {
            Patrol();

            if (!playerInside) return;

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                MinigameController.Instance.RequestStart(this);
            }
        }

        private void Patrol()
        {
            if (patrolPoints == null || patrolPoints.Length < 2)
                return;

            Transform target = patrolPoints[currentPointIndex];

            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, target.position) <= reachDistance)
            {
                currentPointIndex++;

                if (currentPointIndex >= patrolPoints.Length)
                    currentPointIndex = loop ? 0 : patrolPoints.Length - 1;
            }
        }

        public MinigameBase GetRandomMinigame()
        {
            return minigamePrefabs[Random.Range(0, minigamePrefabs.Length)];
        }

        public void HideSeller() => gameObject.SetActive(false);
        public void ShowSeller() => gameObject.SetActive(true);

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                playerInside = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                playerInside = false;
        }
    }
}