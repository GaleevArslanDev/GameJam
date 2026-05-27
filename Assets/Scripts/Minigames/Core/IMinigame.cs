using System;

namespace Minigames.Core
{
    public interface IMinigame
    {
        event Action<bool> OnMinigameFinished;

        void StartGame();
        void StopGame();
    }
}