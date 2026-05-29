using Minigames.Core;
using UnityEngine;

namespace Minigames.HookGrab
{
    public class HookGrabGame : MinigameBase
    {
        public static HookGrabGame Instance;

        [Header("Targets")]
        [SerializeField]
        private HookTarget[] targets;

        [Header("Game")]
        [SerializeField]
        private int discountsNeeded = 5;

        [SerializeField]
        private int maxMisses = 3;

        [SerializeField]
        private float nextTargetDelay = 1f;

        private int currentDiscounts;
        private int currentMisses;

        private HookTarget currentTarget;

        private bool active;

        private void Awake()
        {
            Instance = this;
        }

        public override void StartGame()
        {
            base.StartGame();

            active = true;
            
            foreach (HookTarget target in targets)
            {
                target.ResetTarget();
            }

            currentDiscounts = 0;
            currentMisses = 0;

            ActivateRandomTarget();
        }

        public override void StopGame()
        {
            base.StopGame();

            active = false;

            foreach (HookTarget target in targets)
            {
                target.gameObject.SetActive(false);
                target.SetInactive();
            }
        }

        private void ActivateRandomTarget()
        {
            foreach (HookTarget target in targets)
            {
                if (target.gameObject.activeSelf)
                {
                    target.SetInactive();
                }
            }

            HookTarget randomTarget = null;

            int safety = 50;

            while (
                randomTarget == null &&
                safety > 0
            )
            {
                safety--;

                int index =
                    Random.Range(
                        0,
                        targets.Length
                    );

                if (
                    targets[index]
                    .gameObject.activeSelf
                )
                {
                    randomTarget =
                        targets[index];
                }
            }

            if (randomTarget == null)
            {
                WinGame();
                return;
            }

            currentTarget = randomTarget;

            currentTarget.SetActiveTarget();
        }

        public void RegisterTargetHit(
            HookTarget target
        )
        {
            if (!active)
                return;

            if (target != currentTarget)
            {
                RegisterMiss();
                return;
            }

            currentDiscounts++;

            if (currentDiscounts >= discountsNeeded)
            {
                WinGame();
                return;
            }

            Invoke(
                nameof(ActivateRandomTarget),
                nextTargetDelay
            );
        }

        public void RegisterMiss()
        {
            if (!active)
                return;

            currentMisses++;

            if (currentMisses >= maxMisses)
            {
                LoseGame();
            }
        }

        private void WinGame()
        {
            active = false;

            Finish(true);
        }

        private void LoseGame()
        {
            active = false;

            Finish(false);
        }
    }
}