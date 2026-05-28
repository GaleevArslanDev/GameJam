using Minigames.Core;
using UnityEngine;

namespace Minigames.Rhythm
{
    public class RhythmGame : MinigameBase
    {
        public static RhythmGame Instance;

        [Header("Game")]
        [SerializeField]
        private int roundsToWin = 6;

        [SerializeField]
        private float delayBetweenRounds = 1f;

        [SerializeField]
        private float timePerRound = 2f;

        [Header("Tiles")]
        [SerializeField]
        private RhythmTile[] tiles;

        private RhythmTile currentTarget;

        private int currentRound;

        private float timer;

        private bool active;
        private bool inputEnabled;
        private bool roundResolved;
        
        public bool IsActive => active;
        public bool InputEnabled => inputEnabled;

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

            inputEnabled = false;

            CancelInvoke();

            foreach (RhythmTile tile in tiles)
            {
                tile.SetIdle();
            }
        }

        private void Update()
        {
            if (!active)
                return;

            if (!inputEnabled)
                return;

            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                LoseGame();
            }
        }

        private void StartRound()
        {
            roundResolved = false;

            inputEnabled = false;

            foreach (RhythmTile tile in tiles)
            {
                tile.SetIdle();
            }

            int index =
                Random.Range(0, tiles.Length);

            currentTarget = tiles[index];

            currentTarget.Activate();
        }

        public void EnableInput()
        {
            timer = timePerRound;

            inputEnabled = true;
        }

        public void TryStep(
            RhythmTile steppedTile
        )
        {
            if (!active)
                return;

            if (!inputEnabled)
                return;

            if (roundResolved)
                return;

            roundResolved = true;

            inputEnabled = false;

            if (steppedTile == currentTarget)
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