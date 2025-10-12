using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public Transform playerBody; // optional, leave null to rotate camera only

    float xRotation = 0f;
    bool cursorLocked = false;

    void Start()
    {
        // Start unlocked; wait for user to click to lock
        UnlockCursor();
    }

    void Update()
    {
        // Lock on left click if not locked
        if (!cursorLocked && Input.GetMouseButtonDown(0))
        {
            LockCursor();
        }

        // Unlock on Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }

        if (!cursorLocked) return; // don't process look when unlocked

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (playerBody != null)
            playerBody.Rotate(Vector3.up * mouseX);
        else
            transform.Rotate(Vector3.up * mouseX, Space.World);
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLocked = true;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorLocked = false;
    }
}
