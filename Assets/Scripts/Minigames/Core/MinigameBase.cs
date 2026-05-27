using System;
using UnityEngine;

namespace Minigames.Core
{
    public abstract class MinigameBase : MonoBehaviour, IMinigame
    {
        public event Action<bool> OnMinigameFinished;

        public virtual void StartGame()
        {
            gameObject.SetActive(true);
        }

        public virtual void StopGame()
        {
            gameObject.SetActive(false);
        }

        protected void Finish(bool success)
        {
            OnMinigameFinished?.Invoke(success);
        }
    }
}