using Minigames.Core;
using UnityEngine;

namespace Minigames.HookGrab
{
    public class HookGrabGame : MinigameBase
    {
        [Header("Targets")]
        [SerializeField] private HookTarget[] targets;

        [Header("Game")]
        [SerializeField] private int discountsNeeded = 5;
        [SerializeField] private int maxMisses = 3;
        [SerializeField] private float nextTargetDelay = 1f;
        [SerializeField] private HookSeller seller;

        private int currentDiscounts;
        private int currentMisses;

        private HookTarget currentTarget;
        private bool active;

        public override void StartGame()
        {
            base.StartGame();

            active = true;
            currentDiscounts = 0;
            currentMisses = 0;

            foreach (var t in targets)
                t.ResetTarget();

            ActivateRandomTarget();
        }

        public override void StopGame()
        {
            base.StopGame();

            active = false;
            
            CancelInvoke();

            foreach (var t in targets)
                t.gameObject.SetActive(false);
        }

        private void ActivateRandomTarget()
        {
            foreach (var t in targets)
                t.SetInactive();

            HookTarget pick = null;
            int safety = 50;

            while (pick == null && safety-- > 0)
            {
                var t = targets[Random.Range(0, targets.Length)];
                if (t.gameObject.activeSelf)
                    pick = t;
            }

            if (pick == null)
            {
                Finish(true);
                return;
            }

            currentTarget = pick;
            currentTarget.SetActiveTarget();
        }

        public void RegisterTargetHit(HookTarget target)
        {
            if (!active) return;

            if (target != currentTarget)
            {
                RegisterMiss();
                return;
            }

            currentDiscounts++;

            if (currentDiscounts >= discountsNeeded)
            {
                Finish(true);
                return;
            }

            Invoke(nameof(ActivateRandomTarget), nextTargetDelay);
        }

        public void RegisterMiss()
        {
            if (!active) return;

            currentMisses++;

            if (currentMisses >= maxMisses)
                Finish(false);
        }
        
        public override void BindSystems(MinigamePlayerSystems systems)
        {
            base.BindSystems(systems);
            systems.EnableHook(seller, this);
        }
    }
}