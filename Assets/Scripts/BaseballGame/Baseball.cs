using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baseball : MonoBehaviour {

    [SerializeField] private LayerMask obstacleLayerMask; // Ball will be destroyed if it collides
    [SerializeField] private SphereCollider itemsCollider;

    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public void Throw(float speed) {
        rb.velocity = speed * transform.forward;
    }

    public void Hit() {
        rb.velocity = -rb.velocity;
        itemsCollider.enabled = false;

    }

    private void OnCollisionEnter(Collision collision) {
        if (Utils.LayerInMask(collision.gameObject.layer, obstacleLayerMask)) {
            Destroy(gameObject);
        }
    }
}
