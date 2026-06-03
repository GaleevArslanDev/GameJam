using System;
using UnityEngine;

namespace Minigames.Core
{
    public abstract class MinigameBase : MonoBehaviour
    {
        public event Action<bool> OnFinished;

        [SerializeField] protected Transform playerPoint;
        [SerializeField] protected Transform lookAtPoint;
        [SerializeField] protected GameObject root;
        [SerializeField] protected Transform sellerRoot;

        public MinigameContext Context { get; protected set; }

        public Transform PlayerPoint => playerPoint;
        public Transform LookAtPoint => lookAtPoint;

        public virtual void Initialize(MinigameContext context)
        {
           
           Context = context;
        }

        public virtual void StartGame()
        {
            root.SetActive(true);
        }

        public virtual void StopGame()
        {
            root.SetActive(false);
        }

        protected void Finish(bool success)
        {
            OnFinished?.Invoke(success);
        }

        public virtual void BindSystems(MinigamePlayerSystems systems)
        {
            Debug.Log("BindSystems on " + gameObject.name);
        }
    }
}