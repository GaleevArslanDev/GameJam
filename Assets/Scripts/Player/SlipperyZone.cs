using UnityEngine;

namespace Player
{
    public class SlipperyZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMotor motor))
            {
                Vector3 dir = motor.transform.forward;

                if (other.TryGetComponent(out Rigidbody rb))
                {
                    dir = rb.linearVelocity.normalized;
                }

                if (other.TryGetComponent(out PlayerSlipState slip))
                {
                    slip.Slip(dir);
                }
            }
        }
    }
}