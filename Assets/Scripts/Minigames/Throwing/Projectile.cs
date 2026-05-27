using UnityEngine;

namespace Minigames.Throwing
{
    public class Projectile : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 5f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (
                collision.collider.CompareTag("Seller")
            )
            {
                ThrowingGame.Instance.RegisterHit();
            }

            Destroy(gameObject);
        }
    }
}