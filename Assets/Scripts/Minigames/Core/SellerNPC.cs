using UnityEngine;
using UnityEngine.InputSystem;
using Shopping;
using World;

namespace Minigames.Core
{
    public class SellerNPC : MonoBehaviour
    {
        [Header("Product")]
        [SerializeField] private ProductData product;

        [Header("Minigames")]
        [SerializeField] private MinigameBase[] minigamePrefabs;

        [Header("Movement")]
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float reachDistance = 0.15f;
        [SerializeField] private bool loop = true;

        [Header("Arena")]
        [SerializeField] private Transform arenaSpawnPoint;
        
        [SerializeField] private DepartmentProduct departmentProduct;
        
        public DepartmentProduct DepartmentProduct => departmentProduct;

        private int currentPointIndex;
        private bool playerInside;

        public Transform ArenaSpawnPoint => arenaSpawnPoint;
        public ProductData Product => product;

        private void Update()
        {
            Patrol();
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
    }
}