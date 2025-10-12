using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ArrowFlight : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 v = rb.linearVelocity;
        if (v.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(v);
        }
    }

    // Optional: make the arrow stick on collision
    void OnCollisionEnter(Collision col)
    {
        // Basic "stick" behavior
        rb.isKinematic = true;
        // parent the arrow to what it hit so it moves with it
        transform.SetParent(col.collider.transform, true);

        // optionally disable collider or remove scripts that move it further
        // GetComponent<Collider>().enabled = false;
        Destroy(this); // remove this script so it doesn't try to change rotation anymore
    }
}
