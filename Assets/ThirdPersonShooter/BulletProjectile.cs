using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
    [SerializeField] private ParticleSystem explosion;
    private Rigidbody bulletRigidbody;

    private void Awake() {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        float speed = 50f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<BulletTarget>() != null) {
            if (other.CompareTag("finalBoss") && Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, other.transform.position) > 4f)
            {
                ;
            }
            else
            {
                // Hit target
                Vector3 targetPosition = other.transform.position;
                StartCoroutine(Explode(targetPosition));
                Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }

        } else {
            // Hit something else
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private IEnumerator Explode(Vector3 targetPosition)
    {
        var temp = Instantiate(explosion, targetPosition, Quaternion.identity);
        yield return new WaitForSeconds (0.3f);
        temp.Pause();
    }

}