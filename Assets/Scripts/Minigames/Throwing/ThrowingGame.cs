using Minigames.Core;
using Minigames.UI;
using UnityEngine;

namespace Minigames.Throwing
{
    public class ThrowingGame : MinigameBase
    {
        [SerializeField] private int hitsRequired = 9;
        [SerializeField] private int maxMisses = 5;

        private int hits;
        private int misses;

        private bool active;

        public override void StartGame()
        {
            base.StartGame();

            hits = 0;
            misses = 0;
            active = true;

            UpdateUI();
        }

        public void RegisterHit()
        {
            if (!active) return;

            hits++;
            UpdateUI();

            if (hits >= hitsRequired)
                Finish(true);
        }

        public void RegisterMiss()
        {
            if (!active) return;

            misses++;
            UpdateUI();

            if (misses >= maxMisses)
                Finish(false);
        }

        private void UpdateUI()
        {
            Context.ThrowingUI.UpdateHits(hits, hitsRequired);
            Context.ThrowingUI.UpdateMisses(misses, maxMisses);
        }
        
        public override void BindSystems(MinigamePlayerSystems systems)
        {
            base.BindSystems(systems);
            systems.EnableThrowing(this);
        }
    }
}