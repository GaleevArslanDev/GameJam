using Shopping;
using UnityEngine;

namespace Core
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private float timeLimit = 480f;

        private float timeLeft;
        private bool running;

        public float Normalized => timeLeft / timeLimit;

        private void Start()
        {
            timeLeft = timeLimit;
            running = true;
        }

        private void Update()
        {
            if (!running) return;

            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0f)
            {
                timeLeft = 0f;
                running = false;

                ShoppingManager.Instance.Lose("Store closed");
            }
        }
    }
}