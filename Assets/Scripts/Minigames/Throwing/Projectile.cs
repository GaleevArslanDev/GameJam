using UnityEngine;

namespace Minigames.Throwing
{
    public class Projectile : MonoBehaviour
    {
        private ThrowingGame game;
        private bool processed;

        public void Init(ThrowingGame g)
        {
            game = g;
        }

        private void Start()
        {
            Invoke(nameof(Miss), 5f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (processed) return;

            if (collision.collider.CompareTag("Seller"))
            {
                processed = true;
                game?.RegisterHit();
                Destroy(gameObject);
            }
        }

        private void Miss()
        {
            if (processed) return;

            processed = true;
            game?.RegisterMiss();
            Destroy(gameObject);
        }
    }
}