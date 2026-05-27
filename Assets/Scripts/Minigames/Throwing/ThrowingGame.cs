using UnityEngine;

namespace Minigames.Throwing
{
    public class ThrowingGame : MonoBehaviour
    {
        public static ThrowingGame Instance;

        [SerializeField]
        private int hitsRequired = 9;

        private int currentHits;

        private bool active;

        private void Awake()
        {
            Instance = this;
        }

        public void BeginGame()
        {
            currentHits = 0;
            active = true;
        }

        public void RegisterHit()
        {
            if (!active)
                return;

            currentHits++;

            Debug.Log(
                "Hits: " +
                currentHits +
                "/" +
                hitsRequired
            );

            if (currentHits >= hitsRequired)
            {
                WinGame();
            }
        }

        private void WinGame()
        {
            active = false;

            Minigames.Core
                .MinigameController.Instance
                .FinishCurrentGame(true);
        }

        public void LoseGame()
        {
            if (!active)
                return;

            active = false;

            Minigames.Core
                .MinigameController.Instance
                .FinishCurrentGame(false);
        }
    }
}