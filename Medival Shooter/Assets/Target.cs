using UnityEngine;

public class Target : MonoBehaviour
{
    public int health = 1;       // How many hits it can take
    public bool stickOnHit = true;
    public GameObject hitEffect; // Optional: particle effect prefab

    void OnCollisionEnter(Collision col)
    {
        // Check if the object that hit us is tagged as "Arrow"
        if (col.collider.CompareTag("Arrow"))
        {
            // Optional: spawn a particle effect
            if (hitEffect != null)
                Instantiate(hitEffect, col.contacts[0].point, Quaternion.identity);

            health--;

            if (stickOnHit)
            {
                // Make the arrow stick to the target
                Rigidbody rb = col.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    col.collider.transform.SetParent(transform, true);
                }
            }

            if (health <= 0)
            {
                Destroy(gameObject); // Destroy target when health reaches 0
            }
        }
    }
}
