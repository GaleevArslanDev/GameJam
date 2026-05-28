using Minigames.Core;
using UnityEngine;

namespace Minigames.Reaction
{
    public class ReactionGame : MinigameBase
    {
        public static ReactionGame Instance;

        [Header("Gameplay")]
        [SerializeField]
        private int roundsToWin = 5;

        [SerializeField]
        private float delayBetweenRounds = 1f;

        [Header("Zone")]
        [SerializeField]
        private ReactionZone reactionZone;

        [SerializeField]
        private Transform leftBorder;

        [SerializeField]
        private Transform rightBorder;

        [Header("Difficulty")]
        [SerializeField]
        private float startZoneWidth = 2.5f;

        [SerializeField]
        private float endZoneWidth = 0.7f;
        
        [SerializeField]
        private float startParticlesRadius = 0.62f;

        private int currentRound;

        private bool active;

        public ReactionZone Zone => reactionZone;

        private void Awake()
        {
            Instance = this;
        }

        public override void StartGame()
        {
            base.StartGame();

            currentRound = 0;

            active = true;

            StartRound();
        }

        public override void StopGame()
        {
            base.StopGame();

            active = false;
        }

        public void CheckHit()
        {
            if (!active)
                return;

            if (reactionZone.SellerInside)
            {
                currentRound++;

                if (currentRound >= roundsToWin)
                {
                    WinGame();

                    return;
                }

                Invoke(
                    nameof(StartRound),
                    delayBetweenRounds
                );
            }
            else
            {
                LoseGame();
            }
        }

        private void StartRound()
        {
            float t =
                (float)currentRound /
                (roundsToWin - 1);

            float zoneWidth =
                Mathf.Lerp(
                    startZoneWidth,
                    endZoneWidth,
                    t
                );

            float minX = leftBorder.position.x;
            float maxX = rightBorder.position.x;

            float particleWidth = startParticlesRadius * zoneWidth;

            float randomX =
                Random.Range(
                    minX + zoneWidth * 0.5f,
                    maxX - zoneWidth * 0.5f
                );

            reactionZone.MoveTo(
                randomX,
                zoneWidth,
                particleWidth
            );
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