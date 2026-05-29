using UnityEngine;

namespace Minigames.HookGrab
{
    public class HookSeller : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3f;

        [SerializeField]
        private float minX = -3f;

        [SerializeField]
        private float maxX = 3f;

        private int direction = 1;

        private bool paused;

        public void SetPaused(
            bool value
        )
        {
            paused = value;
        }

        private void Update()
        {
            if (paused)
                return;

            Vector3 pos =
                transform.localPosition;

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

            transform.localPosition = pos;
        }
    }
}