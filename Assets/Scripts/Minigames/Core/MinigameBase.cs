using System;
using UnityEngine;

namespace Minigames.Core
{
    public abstract class MinigameBase : MonoBehaviour, IMinigame
    {
        public event Action<bool> OnMinigameFinished;

        [Header("Setup")]
        [SerializeField] protected Transform playerPoint;

        [SerializeField] protected GameObject minigameRoot;

        public Transform PlayerPoint => playerPoint;

        public virtual void StartGame()
        {
            minigameRoot.SetActive(true);
        }

        public virtual void StopGame()
        {
            minigameRoot.SetActive(false);
        }

        protected void Finish(bool success)
        {
            OnMinigameFinished?.Invoke(success);
        }
    }
}