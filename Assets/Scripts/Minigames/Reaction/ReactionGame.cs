using Minigames.Core;
using UnityEngine;

namespace Minigames.Reaction
{
    public class ReactionGame : MinigameBase
    {
        [Header("Gameplay")]
        [SerializeField] private int roundsToWin = 5;
        [SerializeField] private float delayBetweenRounds = 1f;

        [Header("Zone")]
        [SerializeField] private ReactionZone reactionZone;
        [SerializeField] private Transform leftBorder;
        [SerializeField] private Transform rightBorder;

        [Header("Difficulty")]
        [SerializeField] private float startZoneWidth = 2.5f;
        [SerializeField] private float endZoneWidth = 0.7f;
        [SerializeField] private float startParticlesRadius = 0.62f;

        private int currentRound;
        private bool active;
        private bool waitingNext;

        public override void StartGame()
        {
            base.StartGame();

            active = true;
            waitingNext = false;
            currentRound = 0;

            StartRound();
        }

        public override void StopGame()
        {
            base.StopGame();

            active = false;
            reactionZone.ResetZone();
        }

        public void CheckHit()
        {
            if (!active || waitingNext || reactionZone.IsTransitioning)
                return;

            waitingNext = true;

            if (reactionZone.SellerInside)
            {
                currentRound++;

                if (currentRound >= roundsToWin)
                {
                    Finish(true);
                    return;
                }

                Invoke(nameof(StartRound), delayBetweenRounds);
            }
            else
            {
                Finish(false);
            }
        }

        private void StartRound()
        {
            waitingNext = false;

            float t = (float)currentRound / (roundsToWin - 1);

            float width = Mathf.Lerp(startZoneWidth, endZoneWidth, t);

            float minX = leftBorder.position.x;
            float maxX = rightBorder.position.x;

            float x = Random.Range(minX + width * 0.5f, maxX - width * 0.5f);

            reactionZone.MoveTo(x, width, startParticlesRadius * width);
        }
        
        public override void BindSystems(MinigamePlayerSystems systems)
        {
            base.BindSystems(systems);
            systems.EnableReaction(this);
        }
    }
}