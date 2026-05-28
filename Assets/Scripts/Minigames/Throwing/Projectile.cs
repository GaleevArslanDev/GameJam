using UnityEngine;

namespace Minigames.Throwing
{
    public class Projectile : MonoBehaviour
    {
        private bool processed;

        private void Start()
        {
            Invoke(nameof(Miss), 5f);
        }

        private void OnCollisionEnter(
            Collision collision
        )
        {
            if (processed)
                return;

            if (
                collision.collider.CompareTag("Seller")
            )
            {
                processed = true;

                ThrowingGame.Instance.RegisterHit();

                Destroy(gameObject);
            }
        }

        private void Miss()
        {
            if (processed)
                return;

            processed = true;

            ThrowingGame.Instance.RegisterMiss();

            Destroy(gameObject);
        }
    }
}