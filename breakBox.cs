using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakBox : MonoBehaviour {

    public Rigidbody rb;
    [Tooltip("The normal force applied in the player when jumping on a box")]
    public float forceReturn = 2f;
    [Tooltip("The force applied in the player when jumping on a box and the space bar is being pressed")]
    public float extraForceReturn = 7f;
    public GameObject crackedBox;

    void Awake() {
        if (rb == null) rb = GetComponent<Rigidbody>();
        // Physics.IgnoreLayerCollision(gameObject.layer, 11);
    }

    void OnCollisionEnter(Collision collision) {
        float relativeVelocity = collision.relativeVelocity.magnitude;
        if (collision.gameObject.tag == "boxBreakable") {
            if (collision.contacts[0].normal.y >= 0.7 && relativeVelocity >= 10) {
                BreakBox(collision);
                if (Input.GetKey(KeyCode.Space)) rb.velocity = Vector3.up * extraForceReturn;
                else rb.velocity = Vector3.up * forceReturn;
            }
        }
    }

    void BreakBox(Collision collision) {
        GameObject clone;
        clone = Instantiate(crackedBox, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
        Destroy(collision.gameObject);
        // Destroy(clone, 3);
    }
}
