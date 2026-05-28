using Minigames.Core;
using Minigames.UI;
using UnityEngine;

namespace Minigames.Throwing
{
    public class ThrowingGame : MinigameBase
    {
        public static ThrowingGame Instance;

        [Header("Game")]
        [SerializeField]
        private int hitsRequired = 9;

        [SerializeField]
        private int maxMisses = 5;

        private int currentHits;
        private int currentMisses;

        private bool active;

        public int HitsRequired => hitsRequired;
        public int MaxMisses => maxMisses;

        private void Awake()
        {
            Instance = this;
        }

        public override void StartGame()
        {
            base.StartGame();

            currentHits = 0;
            currentMisses = 0;

            active = true;

            UpdateUI();
        }

        public override void StopGame()
        {
            base.StopGame();
            
            Projectile[] projectiles =
                FindObjectsByType<Projectile>(
                    FindObjectsSortMode.None
                );

            foreach (Projectile projectile in projectiles)
            {
                Destroy(projectile.gameObject);
            }

            active = false;
        }

        public void RegisterHit()
        {
            if (!active)
                return;

            currentHits++;

            UpdateUI();

            if (currentHits >= hitsRequired)
            {
                WinGame();
            }
        }

        public void RegisterMiss()
        {
            if (!active)
                return;

            currentMisses++;

            UpdateUI();

            if (currentMisses >= maxMisses)
            {
                LoseGame();
            }
        }

        private void UpdateUI()
        {
            ThrowingGameUI.Instance.UpdateHits(
                currentHits,
                hitsRequired
            );

            ThrowingGameUI.Instance.UpdateMisses(
                currentMisses,
                maxMisses
            );
        }

        private void WinGame()
        {
            active = false;

            Finish(true);
        }

        public void LoseGame()
        {
            if (!active)
                return;

            active = false;

            Finish(false);
        }
    }
}