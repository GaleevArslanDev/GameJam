using Minigames.Throwing;
using Minigames.Rhythm;
using Minigames.HookGrab;
using Minigames.Reaction;
using Minigames.UI;
using UnityEngine;

namespace Minigames.Core
{
    public class MinigameContext
    {
        public GameObject Player;
        public Transform PlayerTransform;

        public MinigamePlayerSystems Systems;

        public ThrowingGameUI ThrowingUI;
        public MinigameResultUI ResultUI;

        public SellerNPC Seller;

        public CharacterController CharacterController;
        public Player.PlayerMotor Motor;
        public Player.PlayerLook Look;
    }
}