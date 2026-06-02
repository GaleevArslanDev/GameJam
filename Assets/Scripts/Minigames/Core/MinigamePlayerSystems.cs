using Minigames.HookGrab;
using Minigames.Reaction;
using Minigames.Rhythm;
using Minigames.Throwing;
using UnityEngine;

namespace Minigames.Core
{
    public class MinigamePlayerSystems : MonoBehaviour
    {
        [field: SerializeField] public SideMoveController SideMove { get; private set; }
        [field: SerializeField] public ProjectileThrower ProjectileThrower { get; private set; }
        [field: SerializeField] public RhythmPlayerMover RhythmMover { get; private set; }
        [field: SerializeField] public HookGrabPlayer HookPlayer { get; private set; }
        [field: SerializeField] public ReactionInput ReactionInput { get; private set; }

        public void EnableThrowing(ThrowingGame game)
        {
            Debug.Log("EnableThrowing on " + gameObject.name);
            DisableAll();

            ProjectileThrower.Init(game);

            SideMove.enabled = true;
            ProjectileThrower.enabled = true;
        }

        public void EnableRhythm(
            RhythmTile forwardTile,
            RhythmTile backwardTile,
            RhythmTile leftTile,
            RhythmTile rightTile,
            Transform centerPoint,
            RhythmGame game)
        {
            Debug.Log("EnableRhythm on " + gameObject.name);
            DisableAll();

            RhythmMover.Initialize(game);

            RhythmMover.forwardTile = forwardTile;
            RhythmMover.backwardTile = backwardTile;
            RhythmMover.leftTile = leftTile;
            RhythmMover.rightTile = rightTile;
            RhythmMover.centerPoint = centerPoint;

            RhythmMover.enabled = true;
        }

        public void EnableHook(HookSeller seller, HookGrabGame game)
        {
            Debug.Log("EnableHook on " + gameObject.name);
            DisableAll();

            HookPlayer.Initialize(game);
            HookPlayer.seller = seller;

            HookPlayer.enabled = true;
        }

        public void EnableReaction(ReactionGame game)
        {
            Debug.Log("EnableReaction on " + gameObject.name);
            DisableAll();

            ReactionInput.Init(game);

            ReactionInput.enabled = true;
        }

        public void DisableAll()
        {
            Debug.Log("DisableAll on " + gameObject.name);
            SideMove.enabled = false;
            ProjectileThrower.enabled = false;
            RhythmMover.enabled = false;
            HookPlayer.enabled = false;
            ReactionInput.enabled = false;
        }
    }
}