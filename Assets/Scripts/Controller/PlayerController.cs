using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float turnSpeed = 360f;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Move();
            Turn();
        }

        private void Move()
        {
            var moveVertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            var move = transform.forward * moveVertical;
            rb.MovePosition(rb.position + move);
        }

        private void Turn()
        {
            var turnHorizontal = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
            var turnRotation = Quaternion.Euler(0f, turnHorizontal, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}