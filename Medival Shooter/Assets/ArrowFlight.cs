using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifeTime = 10f;
    Rigidbody rb;
    bool hasHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.parent = null;     // detach from bow/camera
        Destroy(gameObject, lifeTime); // safe lifetime
        Debug.Log("Arrow.Start at " + transform.position);
    }

    void FixedUpdate()
    {
        if (!hasHit && rb != null && rb.linearVelocity.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
    }

    void OnCollisionEnter(Collision col)
    {
        if (hasHit) return;
        // ignore bow collisions if not already
        if (col.gameObject.CompareTag("Bow")) return;

        hasHit = true;
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
        }
        transform.SetParent(col.transform, true);
        Debug.Log("Arrow hit " + col.gameObject.name);
    }
}
