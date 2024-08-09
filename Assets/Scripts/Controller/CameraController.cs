using UnityEngine;

namespace Controller
{
    public class CameraController : MonoBehaviour
    {
        public Transform target; // Takip edilecek karakter
        public Vector3 offset; // Kameranın karaktere göre pozisyonu
        public float distance = 10f; // Kameranın hedef karakterden olan mesafesi
        public float smoothSpeed = 0.125f; // Kamera hareketinin yumuşaklığı

        private void LateUpdate()
        {
            HandlePosition();
        }

        private void HandlePosition()
        {
            // İstenilen pozisyonu hesapla
            var desiredPosition = target.position + offset - target.forward * distance;

            // Pozisyonu yumuşak bir şekilde güncelle
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Kameranın hedefe bakmasını sağla
            transform.LookAt(target);
        }
    }
}
