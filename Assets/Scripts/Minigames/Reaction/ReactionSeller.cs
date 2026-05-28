using UnityEngine;

namespace Minigames.Reaction
{
    public class ReactionSeller : MonoBehaviour
    {
        [SerializeField]
        private float speed = 4f;

        [SerializeField]
        private float minX = -4f;

        [SerializeField]
        private float maxX = 4f;

        private int direction = 1;

        public float CurrentX =>
            transform.position.x;

        private void Update()
        {
            Vector3 pos = transform.position;

            pos.x +=
                direction *
                speed *
                Time.deltaTime;

            if (pos.x >= maxX)
            {
                pos.x = maxX;

                direction = -1;
            }

            if (pos.x <= minX)
            {
                pos.x = minX;

                direction = 1;
            }

            transform.position = pos;
        }
    }
}