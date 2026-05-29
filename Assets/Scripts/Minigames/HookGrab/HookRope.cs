using UnityEngine;

namespace Minigames.HookGrab
{
    [RequireComponent(typeof(LineRenderer))]
    public class HookRope : MonoBehaviour
    {
        private LineRenderer line;

        private Transform startPoint;
        private Transform endPoint;

        private void Awake()
        {
            line =
                GetComponent<LineRenderer>();
        }

        public void Init(
            Transform start,
            Transform end
        )
        {
            startPoint = start;
            endPoint = end;
        }

        private void Update()
        {
            if (
                startPoint == null ||
                endPoint == null
            )
            {
                return;
            }

            line.SetPosition(
                0,
                startPoint.position
            );

            line.SetPosition(
                1,
                endPoint.position
            );
        }
    }
}