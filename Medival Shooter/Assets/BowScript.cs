using UnityEngine;
using UnityEngine.UI;

public class BowScript : MonoBehaviour
{
    [Header("References")]
    public GameObject arrowPrefab;    // assign arrow prefab
    public Transform shootPoint;      // assign ShootPoint (child transform)

    [Header("Charging")]
    public float maxForce = 40f;      // maximum launch speed
    public float chargeSpeed = 20f;   // how fast force grows (units/sec)
    public float minForce = 5f;       // optional minimum force so very short taps still fire

    [Header("UI")]
    public Slider powerBar;           // assign PowerBar Slider
    public Image powerFillImage;      // assign Slider Fill Image (optional)

    private float currentForce = 0f;
    private bool isCharging = false;

    void Start()
    {
        // If user didn't assign fill Image manually, try to get it from slider
        if (powerBar != null && powerFillImage == null && powerBar.fillRect != null)
        {
            powerFillImage = powerBar.fillRect.GetComponent<Image>();
        }

        if (powerBar != null)
        {
            powerBar.minValue = 0f;
            powerBar.maxValue = maxForce;
            powerBar.value = 0f;
        }
    }

    void Update()
    {
        // Start charging when key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            currentForce = 0f; // start from zero or from minForce if you prefer
        }

        // While holding, increase force
        if (isCharging && Input.GetKey(KeyCode.Space))
        {
            currentForce += chargeSpeed * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, 0f, maxForce);

            if (powerBar != null)
                powerBar.value = currentForce;

            if (powerFillImage != null)
            {
                float t = currentForce / maxForce;
                // green -> yellow -> red
                Color c = Color.Lerp(Color.green, Color.yellow, t * 0.7f);
                c = Color.Lerp(Color.green, Color.red, t);
                powerFillImage.color = c;
            }
        }

        // Release: shoot
        if (isCharging && Input.GetKeyUp(KeyCode.Space))
        {
            ShootArrow();
            isCharging = false;
            currentForce = 0f;

            if (powerBar != null)
                powerBar.value = 0f;
            if (powerFillImage != null)
                powerFillImage.color = Color.green;
        }
    }

    void ShootArrow()
    {
        if (arrowPrefab == null || shootPoint == null) return;

        // Ensure at least minForce
        float forceToApply = Mathf.Max(currentForce, minForce);

        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Set velocity directly for predictable result
            rb.linearVelocity = shootPoint.forward * forceToApply;

            // Safety: ensure good collision settings on spawned arrow
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        // Optional: destroy arrow after some seconds so the scene doesn't fill up
        Destroy(arrow, 10f);
    }
}
