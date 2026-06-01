using Minigames.Core;
using UnityEngine;

namespace Minigames.Rhythm
{
    public class RhythmGame : MinigameBase
    {
        [SerializeField] private int roundsToWin = 6;
        [SerializeField] private float delayBetweenRounds = 1f;
        [SerializeField] private float timePerRound = 2f;
        
        [SerializeField] private RhythmTile forwardTile;
        [SerializeField] private RhythmTile backwardTile;
        [SerializeField] private RhythmTile leftTile;
        [SerializeField] private RhythmTile rightTile;
        
        [SerializeField] private Transform centerPoint;

        [SerializeField] private RhythmTile[] tiles;

        private RhythmTile currentTarget;
        private int currentRound;

        private float timer;
        private bool active;
        private bool inputEnabled;
        private bool resolved;
        
        public bool IsActive => active;

        public bool InputEnabled => inputEnabled;

        public override void StartGame()
        {
            base.StartGame();

            active = true;
            inputEnabled = false;
            resolved = false;
            currentRound = 0;

            foreach (var t in tiles)
                t.SetIdle();

            StartRound();
        }

        public override void StopGame()
        {
            base.StopGame();

            active = false;
            CancelInvoke();
        }

        private void Update()
        {
            if (!active || !inputEnabled)
                return;

            timer -= Time.deltaTime;

            if (timer <= 0)
                Finish(false);
        }

        private void StartRound()
        {
            resolved = false;
            inputEnabled = false;

            foreach (var t in tiles)
                t.SetIdle();

            currentTarget = tiles[Random.Range(0, tiles.Length)];
            currentTarget.Activate(this);
        }

        public void EnableInput()
        {
            timer = timePerRound;
            inputEnabled = true;
        }

        public void TryStep(RhythmTile tile)
        {
            if (!active || !inputEnabled || resolved)
                return;

            resolved = true;
            inputEnabled = false;

            if (tile == currentTarget)
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
        
        public override void BindSystems(MinigamePlayerSystems systems)
        {
            base.BindSystems(systems);
            systems.EnableRhythm(
                forwardTile,
                backwardTile,
                leftTile,
                rightTile,
                centerPoint,
                this
            );
        }
    }
}