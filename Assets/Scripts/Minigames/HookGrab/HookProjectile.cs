using UnityEngine;

namespace Minigames.HookGrab
{
    public class HookProjectile : MonoBehaviour
    {
        [SerializeField]
        private float speed = 20f;

        [SerializeField]
        private float maxDistance = 15f;

        [SerializeField]
        private HookRope rope;

        private HookGrabPlayer owner;
        
        private HookSeller seller;

        private Vector3 startPosition;

        private bool returning;

        private HookTarget hookedTarget;

        public void Init(
            HookGrabPlayer player,
            HookSeller hookSeller
        )
        {
            owner = player;

            seller = hookSeller;

            startPosition =
                transform.position;

            rope.Init(
                player.HookSpawn,
                transform
            );
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (!returning)
            {
                transform.position +=
                    transform.forward *
                    speed *
                    Time.deltaTime;

                float distance =
                    Vector3.Distance(
                        startPosition,
                        transform.position
                    );

                if (distance >= maxDistance)
                {
                    StartReturn();
                }
            }
            else
            {
                Vector3 direction =
                    (
                        owner.HookSpawn.position -
                        transform.position
                    ).normalized;

                transform.position +=
                    direction *
                    speed *
                    Time.deltaTime;

                float distance =
                    Vector3.Distance(
                        transform.position,
                        owner.HookSpawn.position
                    );

                if (distance <= 0.3f)
                {
                    ReachPlayer();
                }
            }
        }

        private void StartReturn()
        {
            returning = true;
        }

        private void ReachPlayer()
        {
            if (hookedTarget != null)
            {
                HookGrabGame.Instance
                    .RegisterTargetHit(
                        hookedTarget
                    );

                hookedTarget.Collect();

                owner.SetMovementLocked(false);

                seller.SetPaused(false);
            }

            owner.Reload();

            Destroy(gameObject);
        }

        private void OnTriggerEnter(
            Collider other
        )
        {
            if (returning)
                return;

            HookTarget target =
                other.GetComponent<HookTarget>();

            if (target != null)
            {
                hookedTarget = target;

                target.HookTo(transform);

                owner.SetMovementLocked(true);

                seller.SetPaused(true);

                StartReturn();

                return;
            }

            if (
                other.CompareTag("Seller")
            )
            {
                HookGrabGame.Instance
                    .RegisterMiss();

                StartReturn();
            }
        }

        private void OnDestroy()
        {
            if (owner != null)
            {
                owner.Reload();

                owner.SetMovementLocked(false);
            }

            if (seller != null)
            {
                seller.SetPaused(false);
            }
        }
    }
}